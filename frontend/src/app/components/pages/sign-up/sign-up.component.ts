import {
    FormControl,
    FormGroup,
    Validators,
    ReactiveFormsModule,
} from '@angular/forms';
import { NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { UserService } from '../../../data/services/user.service';
import { CustomValidators } from '../../../helpers/custom-validators';
import { NotificationService } from '../../../data/services/notification.service';

@Component({
    selector: 'app-sign-up',
    standalone: true,
    imports: [ReactiveFormsModule, RouterModule, NgIf],
    templateUrl: './sign-up.component.html',
    styleUrls: ['./sign-up.component.css'],
})
export class SignUpComponent {
    constructor(
        private readonly _userService: UserService,
        private readonly _router: Router,
        private readonly _notifyService: NotificationService
    ) {}

    public Form: FormGroup = new FormGroup({
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
        email: new FormControl<string>('', [
            Validators.required,
            Validators.email,
            Validators.maxLength(150),
        ]),
        login: new FormControl<string>('', [
            Validators.required,
            Validators.minLength(6),
            Validators.maxLength(20),
        ]),
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

    public firstName() {
        return this.Form.controls['firstName'];
    }

    public lastName() {
        return this.Form.controls['lastName'];
    }

    public email() {
        return this.Form.controls['email'];
    }

    public login() {
        return this.Form.controls['login'];
    }

    public password() {
        return this.Form.controls['password'];
    }

    public confirmPassword() {
        return this.Form.controls['confirmPassword'];
    }

    public onSubmit(): void {
        if (!this.Form.valid) {
            return;
        }

        const userRegister = this.Form.value;

        this._userService.register(userRegister).subscribe({
            next: () => {
                this._notifyService.showInfoNotification(
                    'You have registered successfully! Now log into your account'
                );
                this._router.navigate(['/log-in']);
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
