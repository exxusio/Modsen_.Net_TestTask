import { Component } from '@angular/core';
import { NgFor, NgIf } from '@angular/common';
import { DataHelper } from '../../../../../helpers/data-helper';
import { SearchComponent } from '../../search/search.component';
import { RowComponent } from '../../../../items/table/row/row.component';
import { KeyByValueHelper } from '../../../../../helpers/key-by-value-hepler';
import { RegistrationService } from '../../../../../data/services/registration.service';
import { RegistrationRead } from '../../../../../data/models/registrations/registration-read';
import { RegistrationInfoComponent } from '../../../../modals/registration-info/registration-info.component';
import { NotificationService } from '../../../../../data/services/notification.service';

@Component({
    selector: 'app-registration-section',
    standalone: true,
    imports: [
        RowComponent,
        NgFor,
        NgIf,
        SearchComponent,
        RegistrationInfoComponent,
    ],
    templateUrl: './registration-section.component.html',
    styleUrl: './registration-section.component.css',
})
export class RegistrationSectionComponent {
    private _registrations: RegistrationRead[] = [];
    public readonly searchField: string = 'registrationDate';

    public get getRegistrations(): RegistrationRead[] {
        return this._registrations.map((registration) => ({
            ...registration,
            registrationDate: DataHelper.formatDateToDisplay(
                registration.registrationDate
            ),
        }));
    }

    public registrations: RegistrationRead[] = [];
    public columns: string[] = [];
    public clickRegistration?: RegistrationRead;

    constructor(
        private readonly _registrationService: RegistrationService,
        private readonly _notifyService: NotificationService
    ) {}

    public ngOnInit(): void {
        this.loadRegistrations();
    }

    public loadRegistrations(): void {
        this._registrationService.getAllRegistrations().subscribe({
            next: (registrations) => {
                this._registrations = registrations;
                this.registrations = this._registrations;
                this.loadDataColumns();
            },
            error: (error) => {
                this._notifyService.showUnexpectedError();
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
                    this.registrations[0].participant,
                    this.registrations[0].participant.email
                ),
                KeyByValueHelper.getKeyByNestedValue(
                    this.registrations[0],
                    this.registrations[0].event,
                    this.registrations[0].event.name
                ),
            ];
        }
    }

    public searchRegistrations(registrations: RegistrationRead[]) {
        this.registrations = registrations.map((registration) => ({
            ...registration,
            registrationDate: DataHelper.formatDateForServer(
                registration.registrationDate
            ),
        }));
    }

    public onRegistrationClicked(data: any): void {
        this.clickRegistration = data;
    }
}
