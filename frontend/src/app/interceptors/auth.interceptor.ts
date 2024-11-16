import {
    HttpErrorResponse,
    HttpHandlerFn,
    HttpInterceptorFn,
    HttpRequest,
} from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { take } from 'rxjs/internal/operators/take';
import { filter } from 'rxjs/internal/operators/filter';
import { switchMap } from 'rxjs/internal/operators/switchMap';
import { catchError } from 'rxjs/internal/operators/catchError';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { throwError } from 'rxjs/internal/observable/throwError';
import { TokenService } from '../data/services/token.service';
import { UserService } from '../data/services/user.service';

let isRefreshing = false;
let refreshTokenSubject: BehaviorSubject<string | null> = new BehaviorSubject<
    string | null
>(null);

export const authInterceptor: HttpInterceptorFn = (httpRequest, next) => {
    const tokenService = inject(TokenService);
    const userService = inject(UserService);
    const router = inject(Router);
    const tokens = tokenService.Tokens;

    if (!tokens) {
        return next(httpRequest);
    }

    if (httpRequest.url.includes('/tokens/refresh')) {
        return next(httpRequest);
    }

    return next(addAccessToken(httpRequest, tokens.accessToken.value)).pipe(
        catchError((error: HttpErrorResponse) => {
            if (error.status === 403 || error.status === 401) {
                return handleTokenRefresh(
                    tokenService,
                    userService,
                    router,
                    httpRequest,
                    next
                );
            }
            return throwError(() => error);
        })
    );
};

const handleTokenRefresh = (
    tokenService: TokenService,
    userService: UserService,
    router: Router,
    httpRequest: HttpRequest<any>,
    next: HttpHandlerFn
) => {
    if (!isRefreshing) {
        isRefreshing = true;
        refreshTokenSubject.next(null);

        return tokenService.refreshUserToken().pipe(
            switchMap((response) => {
                isRefreshing = false;
                refreshTokenSubject.next(response.accessToken.value);
                return next(
                    addAccessToken(httpRequest, response.accessToken.value)
                );
            }),
            catchError((error) => {
                isRefreshing = false;
                userService.logout();
                router.navigateByUrl('/');
                return throwError(() => error);
            })
        );
    } else {
        return refreshTokenSubject.pipe(
            filter((token) => token !== null),
            take(1),
            switchMap((token) => next(addAccessToken(httpRequest, token!)))
        );
    }
};

const addAccessToken = (req: HttpRequest<any>, token: string) => {
    return req.clone({
        setHeaders: {
            Authorization: `Bearer ${token}`,
        },
    });
};
