import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, firstValueFrom, map } from 'rxjs';
import { LoginResponse, User } from '../shared/models/auth';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly baseUrl = `http://localhost:5135/`;
  private currentUserSource$ = new BehaviorSubject<User | undefined>(undefined);
  currentUser$ = this.currentUserSource$.asObservable();

  constructor(private http: HttpClient) {}

  login(username: string, password: string): Observable<LoginResponse> {
    return this.http
      .post<LoginResponse>(this.baseUrl + 'users/login', {
        username,
        password,
      })
      .pipe(
        map((res) => {
          localStorage.setItem('token', res.token);
          // decode token then set current user props
          jwtDecode();
          this.currentUserSource$.next(decodedToken);

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
