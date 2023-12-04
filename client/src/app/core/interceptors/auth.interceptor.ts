import { HttpInterceptorFn } from '@angular/common/http';
import { HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';

export const authInterceptor: HttpInterceptorFn = (req, next): Observable<HttpEvent<any>> => {
  const userToken = localStorage.getItem('token');
  req = req.clone({
    headers: req.headers.set('Authorization', `Bearer ${userToken}`),
  });
  return next(req);
};
