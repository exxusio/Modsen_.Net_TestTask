import { Component, EventEmitter, Input, Output } from '@angular/core';
import { RowComponent } from '../../items/table/row/row.component';
import { SearchComponent } from '../../pages/admin-panel/search/search.component';
import { ChangeRoleComponent } from '../change-role/change-role.component';
import { NgFor, NgIf } from '@angular/common';
import { UserRead } from '../../../data/models/users/user-read';
import { RegistrationService } from '../../../data/services/registration.service';
import { KeyByValueHelper } from '../../../helpers/key-by-value-hepler';
import { NotificationService } from '../../../data/services/notification.service';

@Component({
    selector: 'app-event-registration',
    standalone: true,
    imports: [RowComponent, SearchComponent, ChangeRoleComponent, NgFor, NgIf],
    templateUrl: './event-registration.component.html',
    styleUrl: './event-registration.component.css',
})
export class EventRegistrationComponent {
    @Input() eventId!: string | null;
    @Output() close = new EventEmitter<void>();

    private _users: UserRead[] = [];
    public readonly searchField: string = 'email';

    public get getUsers(): UserRead[] {
        return this._users;
    }

    public users: UserRead[] = [];
    public columns: string[] = [];

    public isChangeRole = false;
    public currentUserId!: string;

    constructor(
        private readonly _registrationService: RegistrationService,
        private readonly _notifyService: NotificationService
    ) {}

    public ngOnInit(): void {
        this.loadRegistrations();
    }

    private loadRegistrations(): void {
        if (this.eventId != null) {
            this._registrationService
                .getEventRegistrations(this.eventId)
                .subscribe({
                    next: (registrations) => {
                        this._users = registrations.map(
                            (registration) => registration.participant
                        );
                        this.users = this._users;
                        this.loadDataColumns();
                    },
                    error: (err) => {
                        this._notifyService.showUnexpectedError();
                    },
                });
        }
    }

    private loadDataColumns() {
        if (this.users.length > 0) {
            this.columns = [
                KeyByValueHelper.getKeyByValue(
                    this.users[0],
                    this.users[0].email
                ),
                KeyByValueHelper.getKeyByValue(
                    this.users[0],
                    this.users[0].firstName
                ),
                KeyByValueHelper.getKeyByValue(
                    this.users[0],
                    this.users[0].lastName
                ),
            ];
        }
    }

    public onUserClicked(data: any): void {
        this.currentUserId = data['id'];
        this.isChangeRole = true;
    }

    public closeModal(): void {
        this.close.emit();
    }
}
