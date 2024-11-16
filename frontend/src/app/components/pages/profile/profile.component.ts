import {
    FormControl,
    FormGroup,
    ReactiveFormsModule,
    Validators,
} from '@angular/forms';
import { Component } from '@angular/core';
import { NgClass, NgIf } from '@angular/common';
import { ChangePasswordComponent } from '../../modals/change-password/change-password.component';
import { NotificationService } from '../../../data/services/notification.service';
import { UserDetailed } from '../../../data/models/users/user-detailed';
import { CustomValidators } from '../../../helpers/custom-validators';
import { UserUpdate } from '../../../data/models/users/user-update';
import { UserService } from '../../../data/services/user.service';
import { DataHelper } from '../../../helpers/data-helper';

@Component({
    selector: 'app-profile',
    standalone: true,
    imports: [NgIf, NgClass, ReactiveFormsModule, ChangePasswordComponent],
    templateUrl: './profile.component.html',
    styleUrl: './profile.component.css',
})
export class ProfileComponent {
    public user!: UserDetailed;
    public profileForm: FormGroup = new FormGroup({
        firstName: new FormControl<string>('', [
            Validators.required,
            Validators.minLength(3),
            Validators.maxLength(50),
        ]),
        lastName: new FormControl<string>('', [
            Validators.required,
            Validators.minLength(3),
            Validators.maxLength(50),
        ]),
        dateOfBirth: new FormControl<string>('', [
            CustomValidators.dateValidator(),
        ]),
        email: new FormControl<string>('', [
            Validators.required,
            Validators.email,
            Validators.maxLength(150),
        ]),
    });
    public isPasswordChangeModalVisible = false;
    public isEditing: boolean = false;

    public firstName() {
        return this.profileForm.controls['firstName'];
    }

    public lastName() {
        return this.profileForm.controls['lastName'];
    }

    public dateOfBirth() {
        return this.profileForm.controls['dateOfBirth'];
    }

    public email() {
        return this.profileForm.controls['email'];
    }

    constructor(
        private readonly _userService: UserService,
        private readonly _notifyService: NotificationService
    ) {}

    ngOnInit(): void {
        this.loadUserProfile();
    }

    private loadUserProfile(): void {
        this._userService.getCurrentUser().subscribe({
            next: (user) => {
                this.user = user;
                this.user.dateOfBirth = DataHelper.formatDateToDisplay(
                    user.dateOfBirth
                );
                this.profileForm.patchValue({
                    firstName: user.firstName,
                    lastName: user.lastName,
                    dateOfBirth: this.user.dateOfBirth,
                    email: user.email,
                });
            },
            error: (err) => {
                this._notifyService.showErrorNotification(
                    'Failed to load user data. Please try again later'
                );
            },
        });
    }

    public updateProfile(): void {
        if (this.profileForm.valid) {
            const updatedUser: UserUpdate = this.profileForm.value;
            updatedUser.dateOfBirth = DataHelper.formatDateForServer(
                updatedUser.dateOfBirth
            );

            this._userService.updateUser(updatedUser).subscribe({
                next: () => {
                    this.loadUserProfile();
                    this._notifyService.showInfoNotification(
                        'Data updated successfully'
                    );
                },
                error: (error) => {
                    if (error?.error?.Message) {
                        this._notifyService.showErrorNotification(
                            error.error.Message
                        );
                    } else {
                        this._notifyService.showUnexpectedError();
                    }
                },
            });
        }
    }

    public toggleChangePasswordModal(): void {
        this.isPasswordChangeModalVisible = !this.isPasswordChangeModalVisible;
    }

    public toggleEditing(): void {
        this.isEditing = !this.isEditing;
    }

    public resetUserData(): void {
        this.toggleEditing();
        this.profileForm.patchValue({
            firstName: this.user.firstName,
            lastName: this.user.lastName,
            dateOfBirth: this.user.dateOfBirth,
            email: this.user.email,
        });
    }
}
