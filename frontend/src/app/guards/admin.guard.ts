import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { UserService } from '../data/services/user.service';

export const adminGuard: CanActivateFn = (route, state) => {
    var isAdminStatus = false;
    inject(UserService)
        .isAdmin()
        .subscribe((isAdmin) => {
            isAdminStatus = isAdmin;
        });

    if (isAdminStatus) {
        return true;
    }

    return inject(Router).createUrlTree(['/']);
};
