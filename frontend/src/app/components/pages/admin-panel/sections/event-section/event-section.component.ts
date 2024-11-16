import { Router } from '@angular/router';
import { Component } from '@angular/core';
import { NgFor, NgIf } from '@angular/common';
import { CreateEventComponent } from '../../../../modals/create-event/create-event.component';
import { KeyByValueHelper } from '../../../../../helpers/key-by-value-hepler';
import { EventService } from '../../../../../data/services/event.service';
import { EventRead } from '../../../../../data/models/events/event-read';
import { RowComponent } from '../../../../items/table/row/row.component';
import { SearchComponent } from '../../search/search.component';
import { NotificationService } from '../../../../../data/services/notification.service';

@Component({
    selector: 'app-event-section',
    standalone: true,
    imports: [NgFor, NgIf, RowComponent, CreateEventComponent, SearchComponent],
    templateUrl: './event-section.component.html',
    styleUrl: './event-section.component.css',
})
export class EventSectionComponent {
    private _events: EventRead[] = [];
    public readonly searchField: string = 'name';

    public get getEvents(): EventRead[] {
        return this._events;
    }

    public events: EventRead[] = [];
    public columns: string[] = [];
    public isCreate = false;

    constructor(
        private readonly _router: Router,
        private readonly _eventService: EventService,
        private readonly _notifyService: NotificationService
    ) {}

    public ngOnInit(): void {
        this.loadEvents();
    }

    public toggleModal() {
        this.isCreate = !this.isCreate;
    }

    public loadEvents(): void {
        this._eventService.getAllEvents().subscribe({
            next: (events) => {
                this._events = events;
                this.events = this._events;
                this.loadDataColumns();
            },
            error: (error) => {
                this._notifyService.showUnexpectedError();
            },
        });
    }

    private loadDataColumns() {
        if (this.events.length > 0) {
            this.columns = [
                KeyByValueHelper.getKeyByValue(
                    this.events[0],
                    this.events[0].name
                ),
                KeyByValueHelper.getKeyByValue(
                    this.events[0],
                    this.events[0].date
                ),
                KeyByValueHelper.getKeyByValue(
                    this.events[0],
                    this.events[0].location
                ),
                KeyByValueHelper.getKeyByNestedValue(
                    this.events[0],
                    this.events[0].category,
                    this.events[0].category.name
                ),
            ];
        }
    }

    public onEventClicked(data: any): void {
        this._router.navigate([`/events/${data['id']}`]);
    }
}
