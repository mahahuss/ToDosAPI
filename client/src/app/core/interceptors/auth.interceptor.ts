import { HttpInterceptorFn } from '@angular/common/http';
import {HttpEvent,} from '@angular/common/http';
import { Observable } from 'rxjs';

export const authInterceptor: HttpInterceptorFn = (req, next) : Observable<HttpEvent<any>> => {
  // console.log('Outgoing HTTP request', req);
  // return next(req).pipe(
  //   tap((event: HttpEvent<any>) => {
  //     console.log('Incoming HTTP response', event);
  //   })
  // );
  const userToken = localStorage.getItem('token')
  const modifiedReq = req.clone({ 
    headers: req.headers.set('Authorization', `Bearer ${userToken}`),
  });
  return next(modifiedReq);
}

