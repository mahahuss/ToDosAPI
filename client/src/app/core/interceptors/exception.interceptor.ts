import { HttpEvent, HttpInterceptorFn } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';
import { inject } from '@angular/core';

export const exceptionsInterceptor: HttpInterceptorFn = (req, next): Observable<HttpEvent<any>> => {
  const toastr = inject(ToastrService);
  return next(req).pipe(
    catchError((error) => {
      console.error('error is intercept', error);
      toastr.error(error.error.message);
      return throwError(() => error);
    }),
  );
};
