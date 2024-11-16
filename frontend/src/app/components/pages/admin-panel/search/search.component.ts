import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
    selector: 'app-search',
    standalone: true,
    imports: [],
    templateUrl: './search.component.html',
    styleUrl: './search.component.css',
})
export class SearchComponent {
    @Input() data: any[] = [];
    @Input() searchField: string = '';
    @Output() searchResults = new EventEmitter<any[]>();

    public onSearch(event: Event): void {
        if (this.data.length > 0) {
            const input = (
                event.target as HTMLInputElement
            ).value.toLowerCase();
            const filteredData = this.data.filter(
                (item) =>
                    item[this.searchField]?.toLowerCase().includes(input) /**/
            );
            this.searchResults.emit(filteredData);
        }
    }
}
