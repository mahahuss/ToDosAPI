import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';
import { BehaviorSubject, Observable, lastValueFrom, map, of } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { Role, UpdateUser, User, UserInfo, UserPhoto, UserToShare } from '../shared/models/auth';
import { ApiResponse } from '../shared/models/common';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly baseUrl = environment.apiUrl;
  private currentUserSource$ = new BehaviorSubject<User | undefined>(undefined);
  currentUser$ = this.currentUserSource$.asObservable();
  private userListSource$ = new BehaviorSubject<Array<UserToShare>>([]);
  userList$ = this.userListSource$.asObservable();
  private usersToShare: UserToShare[] | undefined;

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
    const user = jwtDecode<User>(token);

    if (user) {
      user.nameid = +user.nameid;
    }

    return user;
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

  getUsers(): Observable<UserInfo[]> {
    return this.http.get<UserInfo[]>(this.baseUrl + 'users');
  }

  changeUserStatus(userId: number, status: boolean): Observable<ApiResponse> {
    return this.http.put<ApiResponse>(this.baseUrl + 'Users/status', { userId: userId, status: status }).pipe(
      map((res) => {
        return res;
      }),
    );
  }

  refreshToken() {
    return this.http.get<ApiResponse>(this.baseUrl + 'users/token', {}).pipe(
      map((res) => {
        localStorage.setItem('token', res.message); //message includes the token
        const user = jwtDecode<User>(res.message);
        this.currentUserSource$.next(user);
        localStorage.setItem('fullname', user.given_name);
        return user;
      }),
    );
  }

  getRoles(): Observable<Role[]> {
    return this.http.get<Role[]>(this.baseUrl + 'users/roles');
  }

  getUserRoles(userId: number): Observable<Role[]> {
    return this.http.get<Role[]>(this.baseUrl + 'users/user-roles/' + userId);
  }

  updateUser(user: UpdateUser): Observable<ApiResponse> {
    return this.http.put<ApiResponse>(this.baseUrl + 'Users/edit-roles', user).pipe(
      map((res) => {
        return res;
      }),
    );
  }

  async getUsersToShare(): Promise<UserToShare[]> {
    if (this.usersToShare) {
      return this.usersToShare;
    }

    const res = await lastValueFrom(this.http.get<UserToShare[]>(this.baseUrl + 'users/users-to-share'));
    this.usersToShare = res;
    return res;
  }

  startRefreshTokenInterval(): void {
    if (this.isTokenValid()) {
      setInterval(() => {
        const currentExpiryTime = jwtDecode(localStorage.getItem('token')!).exp!;
        const fiveMinBefore = new Date(currentExpiryTime * 1000 - 30_000);
        const timeNow = new Date();

        if (fiveMinBefore < timeNow) {
          this.refreshToken().subscribe();
        }
      }, 1000 * 30);
    }
  }

  getUserPhoto(): Observable<UserPhoto> {
    return this.http.get<UserPhoto>(this.baseUrl + 'users/image');
  }
}
