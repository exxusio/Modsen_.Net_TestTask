import { EventRead } from '../events/event-read';
import { UserRead } from '../users/user-read';

export interface RegistrationRead {
    id: string;
    event: EventRead;
    participant: UserRead;
    registrationDate: string;
}
