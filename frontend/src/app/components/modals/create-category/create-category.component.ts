import { NgIf } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';
import {
    FormControl,
    FormGroup,
    ReactiveFormsModule,
    Validators,
} from '@angular/forms';
import { CategoryCreate } from './../../../data/models/categories/category-create';
import { CategoryService } from '../../../data/services/category.service';
import { NotificationService } from '../../../data/services/notification.service';

@Component({
    selector: 'app-create-category',
    standalone: true,
    imports: [NgIf, ReactiveFormsModule],
    templateUrl: './create-category.component.html',
    styleUrl: './create-category.component.css',
})
export class CreateCategoryComponent {
    public createCategoryForm: FormGroup = new FormGroup({
        name: new FormControl<string>('', [
            Validators.required,
            Validators.minLength(3),
            Validators.maxLength(30),
        ]),
    });

    public name() {
        return this.createCategoryForm.controls['name'];
    }

    @Output() close = new EventEmitter<void>();
    @Output() updateData = new EventEmitter<string>();

    constructor(
        private readonly _categoryService: CategoryService,
        private readonly _notifyService: NotificationService
    ) {}

    public createCategory(): void {
        if (this.createCategoryForm.valid) {
            const category: CategoryCreate = this.createCategoryForm.value;
            this._categoryService.createCategory(category).subscribe({
                next: (category) => {
                    this.close.emit();
                    this.updateData.emit(category.id);
                    this._notifyService.showSuccessNotification(
                        'Category successfully created'
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

    public closeModal(): void {
        this.close.emit();
    }
}
