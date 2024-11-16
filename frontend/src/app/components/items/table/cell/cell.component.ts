import { CommonModule, DatePipe, NgFor, NgIf } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { TimePipe } from '../../../../data/pipes/time.pipe';

@Component({
    selector: 'app-cell',
    standalone: true,
    imports: [NgIf, NgFor, CommonModule, DatePipe, TimePipe],
    templateUrl: './cell.component.html',
    styleUrl: './cell.component.css',
    host: { '(click)': 'onClick()' },
})
export class CellComponent {
    @Input() data: any;
    @Input() column!: string;
    @Output() clicked = new EventEmitter<any>();

    public getMultipleColumns(column: string): string[] {
        const match = column.match(/^\[(.*)\]$/);

        if (match) {
            return match[1].split(',').map((item) => item.trim());
        }

        return [];
    }

    public isMultipleColumns(column: string): boolean {
        const multipleColumns = this.getMultipleColumns(column);
        return multipleColumns.length > 1;
    }

    public isDateColumn(column: string): boolean {
        const value = this.getNestedValue(this.data, column);
        return value instanceof Date || !isNaN(Date.parse(value));
    }

    public isTimeColumn(column: string): boolean {
        const value = this.getNestedValue(this.data, column);
        const date = new Date(`2001-01-01T${value}`);
        return !isNaN(date.getTime()) && date.toLocaleTimeString() === value;
    }

    public getNestedValue(obj: any, path: string): any {
        if (!path) return obj;
        const pathArray = path.split('.');
        return pathArray.reduce((acc, part) => acc && acc[part], obj);
    }

    public onClick(): void {
        this.clicked.emit(this.data);
    }
}
