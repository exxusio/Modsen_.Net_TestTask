import { EventRead } from './event-read';

export interface EventsByFilter {
    events: EventRead[];
    totalCount: number;
}
