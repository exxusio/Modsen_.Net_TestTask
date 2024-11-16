import { Injectable } from '@angular/core';
import { tap } from 'rxjs/internal/operators/tap';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/internal/operators/map';
import { Observable } from 'rxjs/internal/Observable';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { UserChangePassword } from '../models/users/user-change-password';
import { UserChangeRole } from '../models/users/user-change-role';
import { UserDetailed } from '../models/users/user-detailed';
import { UserRegister } from '../models/users/user-register';
import { UserUpdate } from '../models/users/user-update';
import { UserLogin } from '../models/users/user-login';
import { UserRead } from '../models/users/user-read';
import { ApiConfig } from '../configs/api-config';
import { Tokens } from '../models/tokens/tokens';
import { TokenService } from './token.service';

@Injectable({
    providedIn: 'root',
})
export class UserService {
    public isAuth(): boolean {
        return !!this._tokenService.Tokens;
    }
    public isAdmin(): Observable<boolean> {
        this.checkAdminStatus();
        return this.isAdminSubject.asObservable();
    }

    private isAdminSubject: BehaviorSubject<boolean> =
        new BehaviorSubject<boolean>(false);

    private readonly _apiUrl = `${ApiConfig.BaseUrl}/users`;

    constructor(
        private readonly _http: HttpClient,
        private readonly _tokenService: TokenService
    ) {}

    public getAllUsers(): Observable<UserRead[]> {
        return this._http.get<UserRead[]>(this._apiUrl);
    }

    public getUser(userId: string): Observable<UserDetailed> {
        return this._http.get<UserDetailed>(`${this._apiUrl}/${userId}`);
    }

    public getCurrentUser(): Observable<UserDetailed> {
        return this._http.get<UserDetailed>(`${this._apiUrl}/me`);
    }

    public register(userRegister: UserRegister): Observable<UserRead> {
        return this._http.post<UserRead>(
            `${this._apiUrl}/register`,
            userRegister
        );
    }

    public login(userLogin: UserLogin): Observable<Tokens> {
        return this._http.post<Tokens>(`${this._apiUrl}/login`, userLogin).pipe(
            tap((tokens) => {
                this._tokenService.saveTokens(tokens);
                this.checkAdminStatus();
            })
        );
    }

    public updateUser(user: UserUpdate): Observable<UserRead> {
        return this._http.put<UserRead>(this._apiUrl, user);
    }

    public changePassword(
        changePasswordData: UserChangePassword
    ): Observable<UserRead> {
        return this._http.put<UserRead>(
            `${this._apiUrl}/user/password`,
            changePasswordData
        );
    }

    public changeUserRole(
        changeRoleData: UserChangeRole
    ): Observable<UserRead> {
        var user = this._http.put<UserRead>(
            `${this._apiUrl}/user/assign-role`,
            changeRoleData
        );
        this.checkAdminStatus();
        return user;
    }

    public logout() {
        this._tokenService.deleteTokens();
        this.checkAdminStatus();
    }

    private checkAdminStatus(): void {
        if (!this.isAuth()) {
            this.isAdminSubject.next(false);
            return;
        }
        this.getCurrentUser()
            .pipe(
                map((user) => user.roleName == 'Admin'),
                tap((isAdmin) => this.isAdminSubject.next(isAdmin))
            )
            .subscribe();
    }
}
