import { NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { UserService } from '../../../data/services/user.service';
import { NotificationService } from '../../../data/services/notification.service';

@Component({
    selector: 'app-menu',
    standalone: true,
    imports: [RouterModule, NgIf],
    templateUrl: './menu.component.html',
    styleUrl: './menu.component.css',
})
export class MenuComponent {
    public isAdminStatus: boolean | null = null;

    constructor(
        private readonly _userService: UserService,
        private readonly _router: Router,
        private readonly _notifyService: NotificationService
    ) {}

    ngOnInit(): void {
        this._userService.isAdmin().subscribe((isAdmin) => {
            this.isAdminStatus = isAdmin;
        });
    }

    public isLoggedIn(): boolean {
        return this._userService.isAuth();
    }

    public isAdmin(): boolean {
        return this.isAdminStatus === true;
    }

    public logout(): void {
        this._userService.logout();
        this._router.navigateByUrl('/');
        this._notifyService.showInfoNotification(
            'You have logged out of your account'
        );
    }

    public preventNavigation(event: Event): void {
        event.preventDefault();
    }
}
