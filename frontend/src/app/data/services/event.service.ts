import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { HttpClient, HttpParams } from '@angular/common/http';
import { EventsByFilter } from '../models/events/event-by-filter';
import { EventUpdate } from '../models/events/event-update';
import { EventCreate } from '../models/events/event-create';
import { EventFilter } from '../models/events/event-filter';
import { EventRead } from '../models/events/event-read';
import { HttpHelper } from '../../helpers/http-helper';
import { ApiConfig } from '../configs/api-config';

@Injectable({
    providedIn: 'root',
})
export class EventService {
    private readonly _apiUrl = `${ApiConfig.BaseUrl}/events`;

    constructor(private readonly http: HttpClient) {}

    public getAllEvents(): Observable<EventRead[]> {
        return this.http.get<EventRead[]>(this._apiUrl);
    }

    public getEventById(eventId: string): Observable<EventRead> {
        return this.http.get<EventRead>(`${this._apiUrl}/${eventId}`);
    }

    public getEventsByFilter(
        filter: EventFilter,
        pageNumber: number,
        pageSize: number
    ): Observable<EventsByFilter> {
        let params = new HttpParams();
        params = HttpHelper.AddEventFilterToQuery(params, filter);
        params = HttpHelper.AddPageSizeToQuery(params, pageSize);
        return this.http.get<EventsByFilter>(
            `${this._apiUrl}/filter/page=${pageNumber}`,
            { params }
        );
    }

    public createEvent(eventCreate: EventCreate): Observable<EventRead> {
        return this.http.post<EventRead>(this._apiUrl, eventCreate);
    }

    public updateEvent(
        eventId: string,
        eventUpdate: EventUpdate
    ): Observable<EventRead> {
        return this.http.put<EventRead>(
            `${this._apiUrl}/${eventId}`,
            eventUpdate
        );
    }

    public deleteEvent(eventId: string): Observable<EventRead> {
        return this.http.delete<EventRead>(`${this._apiUrl}/${eventId}`);
    }
}
