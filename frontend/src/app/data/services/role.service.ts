import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { RoleRead } from '../models/roles/role-read';
import { ApiConfig } from '../configs/api-config';

@Injectable({
    providedIn: 'root',
})
export class RoleService {
    private readonly _apiUrl = `${ApiConfig.BaseUrl}/roles`;

    constructor(private readonly _http: HttpClient) {}

    public getAllRoles(): Observable<RoleRead[]> {
        return this._http.get<RoleRead[]>(this._apiUrl);
    }
}
