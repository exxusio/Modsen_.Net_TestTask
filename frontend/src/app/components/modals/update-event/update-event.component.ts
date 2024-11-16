import {
    FormControl,
    FormGroup,
    ReactiveFormsModule,
    Validators,
} from '@angular/forms';
import { DatePipe, NgFor, NgIf } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CreateCategoryComponent } from '../create-category/create-category.component';
import { CategoryRead } from '../../../data/models/categories/category-read';
import { EventUpdate } from '../../../data/models/events/event-update';
import { EventService } from '../../../data/services/event.service';
import { EventRead } from '../../../data/models/events/event-read';
import { CategoryService } from '../../../data/services/category.service';
import { NotificationService } from '../../../data/services/notification.service';

@Component({
    selector: 'app-update-event',
    standalone: true,
    imports: [NgFor, NgIf, ReactiveFormsModule, CreateCategoryComponent],
    templateUrl: './update-event.component.html',
    styleUrl: './update-event.component.css',
    providers: [DatePipe],
})
export class UpdateEventComponent {
    public categories: CategoryRead[] = [];

    @Input() event!: EventRead;
    @Output() close = new EventEmitter<void>();
    @Output() updateData = new EventEmitter<string>();

    public updateEventForm: FormGroup = new FormGroup({
        name: new FormControl<string>('', [
            Validators.required,
            Validators.minLength(3),
            Validators.maxLength(100),
        ]),
        description: new FormControl<string>('', [Validators.maxLength(3000)]),
        date: new FormControl<string>('', [Validators.required]),
        time: new FormControl<string>('', [Validators.required]),
        location: new FormControl<string>('', [
            Validators.required,
            Validators.minLength(3),
            Validators.maxLength(200),
        ]),
        imageUrl: new FormControl<string>('', []),
        maxParticipants: new FormControl<string>('', [
            Validators.required,
            Validators.min(1),
        ]),
        categoryId: new FormControl<string>('', [Validators.required]),
    });

    public isNewCategory = false;

    public name() {
        return this.updateEventForm.controls['name'];
    }

    public description() {
        return this.updateEventForm.controls['description'];
    }

    public date() {
        return this.updateEventForm.controls['date'];
    }

    public time() {
        return this.updateEventForm.controls['time'];
    }

    public location() {
        return this.updateEventForm.controls['location'];
    }

    public imageUrl() {
        return this.updateEventForm.controls['imageUrl'];
    }

    public maxParticipants() {
        return this.updateEventForm.controls['maxParticipants'];
    }

    public categoryId() {
        return this.updateEventForm.controls['categoryId'];
    }

    constructor(
        private readonly _eventService: EventService,
        private readonly _categoryService: CategoryService,
        private readonly _datePipe: DatePipe,
        private readonly _notifyService: NotificationService
    ) {}

    public loadCategory(): void {
        this._categoryService.getAllCategories().subscribe({
            next: (categories) => {
                this.categories = categories;
            },
            error: (error) => {
                this._notifyService.showUnexpectedError();
            },
        });
    }

    ngOnInit(): void {
        this.loadCategory();

        this.updateEventForm.patchValue({
            name: this.event.name,
            description: this.event.description,
            date: this._datePipe.transform(this.event.date, 'yyyy-MM-dd'),
            time: this.event.time,
            location: this.event.location,
            imageUrl: this.event.imageUrl,
            maxParticipants: this.event.maxParticipants,
            categoryId: this.event.category.id,
        });
    }

    public updateEvent(): void {
        if (this.updateEventForm.valid) {
            const updatedEvent: EventUpdate = this.updateEventForm.value;

            if (updatedEvent.time != this.event.time) {
                updatedEvent.time = updatedEvent.time + ':00';
            }

            this._eventService
                .updateEvent(this.event.id, updatedEvent)
                .subscribe({
                    next: (event) => {
                        this.updateData.emit(event.id);
                        this.close.emit();
                        this._notifyService.showSuccessNotification(
                            'Event successfully updated'
                        );
                    },
                    error: (error) => {
                        if (error?.error?.Message) {
                            this._notifyService.showErrorNotification(
                                error.error.Message
                            );
                        } else {
                            this._notifyService.showUnexpectedError();
                        }
                    },
                });
        }
    }

    public newCategory(event: Event): void {
        const selectElement = event.target as HTMLSelectElement;
        const selectedValue = selectElement.value;

        if (selectedValue === 'new') {
            this.isNewCategory = !this.isNewCategory;
        }
    }

    public updateCategory(categorId: string): void {
        this.loadCategory();
        this.updateEventForm.get('categoryId')?.setValue(categorId);
    }

    public cancel() {
        this.close.emit();
    }
}
