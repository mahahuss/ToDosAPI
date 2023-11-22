import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { LoginResponse, User, toDoTask } from '../shared/models/auth';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class TodosService {
  private readonly baseUrl = environment.apiUrl;
  private userTodosSource$ = new BehaviorSubject< Array<toDoTask> | undefined>(undefined);
  userTodos$ = this.userTodosSource$.asObservable();

  constructor(private http: HttpClient) {}


  getUserTodos(userId : number): Observable<Array<toDoTask>> {
    var reqHeader = new HttpHeaders({ 
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + localStorage.getItem('token')
   }); 
   console.log(reqHeader);
    return this.http
      .get<Array<toDoTask>>(this.baseUrl + 'tasks/'+userId, { headers: reqHeader })
      .pipe(
        map((res) => {
           const todo = <Array<toDoTask>> res;
          this.userTodosSource$.next(todo);
          return todo;
        })
      );
  }

}
