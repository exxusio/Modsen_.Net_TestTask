import { Component, OnDestroy, OnInit } from '@angular/core';
import {
    AppNotification,
    NotificationService,
} from '../../../data/services/notification.service';
import { Subscription } from 'rxjs/internal/Subscription';
import { NgFor } from '@angular/common';
import { Router } from '@angular/router';

@Component({
    selector: 'app-notification',
    standalone: true,
    imports: [NgFor],
    templateUrl: './notification.component.html',
    styleUrl: './notification.component.css',
})
export class NotificationComponent implements OnInit, OnDestroy {
    public notifications: AppNotification[] = [];
    private subscription!: Subscription;

    constructor(
        private readonly _notificationService: NotificationService,
        private readonly _router: Router
    ) {}

    public ngOnInit(): void {
        this.subscription = this._notificationService.notifications$.subscribe(
            (notification) => {
                this.notifications.push(notification);
                setTimeout(() => this.activateNotification(notification), 10);
                setTimeout(() => this.removeNotification(notification), 6000);
            }
        );
    }

    public ngOnDestroy(): void {
        this.subscription.unsubscribe();
    }

    public activateNotification(notification: AppNotification) {
        const index = this.notifications.indexOf(notification);
        if (index !== -1) {
            const element = document.querySelectorAll('.notify')[index];
            element?.classList.add('active');
        }
    }

    public removeNotification(notification: AppNotification) {
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

    public handleURL(notification: AppNotification) {
        if (notification.url) {
            this._router.navigateByUrl(notification.url);
        }
    }
}
