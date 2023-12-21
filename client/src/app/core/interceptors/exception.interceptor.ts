import { HttpEvent, HttpInterceptorFn } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

export const exceptionsInterceptor: HttpInterceptorFn = (req, next): Observable<HttpEvent<any>> => {
  return next(req).pipe(
    catchError((error) => {
      console.error('error is intercept', error);
      return throwError(() => error);
    }),
  );
};
