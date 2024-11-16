import { Injectable } from '@angular/core';
import { tap } from 'rxjs/internal/operators/tap';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { throwError } from 'rxjs/internal/observable/throwError';
import { CookiesConfig } from '../configs/cookies-config';
import { AppCookieService } from './app-cookie.service';
import { ApiConfig } from '../configs/api-config';
import { Tokens } from '../models/tokens/tokens';
import { TokenRefresh } from '../models/tokens/token-refresh';

@Injectable({
    providedIn: 'root',
})
export class TokenService {
    public Tokens: Tokens | null = null;

    private readonly _apiUrl = `${ApiConfig.BaseUrl}/tokens`;

    constructor(
        private readonly _http: HttpClient,
        private readonly _cookieService: AppCookieService
    ) {
        if (!this.Tokens) {
            this.Tokens = this._cookieService.get<Tokens>(CookiesConfig.Tokens);
        }
    }

    public refreshUserToken(): Observable<Tokens> {
        if (this.Tokens === null) {
            return throwError(() => new Error('Tokens are null'));
        }

        const tokenRefreshRequest: TokenRefresh = {
            key: this.Tokens.refreshToken.value,
        };

        return this._http
            .post<Tokens>(`${this._apiUrl}/refresh`, tokenRefreshRequest)
            .pipe(
                tap((tokenResponse) => {
                    this.saveTokens(tokenResponse);
                })
            );
    }

    public saveTokens(tokens: Tokens) {
        this.Tokens = tokens;
        this._cookieService.save<Tokens>(CookiesConfig.Tokens, this.Tokens);
    }

    public deleteTokens() {
        this.Tokens = null;
        this._cookieService.delete(CookiesConfig.Tokens);
    }
}
