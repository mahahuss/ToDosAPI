import { HttpInterceptorFn } from '@angular/common/http';
import { HttpEvent } from '@angular/common/http';
import { inject } from '@angular/core';
import { Observable, finalize } from 'rxjs';
import { LoaderService } from '../../services/loader.service';

export const authInterceptor: HttpInterceptorFn = (req, next): Observable<HttpEvent<any>> => {
  const loader = inject(LoaderService);
  loader.showLoader();
  const userToken = localStorage.getItem('token');
  req = req.clone({
    headers: req.headers.set('Authorization', `Bearer ${userToken}`),
  });
  return next(req).pipe(finalize(() => loader.hideLoader()));
};
