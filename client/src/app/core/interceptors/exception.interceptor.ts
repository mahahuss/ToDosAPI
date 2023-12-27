import { HttpEvent, HttpInterceptorFn } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';

export const exceptionsInterceptor: HttpInterceptorFn = (req, next): Observable<HttpEvent<any>> => {
  var rr: ToastrService;
  return next(req).pipe(
    catchError((error) => {
      console.error('error is intercept', error);
      rr.error(error);
      return throwError(() => error);
    }),
  );
};
