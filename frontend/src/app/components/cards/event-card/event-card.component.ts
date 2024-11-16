import { DatePipe, NgClass } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { EventRead } from '../../../data/models/events/event-read';
import { EventConfig } from '../../../data/configs/event-config';
import { TimePipe } from '../../../data/pipes/time.pipe';

@Component({
    selector: 'app-event-card',
    standalone: true,
    imports: [NgClass, TimePipe, DatePipe],
    templateUrl: './event-card.component.html',
    styleUrl: './event-card.component.css',
    host: {
        '[class.no-seats]': '!event.hasAvailableSeats',
        '(click)': 'onClickEvent()',
    },
})
export class EventCardComponent {
    @Input() event!: EventRead;
    @Output() eventClicked = new EventEmitter<string>();
    public defaultImage = EventConfig.DefaultImageUrl;

    public onClickEvent(): void {
        this.eventClicked.emit(this.event.id);
    }
}
