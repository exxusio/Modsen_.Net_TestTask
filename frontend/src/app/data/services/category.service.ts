import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { CategoryCreate } from '../models/categories/category-create';
import { CategoryRead } from '../models/categories/category-read';
import { ApiConfig } from '../configs/api-config';

@Injectable({
    providedIn: 'root',
})
export class CategoryService {
    private readonly _apiUrl = `${ApiConfig.BaseUrl}/categories`;

    constructor(private readonly _http: HttpClient) {}

    public getAllCategories(): Observable<CategoryRead[]> {
        return this._http.get<CategoryRead[]>(this._apiUrl);
    }

    public createCategory(
        categoryCreate: CategoryCreate
    ): Observable<CategoryRead> {
        return this._http.post<CategoryRead>(this._apiUrl, categoryCreate);
    }
}
