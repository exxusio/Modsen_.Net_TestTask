import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
    providedIn: 'root',
})
export class AppCookieService {
    constructor(private readonly _cookieService: CookieService) {}

    public save<TItem>(name: string, item: TItem) {
        const serializedObject = JSON.stringify(item);
        this._cookieService.set(name, serializedObject);
    }

    public get<TItem>(name: string) {
        const serializedObject = this._cookieService.get(name);
        if (!serializedObject) {
            return null;
        }
        const object: TItem = JSON.parse(serializedObject);
        return object;
    }

    public delete(name: string) {
        this._cookieService.delete(name);
    }
}
