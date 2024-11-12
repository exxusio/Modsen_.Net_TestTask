import { HttpParams } from '@angular/common/http';
import { EventFilter } from '../data/models/events/event-filter';

export class HttpHelper {
    static AddPageSizeToQuery(httpParams: HttpParams, pageSize: number) {
        httpParams = httpParams.append('pageSize', pageSize.toString());

        return httpParams;
    }

    static AddEventFilterToQuery(httpParams: HttpParams, filter: EventFilter) {
        httpParams = httpParams.append('eventName', filter.eventName);
        httpParams = httpParams.append('fromDate', filter.fromDate);
        httpParams = httpParams.append('toDate', filter.toDate);
        httpParams = httpParams.append('fromTime', filter.fromTime);
        httpParams = httpParams.append('toTime', filter.toTime);
        httpParams = httpParams.append('location', filter.location);
        httpParams = httpParams.append('categoryId', filter.categoryId);

        return httpParams;
    }
}
