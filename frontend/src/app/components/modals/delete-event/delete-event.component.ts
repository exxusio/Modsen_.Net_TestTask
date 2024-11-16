import { Router } from '@angular/router';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { EventService } from '../../../data/services/event.service';
import { NotificationService } from '../../../data/services/notification.service';

@Component({
    selector: 'app-delete-event',
    standalone: true,
    imports: [],
    templateUrl: './delete-event.component.html',
    styleUrl: './delete-event.component.css',
})
export class DeleteEventComponent {
    @Input() eventId!: string | null;
    @Output() close = new EventEmitter<void>();

    constructor(
        private readonly _eventService: EventService,
        private readonly _router: Router,
        private readonly _notifyService: NotificationService
    ) {}

    public confirm() {
        if (this.eventId) {
            this._eventService.deleteEvent(this.eventId).subscribe({
                next: () => {
                    this._notifyService.showSuccessNotification(
                        'Event successfully deleted '
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

    public cancel() {
        this.close.emit();
    }
}
