import { ActivatedRouteSnapshot, CanActivateFn, Router, RouterStateSnapshot } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { inject } from '@angular/core';

export const loggedInAuthGuardGuard: CanActivateFn = (
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  if (authService.loginSuccess$) {
    console.log("here !!" + authService.loginSuccess$.subscribe({
        next: (res) => {
          console.log(res)
        }
      }))

    router.navigate(['/home']);
    return false;
  } else {
    return true;
  }
};
