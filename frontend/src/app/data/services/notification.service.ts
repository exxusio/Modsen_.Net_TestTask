import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Subject } from 'rxjs/internal/Subject';
import { HubConnection } from '@microsoft/signalr/dist/esm/HubConnection';
import { HubConnectionBuilder } from '@microsoft/signalr/dist/esm/HubConnectionBuilder';
import { NotifyType } from '../configs/notify-type';
import { ApiConfig } from '../configs/api-config';
import { TokenService } from './token.service';

export interface AppNotification {
    action: string;
    text: string;
    type: 'success' | 'error' | 'info' | 'warning';
    url?: string;
}

@Injectable({
    providedIn: 'root',
})
export class NotificationService {
    private readonly _apiUrl = `${ApiConfig.BaseUrl}`;

    private notificationSubject = new Subject<AppNotification>();
    public notifications$ = this.notificationSubject.asObservable();

    private _hubConnection: HubConnection;

    constructor(private readonly _tokenService: TokenService) {
        const accessToken = this._tokenService.Tokens?.accessToken.value;

        const options: signalR.IHttpConnectionOptions = {
            skipNegotiation: true,
            transport: signalR.HttpTransportType.WebSockets,
            accessTokenFactory: () => `${accessToken}`,
        };

        this._hubConnection = new HubConnectionBuilder()
            .withUrl(`${this._apiUrl}/notify/event`, options)
            .build();

        this.startConnection();
    }

    private startConnection() {
        this._hubConnection.start().then(() => {
            this.subscribeToHub();
        });
    }

    public showCustomNotification(
        action: string,
        text: string,
        type: 'success' | 'error' | 'info' | 'warning' = 'info',
        url?: string
    ) {
        this.notificationSubject.next({ action, text, type, url });
    }

    public showSuccessNotification(message: string): void {
        this.notificationSubject.next({
            action: 'Success',
            text: message,
            type: 'success',
        });
    }

    public showErrorNotification(message: string): void {
        this.notificationSubject.next({
            action: 'Error',
            text: message,
            type: 'error',
        });
    }

    public showInfoNotification(message: string): void {
        this.notificationSubject.next({
            action: 'Info',
            text: message,
            type: 'info',
        });
    }

    public showWarningNotification(message: string, url?: string): void {
        this.notificationSubject.next({
            action: 'Warning',
            text: message,
            type: 'warning',
            url: url,
        });
    }

    public showUnexpectedError(): void {
        this.notificationSubject.next({
            action: 'Error',
            text: 'An unexpected error occurred. Please try again later',
            type: 'error',
        });
    }

    private subscribeToHub() {
        this._hubConnection.on(NotifyType.EventUpdated, (message) => {
            this.showWarningNotification(
                message?.message +
                    `: ${message?.event.name}<br>Click to view details`,
                `events/${message?.event.id}`
            );
        });
        this._hubConnection.on(NotifyType.EventDeleted, (message) => {
            this.showWarningNotification(
                message?.message + `: ${message?.event.name}`
            );
        });
    }
}
