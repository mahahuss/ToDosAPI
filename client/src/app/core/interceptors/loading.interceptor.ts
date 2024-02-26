import { HttpEvent, HttpInterceptorFn, HttpRequest, HttpResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { Observable, map } from 'rxjs';
import { LoaderService } from '../../services/loader.service';

let requests: HttpRequest<unknown>[] = [];

export const loadingInterceptor: HttpInterceptorFn = (req, next): Observable<HttpEvent<any>> => {
  if (isException(req)) {
    console.log('NO LOADING');

    return next(req);
  }

  const loader = inject(LoaderService);

  requests.push(req);
  loader.loader(true);
  return next(req).pipe(
    map((event) => {
      if (event instanceof HttpResponse) {
        const index = requests.indexOf(req);

        if (index > -1) {
          requests.splice(index, 1);
        }

        loader.loader(requests.length > 0);
      }
      return event;
    }),
  );
};

function isException(req: HttpRequest<unknown>) {
  if (req.url.includes('tasks') && req.method === 'PUT') {
    return true;
  }

  return false;
}
