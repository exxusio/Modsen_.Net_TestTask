import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { UserService } from '../data/services/user.service';

export const unauthGuard: CanActivateFn = (route, state) => {
    const isLoggedIn = inject(UserService).isAuth();

    if (isLoggedIn) {
        return inject(Router).createUrlTree(['/']);
    }

    return true;
};
