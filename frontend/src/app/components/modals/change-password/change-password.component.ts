import {
    FormControl,
    FormGroup,
    ReactiveFormsModule,
    Validators,
} from '@angular/forms';
import { NgIf } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';
import { UserChangePassword } from '../../../data/models/users/user-change-password';
import { CustomValidators } from '../../../helpers/custom-validators';
import { UserService } from '../../../data/services/user.service';
import { NotificationService } from '../../../data/services/notification.service';

@Component({
    selector: 'app-change-password',
    standalone: true,
    imports: [NgIf, ReactiveFormsModule],
    templateUrl: './change-password.component.html',
    styleUrl: './change-password.component.css',
})
export class ChangePasswordComponent {
    public changePasswordForm: FormGroup = new FormGroup({
        password: new FormControl<string>('', [
            Validators.required,
            Validators.minLength(8),
            Validators.maxLength(20),
        ]),
        confirmPassword: new FormControl<string>('', [
            Validators.required,
            CustomValidators.equalTo('password'),
        ]),
    });

    public password() {
        return this.changePasswordForm.controls['password'];
    }

    public confirmPassword() {
        return this.changePasswordForm.controls['confirmPassword'];
    }

    @Output() close = new EventEmitter<void>();

    constructor(
        private readonly _userService: UserService,
        private readonly _notifyService: NotificationService
    ) {}

    public changePassword(): void {
        if (this.changePasswordForm.valid) {
            const passwordData: UserChangePassword =
                this.changePasswordForm.value;
            this._userService.changePassword(passwordData).subscribe({
                next: () => {
                    this.close.emit();
                    this._notifyService.showSuccessNotification(
                        'Password updated successfully'
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

    public closeModal(): void {
        this.close.emit();
    }
}
