import {
    FormControl,
    FormGroup,
    ReactiveFormsModule,
    Validators,
} from '@angular/forms';
import { NgFor, NgIf } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';
import { CreateCategoryComponent } from '../create-category/create-category.component';
import { CategoryRead } from './../../../data/models/categories/category-read';
import { CategoryService } from './../../../data/services/category.service';
import { EventCreate } from '../../../data/models/events/event-create';
import { EventService } from '../../../data/services/event.service';
import { CustomValidators } from '../../../helpers/custom-validators';
import { NotificationService } from '../../../data/services/notification.service';

@Component({
    selector: 'app-create-event',
    standalone: true,
    imports: [NgFor, NgIf, ReactiveFormsModule, CreateCategoryComponent],
    templateUrl: './create-event.component.html',
    styleUrl: './create-event.component.css',
})
export class CreateEventComponent {
    public categories: CategoryRead[] = [];

    public createEventForm: FormGroup = new FormGroup({
        name: new FormControl<string>('', [
            Validators.required,
            Validators.minLength(3),
            Validators.maxLength(100),
        ]),
        description: new FormControl<string>('', [Validators.maxLength(3000)]),
        date: new FormControl<string>('', [
            Validators.required,
            CustomValidators.futureDateValidator(),
        ]),
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

    public name() {
        return this.createEventForm.controls['name'];
    }

    public description() {
        return this.createEventForm.controls['description'];
    }

    public date() {
        return this.createEventForm.controls['date'];
    }

    public time() {
        return this.createEventForm.controls['time'];
    }

    public location() {
        return this.createEventForm.controls['location'];
    }

    public imageUrl() {
        return this.createEventForm.controls['imageUrl'];
    }

    public maxParticipants() {
        return this.createEventForm.controls['maxParticipants'];
    }

    public categoryId() {
        return this.createEventForm.controls['categoryId'];
    }

    @Output() close = new EventEmitter<void>();
    @Output() updateData = new EventEmitter<void>();
    public isNewCategory = false;

    constructor(
        private readonly _eventService: EventService,
        private readonly _categoryService: CategoryService,
        private readonly _notifyService: NotificationService
    ) {}

    ngOnInit(): void {
        this.loadCategory();
    }

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

    public createEvent(): void {
        if (this.createEventForm.valid) {
            const event: EventCreate = this.createEventForm.value;
            event.time = event.time + ':00';
            this._eventService.createEvent(event).subscribe({
                next: () => {
                    this.close.emit();
                    this.updateData.emit();
                    this._notifyService.showSuccessNotification(
                        'Event successfully created'
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
            this.createEventForm.get('categoryId')?.setValue('');
        }
    }

    public updateCategory(categorId: string): void {
        this.loadCategory();
        this.createEventForm.get('categoryId')?.setValue(categorId);
    }

    public closeModal(): void {
        this.close.emit();
    }
}
