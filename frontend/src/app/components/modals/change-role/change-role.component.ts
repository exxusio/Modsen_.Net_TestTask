import {
    FormControl,
    FormGroup,
    ReactiveFormsModule,
    Validators,
} from '@angular/forms';
import { NgIf, NgFor, DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { UserChangeRole } from '../../../data/models/users/user-change-role';
import { UserDetailed } from '../../../data/models/users/user-detailed';
import { UserService } from '../../../data/services/user.service';
import { RoleService } from '../../../data/services/role.service';
import { RoleRead } from '../../../data/models/roles/role-read';
import { NotificationService } from '../../../data/services/notification.service';

@Component({
    selector: 'app-change-role',
    standalone: true,
    imports: [NgFor, NgIf, ReactiveFormsModule, DatePipe],
    templateUrl: './change-role.component.html',
    styleUrl: './change-role.component.css',
})
export class ChangeRoleComponent {
    @Input() userId!: string;
    @Output() close = new EventEmitter<void>();

    public user!: UserDetailed;
    public roles: RoleRead[] = [];
    public optionField = 'name';
    public valueField = 'id';

    public changeRoleForm: FormGroup = new FormGroup({
        roleId: new FormControl<string>('', [Validators.required]),
    });

    public roleId() {
        return this.changeRoleForm.controls['roleId'];
    }

    constructor(
        private readonly _userService: UserService,
        private readonly _roleService: RoleService,
        private readonly _notifyService: NotificationService
    ) {}

    ngOnInit(): void {
        this.loadUser();
        this.loadRoles();
    }

    private loadUser(): void {
        this._userService.getUser(this.userId).subscribe((user) => {
            this.user = user;
            this.loadCurrentRole(); /*плохо работает*/
        });
    }

    private loadRoles(): void {
        this._roleService.getAllRoles().subscribe((roles) => {
            this.roles = roles;
        });
    }

    private loadCurrentRole(): void {
        if (this.user && this.roles.length > 0) {
            const userRole = this.roles.find(
                (role) => role.name === this.user.roleName /**/
            );
            if (userRole) {
                this.changeRoleForm.patchValue({
                    roleId: userRole.id,
                });
            }
        }
    }

    public changeUserRole(): void {
        if (this.changeRoleForm.valid) {
            const userData: UserChangeRole = this.changeRoleForm.value;
            userData.userId = this.userId;
            this._userService.changeUserRole(userData).subscribe({
                next: () => {
                    this._notifyService.showSuccessNotification(
                        'Role updated successfully'
                    );
                    this.close.emit();
                },
                error: (error) => {
                    this._notifyService.showUnexpectedError();
                },
            });
        }
    }

    public closeModal(): void {
        this.close.emit();
    }
}
