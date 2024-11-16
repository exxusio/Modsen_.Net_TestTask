import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/internal/Subject';
import { EventFilter } from '../models/events/event-filter';

@Injectable({
    providedIn: 'root',
})
export class FilterService {
    private currentFilter: EventFilter = {
        eventName: '',
        fromDate: '',
        toDate: '',
        fromTime: '',
        toTime: '',
        location: '',
        categoryId: '',
    };

    private filterChangedSource = new Subject<EventFilter>();
    filterChanged$ = this.filterChangedSource.asObservable();

    public emitFilterChanged(filter: EventFilter) {
        const cleanedFilter = this.cleanFilter(filter);
        cleanedFilter.eventName = this.currentFilter.eventName;

        this.currentFilter = { ...this.currentFilter, ...cleanedFilter };

        this.filterChangedSource.next(this.currentFilter);
    }

    public emitFilterNameChanged(name: string) {
        this.currentFilter.eventName = name || '';
        this.filterChangedSource.next(this.currentFilter);
    }

    private cleanFilter(filter: EventFilter): EventFilter {
        const cleanedFilter: EventFilter = { ...filter };

        Object.keys(cleanedFilter).forEach((key) => {
            if (cleanedFilter[key] == null || cleanedFilter[key] == undefined) {
                cleanedFilter[key] = '';
            }
        });

        return cleanedFilter;
    }
}
