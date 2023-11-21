import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, firstValueFrom, map } from 'rxjs';
import { LoginResponse, User } from '../shared/models/auth';
import { jwtDecode } from 'jwt-decode';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly baseUrl = `http://localhost:5135/`;
  private currentUserSource$ = new BehaviorSubject<User | undefined>(undefined);
  currentUser$ = this.currentUserSource$.asObservable();
  private loginSuccessSource$ = new BehaviorSubject<boolean>(false);
  loginSuccess$ = this.loginSuccessSource$.asObservable();


  constructor(private http: HttpClient, private router : Router) {}


  checkTokenExpiry(){
    if (localStorage.getItem('token') !=null){
      console.log("token not null "+ localStorage.getItem('token') )
    if (Date.now() >= jwtDecode(localStorage.getItem('token')!).exp! * 1000){
      localStorage.removeItem('token');
      this.loginSuccessSource$.next(false);
      this.router.navigate(["/login"]);
    }
    else {
      console.log("token null "+ localStorage.getItem('token'))
      this.router.navigate(["/login"]);
    }
    }
   }
  

  login(username: string, password: string): Observable<LoginResponse> {
    return this.http
      .post<LoginResponse>(this.baseUrl + 'users/login', {
        username,
        password,
      })
      .pipe(
        map((res) => {
          console.log(res);
          if (res !=null){
          localStorage.setItem('token', res.token);
          // decode token then set current user props
          this.currentUserSource$.next(jwtDecode<User | undefined>(res.token));
          this.loginSuccessSource$.next(true);
          }
          // this.loginSuccessSource$.subscribe(() => {
          // });
          return res;
        })
      );
  }

 
    
  async login2(username: string, password: string): Promise<LoginResponse> {
    const response = await firstValueFrom(
      this.http.post<LoginResponse>(this.baseUrl + 'users', {
        username,
        password,
      })
    );
    return response;
  }
}
