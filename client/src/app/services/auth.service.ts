import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';
import { BehaviorSubject, Observable, firstValueFrom, map } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { LoginResponse, User } from '../shared/models/auth';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly baseUrl = environment.apiUrl;
  private currentUserSource$ = new BehaviorSubject<User | undefined>(undefined);
  currentUser$ = this.currentUserSource$.asObservable();

  constructor(private http: HttpClient) {}

  isTokenValid(): boolean {
    const token = localStorage.getItem('token');

    if (!token) {
      return false;
    }

    return Date.now() < jwtDecode(token).exp! * 1000;
  }

  setCurrentUserFromToken(): void {
    const token = localStorage.getItem('token');

    if (token) this.currentUserSource$.next(jwtDecode<User>(token));
  }

  getCurrentUserFromToken(): User | undefined {
    const token = localStorage.getItem('token');

    if (!token) {
      return undefined;
    }

    return jwtDecode<User>(token);
  }

  login(username: string, password: string): Observable<User> {
    return this.http
      .post<LoginResponse>(this.baseUrl + 'users/login', {
        username,
        password,
      })
      .pipe(
        map((res) => {
          localStorage.setItem('token', res.token);
          const user = jwtDecode<User>(res.token);
          this.currentUserSource$.next(user);
          return user;
        }),
      );
  }

  async login2(username: string, password: string): Promise<LoginResponse> {
    const response = await firstValueFrom(
      this.http.post<LoginResponse>(this.baseUrl + 'users', {
        username,
        password,
      }),
    );
    return response;
  }
}
