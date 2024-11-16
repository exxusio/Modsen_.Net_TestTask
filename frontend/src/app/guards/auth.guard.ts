import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { UserService } from '../data/services/user.service';

export const authGuard: CanActivateFn = (route, state) => {
    const isLoggedIn = inject(UserService).isAuth();

    if (isLoggedIn) {
        return true;
    }

    return inject(Router).createUrlTree(['/sign-up']);
};
