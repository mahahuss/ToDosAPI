import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';
import { BehaviorSubject, Observable, firstValueFrom, map } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { User, UserProfile } from '../shared/models/auth';
import { ToDoTask } from '../shared/models/todo';
import { ApiResponse } from '../shared/models/common';

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
    if (!token) return undefined;
    return jwtDecode<User>(token);
  }

  updateCurrentUser(user: User) {
    this.currentUserSource$.next(user);
  }

  login(username: string, password: string): Observable<User> {
    return this.http
      .post<ApiResponse>(this.baseUrl + 'users/login', {
        username,
        password,
      })
      .pipe(
        map((res) => {
          localStorage.setItem('token', res.message); //message includes the token
          const user = jwtDecode<User>(res.message);
          this.currentUserSource$.next(user);
          localStorage.setItem('fullname', user.given_name);

          return user;
        }),
      );
  }

  updateUserProfile(userInfo: FormData): Observable<ApiResponse> {
    return this.http.put<ApiResponse>(this.baseUrl + 'Users', userInfo).pipe(
      map((res) => {
        return res;
      }),
    );
  }
}
