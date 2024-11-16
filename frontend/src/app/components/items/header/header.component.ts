import { NgIf } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { Component, HostListener } from '@angular/core';
import { UserService } from '../../../data/services/user.service';
import { FilterService } from '../../../data/services/filter.service';
import { EventFilter } from '../../../data/models/events/event-filter';
import { FilterComponent } from '../filter/filter.component';
import { NotificationComponent } from '../notification/notification.component';
import { NotificationService } from '../../../data/services/notification.service';

@Component({
    selector: 'app-header',
    standalone: true,
    imports: [RouterModule, NgIf, FilterComponent, NotificationComponent],
    templateUrl: './header.component.html',
    styleUrl: './header.component.css',
})
export class HeaderComponent {
    public menuOpen = false;
    public isHomePage = false;

    constructor(
        private readonly _userService: UserService,
        private readonly _filterService: FilterService,
        private readonly _router: Router,
        private readonly _notifyService: NotificationService
    ) {
        this._router.events.subscribe(() => {
            this.isHomePage = this._router.url === '/';
        });
    }

    public onFilterChanged(filter: EventFilter): void {
        this._filterService.emitFilterChanged(filter);
    }

    public onSearch(event: Event): void {
        this._router.navigateByUrl('/').then(() => {
            const input = (event.target as HTMLInputElement).value;
            this._filterService.emitFilterNameChanged(input);
        });
    }

    public isLoggedIn(): boolean {
        return this._userService.isAuth();
    }

    public toggleMenu(): void {
        this.menuOpen = !this.menuOpen;
    }

    public logout(): void {
        this._userService.logout();
        this._router.navigateByUrl('/');
        this._notifyService.showInfoNotification(
            'You have logged out of your account'
        );
    }

    @HostListener('document:click', ['$event'])
    private onDocumentClick(event: MouseEvent): void {
        const clickedInsideMenu = (event.target as HTMLElement).closest(
            '.header-right .user-menu'
        );

        if (!clickedInsideMenu) {
            this.menuOpen = false;
        }
    }
}
