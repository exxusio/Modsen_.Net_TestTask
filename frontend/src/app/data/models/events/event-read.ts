import { CategoryRead } from '../categories/category-read';

export interface EventRead {
    id: string;
    name: string;
    description: string;
    date: string;
    time: string;
    location: string;
    imageUrl: string;
    maxParticipants: number;
    registeredCount: number;
    hasAvailableSeats: boolean;
    category: CategoryRead;
}
