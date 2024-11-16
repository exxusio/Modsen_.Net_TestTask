import { Routes } from '@angular/router';
import { MyRegistrationsComponent } from './components/pages/my-registrations/my-registrations.component';
import { PageNotFoundComponent } from './components/pages/page-not-found/page-not-found.component';
import { AdminPanelComponent } from './components/pages/admin-panel/admin-panel.component';
import { ProfileComponent } from './components/pages/profile/profile.component';
import { SignUpComponent } from './components/pages/sign-up/sign-up.component';
import { LogInComponent } from './components/pages/log-in/log-in.component';
import { EventComponent } from './components/pages/event/event.component';
import { HomeComponent } from './components/pages/home/home.component';
import { unauthGuard } from './guards/unauth.guard';
import { authGuard } from './guards/auth.guard';
import { adminGuard } from './guards/admin.guard';

export const routes: Routes = [
    { path: 'sign-up', component: SignUpComponent, canActivate: [unauthGuard] },
    { path: 'log-in', component: LogInComponent, canActivate: [unauthGuard] },
    { path: 'profile', component: ProfileComponent, canActivate: [authGuard] },
    { path: 'events/:eventId', component: EventComponent, canActivate: [] },
    {
        path: 'my-registrations',
        component: MyRegistrationsComponent,
        canActivate: [authGuard],
    },
    {
        path: 'admin-panel',
        component: AdminPanelComponent,
        canActivate: [authGuard, adminGuard],
    },
    { path: '', component: HomeComponent, canActivate: [] },
    { path: '**', component: PageNotFoundComponent },
];
