import { Component } from '@angular/core';
import { NgFor, NgIf } from '@angular/common';
import { ChangeRoleComponent } from '../../../../modals/change-role/change-role.component';
import { KeyByValueHelper } from '../../../../../helpers/key-by-value-hepler';
import { RowComponent } from '../../../../items/table/row/row.component';
import { UserService } from '../../../../../data/services/user.service';
import { UserRead } from '../../../../../data/models/users/user-read';
import { SearchComponent } from '../../search/search.component';
import { NotificationService } from '../../../../../data/services/notification.service';

@Component({
    selector: 'app-user-section',
    standalone: true,
    imports: [NgFor, NgIf, RowComponent, ChangeRoleComponent, SearchComponent],
    templateUrl: './user-section.component.html',
    styleUrl: './user-section.component.css',
})
export class UserSectionComponent {
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
        private readonly _userService: UserService,
        private readonly _notifyService: NotificationService
    ) {}

    public ngOnInit(): void {
        this.loadUsers();
    }

    private loadUsers(): void {
        this._userService.getAllUsers().subscribe({
            next: (users) => {
                this._users = users;
                this.users = this._users;
                this.loadDataColumns();
            },
            error: (error) => {
                this._notifyService.showUnexpectedError();
            },
        });
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
}
