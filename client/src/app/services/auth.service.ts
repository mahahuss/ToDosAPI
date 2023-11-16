import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, firstValueFrom, map } from 'rxjs';
import { LoginResponse } from '../shared/models/auth';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly baseUrl = `http://localhost:5135/`;

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
