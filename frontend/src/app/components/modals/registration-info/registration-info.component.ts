import { Component, EventEmitter, Input, Output } from '@angular/core';
import { RegistrationRead } from '../../../data/models/registrations/registration-read';
import { Router } from '@angular/router';
import { ChangeRoleComponent } from '../change-role/change-role.component';
import { ChangePasswordComponent } from '../change-password/change-password.component';
import { NgIf } from '@angular/common';

@Component({
    selector: 'app-registration-info',
    standalone: true,
    imports: [ChangeRoleComponent, ChangePasswordComponent, NgIf],
    templateUrl: './registration-info.component.html',
    styleUrl: './registration-info.component.css',
})
export class RegistrationInfoComponent {
    @Input() registration?: RegistrationRead;
    @Output() close = new EventEmitter<void>();
    public isUser = false;
    public currentUserId!: string;

    constructor(private readonly _router: Router) {}

    public viewEvent() {
        this._router.navigate([`/events/${this.registration?.event.id}`]);
        this.close.emit();
    }

    public viewUser() {
        this.isUser = true;
        if (this.registration?.participant.id) {
            this.currentUserId = this.registration.participant.id;
        }
    }

    public cancel() {
        this.close.emit();
    }
}
