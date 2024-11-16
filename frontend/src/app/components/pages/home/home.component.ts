import { NgFor, NgIf } from '@angular/common';
import { Router } from '@angular/router';
import { Component } from '@angular/core';
import { EventCardComponent } from '../../cards/event-card/event-card.component';
import { PaginationComponent } from '../../items/pagination/pagination.component';
import { EventsByFilter } from '../../../data/models/events/event-by-filter';
import { EventFilter } from '../../../data/models/events/event-filter';
import { FilterService } from '../../../data/services/filter.service';
import { EventService } from '../../../data/services/event.service';
import { NotificationService } from '../../../data/services/notification.service';

@Component({
    selector: 'app-home',
    standalone: true,
    imports: [NgIf, NgFor, EventCardComponent, PaginationComponent],
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css'],
})
export class HomeComponent {
    public eventsByFilter!: EventsByFilter;
    public currentPage: number = 1;
    public totalPages: number = 1;
    private readonly pageSize: number = 14;

    public filter: EventFilter = {
        eventName: '',
        fromDate: '',
        toDate: '',
        fromTime: '',
        toTime: '',
        location: '',
        categoryId: '',
    };

    constructor(
        private readonly _router: Router,
        private readonly _filterService: FilterService,
        private readonly _eventService: EventService,
        private readonly _notifyService: NotificationService
    ) {
        this._filterService.filterChanged$.subscribe((filter) => {
            this.onFilterChanged(filter);
        });
    }

    public ngOnInit(): void {
        this.loadEvents();
    }

    private loadEvents(): void {
        this._eventService
            .getEventsByFilter(this.filter, this.currentPage, this.pageSize)
            .subscribe({
                next: (response) => {
                    this.eventsByFilter = response;
                    this.totalPages = Math.ceil(
                        this.eventsByFilter.totalCount / this.pageSize
                    );
                },
                error: (err) => {
                    this._notifyService.showErrorNotification(
                        'Failed to load events data. Please try again later'
                    );
                },
            });
    }

    public onFilterChanged(filter: EventFilter): void {
        this.filter = filter;
        this.currentPage = 1;
        this.loadEvents();
    }

    public onEventClicked(eventId: string): void {
        this._router.navigate([`/events/${eventId}`]);
    }

    public onPageChange(page: number): void {
        this.currentPage = page;
        this.loadEvents();
    }
}
