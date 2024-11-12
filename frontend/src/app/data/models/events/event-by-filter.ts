import { EventRead } from './event-read';

export interface EventByFilter {
    events: EventRead[];
    totalCount: number;
}
