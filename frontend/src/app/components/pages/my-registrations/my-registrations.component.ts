import { NgFor } from '@angular/common';
import { Router } from '@angular/router';
import { Component } from '@angular/core';
import { RegistrationRead } from '../../../data/models/registrations/registration-read';
import { RegistrationService } from '../../../data/services/registration.service';
import { KeyByValueHelper } from '../../../helpers/key-by-value-hepler';
import { RowComponent } from '../../items/table/row/row.component';
import { NotificationService } from '../../../data/services/notification.service';

@Component({
    selector: 'app-my-registrations',
    standalone: true,
    imports: [NgFor, RowComponent],
    templateUrl: './my-registrations.component.html',
    styleUrl: './my-registrations.component.css',
})
export class MyRegistrationsComponent {
    public registrations: RegistrationRead[] = [];
    public columns: string[] = [];

    constructor(
        private readonly _router: Router,
        private readonly _registrationService: RegistrationService,
        private readonly _notifyService: NotificationService
    ) {}

    public ngOnInit(): void {
        this.loadRegistration();
    }

    private loadRegistration(): void {
        this._registrationService.getUserRegistrations().subscribe({
            /**/
            next: (registrations) => {
                this.registrations = registrations;
                this.loadDataColumns();
            },
            error: (err) => {
                this._notifyService.showErrorNotification(
                    'Failed to load registrations. Please try again later'
                );
            },
        });
    }

    private loadDataColumns() {
        if (this.registrations.length > 0) {
            this.columns = [
                KeyByValueHelper.getKeyByValue(
                    this.registrations[0],
                    this.registrations[0].registrationDate
                ),
                KeyByValueHelper.getKeyByNestedValue(
                    this.registrations[0],
                    this.registrations[0].event,
                    this.registrations[0].event.name
                ),
                `[${KeyByValueHelper.getKeyByNestedValue(
                    this.registrations[0],
                    this.registrations[0].event,
                    this.registrations[0].event.date
                )}, ${KeyByValueHelper.getKeyByNestedValue(
                    this.registrations[0],
                    this.registrations[0].event,
                    this.registrations[0].event.time
                )}]`,
            ];
        }
    }

    public onRegistrationClicked(data: any): void {
        this._router.navigate([`/events/${data['event']['id']}`]);
    }
}
