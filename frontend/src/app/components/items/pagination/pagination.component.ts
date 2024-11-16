import { NgClass, NgFor, NgIf } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
    selector: 'app-pagination',
    standalone: true,
    imports: [NgClass, NgIf, NgFor],
    templateUrl: './pagination.component.html',
    styleUrl: './pagination.component.css',
})
export class PaginationComponent {
    @Input() currentPage: number = 1;
    @Input() totalPages: number = 1;
    @Output() pageChange = new EventEmitter<number>();

    public getPages(): number[] {
        const pages = [];
        const maxPages = 5;
        let startPage = Math.max(this.currentPage - 2, 1);

        for (
            let i = startPage;
            i < startPage + maxPages && i <= this.totalPages;
            i++
        ) {
            pages.push(i);
        }

        return pages;
    }

    public changePage(page: number): void {
        if (page >= 1 && page <= this.totalPages) {
            this.pageChange.emit(page);
        }
    }
}
