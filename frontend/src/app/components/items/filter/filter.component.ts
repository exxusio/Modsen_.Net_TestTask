import { NgFor, NgIf } from '@angular/common';
import { Component, EventEmitter, Output, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CategoryRead } from './../../../data/models/categories/category-read';
import { CategoryService } from '../../../data/services/category.service';
import { EventFilter } from '../../../data/models/events/event-filter';
import { NotificationService } from '../../../data/services/notification.service';

@Component({
    selector: 'app-filter',
    standalone: true,
    imports: [NgFor, NgIf, ReactiveFormsModule],
    templateUrl: './filter.component.html',
    styleUrls: ['./filter.component.css'],
})
export class FilterComponent implements OnInit {
    @Output() filterChanged = new EventEmitter<EventFilter>();

    public filterForm: FormGroup;
    public categories: CategoryRead[] = [];
    public optionField = 'name';
    public valueField = 'id';
    public isFilterPanelVisible = false;

    constructor(
        private readonly _fb: FormBuilder,
        private readonly _categoryService: CategoryService,
        private readonly _notifyService: NotificationService
    ) {
        this.filterForm = this._fb.group({
            eventName: '',
            fromDate: '',
            toDate: '',
            fromTime: '',
            toTime: '',
            location: '',
            categoryId: '',
        });
    }

    ngOnInit(): void {
        this._categoryService.getAllCategories().subscribe({
            next: (categories) => {
                this.categories = categories;
            },
            error: (error) => {
                this._notifyService.showErrorNotification(
                    'Failed to load category data. Please try again later'
                );
            },
        });
    }

    public toggleFilterPanel(): void {
        this.isFilterPanelVisible = !this.isFilterPanelVisible;
    }

    public onSubmit(): void {
        const filter: EventFilter = this.filterForm.value;
        this.filterChanged.emit(filter);
        this.isFilterPanelVisible = false;
    }

    public onReset(): void {
        this.filterForm.reset({
            eventName: this.filterForm.value.eventName,
            fromDate: '',
            toDate: '',
            fromTime: '',
            toTime: '',
            location: '',
            categoryId: '',
        });
        this.onSubmit();
    }
}
