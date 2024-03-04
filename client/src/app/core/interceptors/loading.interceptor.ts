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
  if (
    (req.url.includes('tasks') && req.method === 'PUT') ||
    (req.url.includes('users/roles') && req.method === 'GET') ||
    (req.url.includes('tasks/share') && req.method === 'POST') ||
    (req.url.includes('tasks') && req.method === 'DELETE') ||
    (req.url.includes('tasks') && req.method === 'POST') ||
    (req.url.includes('Tasks/user-tasks-only') && req.method === 'GET') ||
    (req.url.includes('Users/status') && req.method === 'PUT') ||
    (req.url.includes('Users/edit-roles') && req.method === 'PUT') ||
    (req.url.includes('users/user-roles') && req.method === 'GET') ||
    (req.url.includes('Users') && req.method === 'PUT') ||
    (req.url.includes('Tasks/attachments') && req.method === 'GET') ||
    (req.url.includes('users/image') && req.method === 'GET')
  ) {
    return true;
  }
  return false;
}
