import { Component, OnDestroy, OnInit } from '@angular/core';
import {
    AppNotification,
    NotificationService,
} from '../../../data/services/notification.service';
import { Subscription } from 'rxjs/internal/Subscription';
import { NgFor } from '@angular/common';

@Component({
    selector: 'app-notification',
    standalone: true,
    imports: [NgFor],
    templateUrl: './notification.component.html',
    styleUrl: './notification.component.css',
})
export class NotificationComponent implements OnInit, OnDestroy {
    notifications: AppNotification[] = [];
    private subscription!: Subscription;

    constructor(private readonly _notificationService: NotificationService) {}

    ngOnInit(): void {
        this.subscription = this._notificationService.notifications$.subscribe(
            (notification) => {
                this.notifications.push(notification);
                setTimeout(() => this.activateNotification(notification), 10);
                setTimeout(() => this.removeNotification(notification), 4000);
            }
        );
    }

    ngOnDestroy(): void {
        this.subscription.unsubscribe();
    }

    activateNotification(notification: AppNotification) {
        const index = this.notifications.indexOf(notification);
        if (index !== -1) {
            const element = document.querySelectorAll('.notify')[index];
            element?.classList.add('active');
        }
    }

    removeNotification(notification: AppNotification) {
        const index = this.notifications.indexOf(notification);
        if (index !== -1) {
            const element = document.querySelectorAll('.notify')[index];
            element?.classList.remove('active');
            setTimeout(() => {
                this.notifications = this.notifications.filter(
                    (n) => n !== notification
                );
            }, 300);
        }
    }
}
