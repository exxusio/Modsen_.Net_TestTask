import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { RegistrationRead } from '../models/registrations/registration-read';
import { ApiConfig } from '../configs/api-config';

@Injectable({
    providedIn: 'root',
})
export class RegistrationService {
    private readonly _apiUrl = `${ApiConfig.BaseUrl}/registrations`;

    constructor(private readonly _http: HttpClient) {}

    public getAllRegistrations(): Observable<RegistrationRead[]> {
        return this._http.get<RegistrationRead[]>(this._apiUrl);
    }

    public getRegistrationDetails(
        eventId: string
    ): Observable<RegistrationRead> {
        return this._http.get<RegistrationRead>(`${this._apiUrl}/${eventId}`);
    }

    public getEventRegistrations(
        eventId: string
    ): Observable<RegistrationRead[]> {
        return this._http.get<RegistrationRead[]>(
            `${this._apiUrl}/event/${eventId}`
        );
    }

    public getUserRegistrations(): Observable<RegistrationRead[]> {
        return this._http.get<RegistrationRead[]>(`${this._apiUrl}/me`);
    }

    public registerForEvent(eventId: string): Observable<RegistrationRead> {
        return this._http.post<RegistrationRead>(
            `${this._apiUrl}/${eventId}`,
            {}
        );
    }

    public unregisterFromEvent(eventId: string): Observable<RegistrationRead> {
        return this._http.delete<RegistrationRead>(
            `${this._apiUrl}/${eventId}`,
            {}
        );
    }
}
