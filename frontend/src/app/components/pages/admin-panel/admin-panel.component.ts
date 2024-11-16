import { NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { TabComponent } from '../../items/tab/tab.component';
import { UserSectionComponent } from './sections/user-section/user-section.component';
import { EventSectionComponent } from './sections/event-section/event-section.component';
import { RegistrationSectionComponent } from './sections/registration-section/registration-section.component';

@Component({
    selector: 'app-admin-panel',
    standalone: true,
    imports: [
        NgIf,
        TabComponent,
        EventSectionComponent,
        UserSectionComponent,
        RegistrationSectionComponent,
    ],
    templateUrl: './admin-panel.component.html',
    styleUrl: './admin-panel.component.css',
})
export class AdminPanelComponent {
    activeTab: string = 'events';
    public tabList: string[] = ['Events', 'Users', 'Registrations'];

    setActiveTab(tab: string): void {
        this.activeTab = tab;
    }
}
