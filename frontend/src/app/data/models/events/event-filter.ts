export interface EventFilter {
    eventName: string;
    fromDate: string;
    toDate: string;
    fromTime: string;
    toTime: string;
    location: string;
    categoryId: string;
    [key: string]: string | number | undefined;
}
