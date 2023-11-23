import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { EditTaskResponse, ToDoTask } from '../shared/models/auth';

@Injectable({
  providedIn: 'root',
})
export class TodosService {
  private readonly baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getUserTodos(userId: number): Observable<ToDoTask[]> {
    const reqHeader = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: 'Bearer ' + localStorage.getItem('token'),
    });
    console.log(reqHeader);
    return this.http.get<ToDoTask[]>(this.baseUrl + 'tasks/' + userId, {
      headers: reqHeader,
    });
  }

  updateTask(task : ToDoTask): Observable<boolean> {
    task.status = task.status ==0? 1:0;
    console.log(task);
    const reqHeader = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: 'Bearer ' + localStorage.getItem('token'),
    });
    return this.http
    .put<EditTaskResponse>(this.baseUrl + 'Tasks', task,  {
      headers: reqHeader,
    })
    .pipe(
      map((res) => {
        return res.status;
      }),
    );
  }
}
