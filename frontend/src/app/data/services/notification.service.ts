import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/internal/Subject';

export interface AppNotification {
    action: string;
    text: string;
    type: 'success' | 'error' | 'info';
}

@Injectable({
    providedIn: 'root',
})
export class NotificationService {
    private notificationSubject = new Subject<AppNotification>();
    public notifications$ = this.notificationSubject.asObservable();

    public showCustomNotification(
        action: string,
        text: string,
        type: 'success' | 'error' | 'info' = 'info'
    ) {
        this.notificationSubject.next({ action, text, type });
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

    public showUnexpectedError(): void {
        this.notificationSubject.next({
            action: 'Error',
            text: 'An unexpected error occurred. Please try again later.',
            type: 'error',
        });
    }
}
