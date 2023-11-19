import { CanActivateFn, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Injectable, inject } from  "@angular/core";
import {Observable} from "rxjs";
import {AuthService} from "../services/auth.service";

export const guardAuthGuard: CanActivateFn = (route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot) => {
    if (inject(AuthService).isAuthenticated())
    return true
    else{
    inject(Router).navigate(['../login']);
    return false;
  }
};