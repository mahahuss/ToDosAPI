import { HttpInterceptorFn } from '@angular/common/http';
import { HttpEvent } from '@angular/common/http';
import { Observable, delay } from 'rxjs';
import { environment } from '../../../environments/environment';

export const delayInterceptor: HttpInterceptorFn = (req, next): Observable<HttpEvent<any>> => {
  if (environment.production) return next(req);

  return next(req).pipe(delay(1000));
};
