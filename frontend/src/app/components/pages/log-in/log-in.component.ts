import {
    FormControl,
    FormGroup,
    ReactiveFormsModule,
    Validators,
} from '@angular/forms';
import { NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { UserService } from '../../../data/services/user.service';
import { NotificationService } from '../../../data/services/notification.service';

@Component({
    selector: 'app-log-in',
    standalone: true,
    imports: [ReactiveFormsModule, RouterModule, NgIf],
    templateUrl: './log-in.component.html',
    styleUrl: './log-in.component.css',
})
export class LogInComponent {
    constructor(
        private readonly _router: Router,
        private readonly _userService: UserService,
        private readonly _notifyService: NotificationService
    ) {}

    public Form: FormGroup = new FormGroup({
        login: new FormControl<string>('', [Validators.required]),
        password: new FormControl<string>('', [Validators.required]),
    });

    public login() {
        return this.Form.controls['login'];
    }

    public password() {
        return this.Form.controls['password'];
    }

    public onSubmit(): void {
        if (!this.Form.valid) {
            return;
        }

        this._userService.login(this.Form.value).subscribe({
            next: () => {
                this._notifyService.showInfoNotification(
                    'You have successfully logged into your account'
                );
                this._router.navigateByUrl('/');
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
