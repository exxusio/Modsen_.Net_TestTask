import { NgFor } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CellComponent } from '../cell/cell.component';

@Component({
    selector: 'app-row',
    standalone: true,
    imports: [NgFor, CellComponent],
    templateUrl: './row.component.html',
    styleUrl: './row.component.css',
})
export class RowComponent {
    @Input() data: any;
    @Input() columns: string[] = [];
    @Output() clicked = new EventEmitter<any>();

    public onClick(data: any): void {
        this.clicked.emit(data);
    }
}
