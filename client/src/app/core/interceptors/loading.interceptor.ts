import { HttpEvent, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Observable, finalize } from 'rxjs';
import { LoaderService } from '../../services/loader.service';

export const loadingInterceptor: HttpInterceptorFn = (req, next): Observable<HttpEvent<any>> => {
  let apiCount = 0;
  const loader = inject(LoaderService);
  loader.loader(apiCount);
  apiCount++;
  return next(req).pipe(
    finalize(() => {
      apiCount--;
      loader.loader(apiCount);
    }),
  );
};
