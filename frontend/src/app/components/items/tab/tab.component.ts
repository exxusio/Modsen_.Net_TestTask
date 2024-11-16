import { NgFor } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
    selector: 'app-tabs',
    standalone: true,
    imports: [NgFor],
    templateUrl: './tab.component.html',
    styleUrl: './tab.component.css',
})
export class TabComponent {
    @Input() activeTab: string = 'Events';
    @Input() tabList: string[] = [];
    @Output() tabChange = new EventEmitter<string>();

    setActiveTab(tab: string): void {
        this.activeTab = tab;
        this.tabChange.emit(tab);
    }
}
