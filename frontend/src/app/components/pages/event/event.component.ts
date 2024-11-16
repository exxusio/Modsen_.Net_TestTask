import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DatePipe, NgClass, NgIf } from '@angular/common';
import { TimePipe } from '../../../data/pipes/time.pipe';
import { EventConfig } from '../../../data/configs/event-config';
import { UserService } from '../../../data/services/user.service';
import { EventRead } from '../../../data/models/events/event-read';
import { EventService } from '../../../data/services/event.service';
import { NotificationService } from '../../../data/services/notification.service';
import { RegistrationService } from './../../../data/services/registration.service';
import { DeleteEventComponent } from '../../modals/delete-event/delete-event.component';
import { UpdateEventComponent } from '../../modals/update-event/update-event.component';
import { EventRegistrationComponent } from '../../modals/event-registration/event-registration.component';

@Component({
    selector: 'app-event',
    standalone: true,
    imports: [
        NgIf,
        NgClass,
        DatePipe,
        TimePipe,
        DeleteEventComponent,
        UpdateEventComponent,
        EventRegistrationComponent,
    ],
    templateUrl: './event.component.html',
    styleUrl: './event.component.css',
})
export class EventComponent {
    public defaultImage = EventConfig.DefaultImageUrl;

    public eventId: string | null = null;
    public event!: EventRead;

    public isRegistered: boolean = false;

    public isCollapsed = true;
    public descriptionLimit = 900;

    public isAdminStatus: boolean | null = null;
    public isMore = false;
    public isView = false;
    public isDelete = false;
    public isEdit = false;

    get truncatedDescription() {
        if (this.event.description.length == 0) {
            return 'Description is missing';
        }
        if (
            this.event.description.length > this.descriptionLimit &&
            this.isCollapsed
        ) {
            return (
                this.event.description.substring(0, this.descriptionLimit) +
                '...'
            );
        } else {
            return this.event.description;
        }
    }

    constructor(
        private readonly _router: Router,
        private readonly _route: ActivatedRoute,
        private readonly _userService: UserService,
        private readonly _eventService: EventService,
        private readonly _registrationService: RegistrationService,
        private readonly _notifyService: NotificationService
    ) {}

    ngOnInit(): void {
        this._userService.isAdmin().subscribe((isAdmin) => {
            this.isAdminStatus = isAdmin;
        });

        this._route.paramMap.subscribe((params) => {
            this.eventId = params.get('eventId');

            if (this.eventId) {
                this.loadEventDetails(this.eventId);
            }
        });
    }

    public isEventInPast(): boolean {
        const dateString = `${this.event.date.split('T')[0]}T${
            this.event.time
        }`;
        const eventDateTime = new Date(dateString);

        const currentDateTime = new Date();

        return eventDateTime < currentDateTime;
    }

    getEventStatusText(): string {
        const currentDate = new Date(new Date().setHours(0, 0, 0, 0));

        if (currentDate > new Date(this.event.date)) {
            return 'Event has already ended';
        } else {
            return 'Event will start soon';
        }
    }

    public isEventEdit(): void {
        this.isEdit = !this.isEdit;
        this.isMore = false;
    }

    public isEventDelete(): void {
        this.isDelete = !this.isDelete;
        this.isMore = false;
    }

    public isViewUsers(): void {
        this.isView = !this.isView;
        this.isMore = false;
    }

    public toggleDescription(): void {
        this.isCollapsed = !this.isCollapsed;
    }

    public loadEventDetails(eventId: string): void {
        this._eventService.getEventById(eventId).subscribe({
            next: (event) => {
                this.event = event;
                this.checkRegistrationStatus(eventId);
            },
            error: (err) => {
                if (err?.error?.Status == 404) {
                    this._router.navigate(['404']);
                } else {
                    this._notifyService.showErrorNotification(
                        'Failed to load event data. Please try again later'
                    );
                    this._router.navigate(['/']);
                }
            },
        });
    }

    private checkRegistrationStatus(eventId: string): void {
        if (!this._userService.isAuth()) {
            return;
        }

        this._registrationService
            .getUserRegistrations() /**/
            .subscribe((registrations) => {
                this.isRegistered = registrations.some(
                    (reg) => reg.event.id === eventId
                );
            });
    }

    public registerForEvent(): void {
        if (!this._userService.isAuth()) {
            this._router.navigateByUrl('sign-up');
            return;
        }

        if (this.eventId) {
            this._registrationService.registerForEvent(this.eventId).subscribe({
                next: () => {
                    this.updateEventDate(true);
                    this._notifyService.showSuccessNotification(
                        'You have successfully registered'
                    );
                },
                error: (err) => {
                    this._notifyService.showUnexpectedError();
                },
            });
        }
    }

    public unregisterFromEvent(): void {
        if (!this._userService.isAuth()) {
            this._router.navigateByUrl('sign-up');
            return;
        }

        if (this.eventId) {
            this._registrationService
                .unregisterFromEvent(this.eventId)
                .subscribe({
                    next: () => {
                        this.updateEventDate(false);
                        this._notifyService.showSuccessNotification(
                            'You have successfully cancelled your registration'
                        );
                    },
                    error: (err) => {
                        this._notifyService.showUnexpectedError();
                    },
                });
        }
    }

    private updateEventDate(isRegistered: boolean): void {
        this.isRegistered = isRegistered;
        this.event.registeredCount += this.isRegistered ? 1 : -1;
        this.event.hasAvailableSeats =
            this.event.registeredCount < this.event.maxParticipants;
    }
}
