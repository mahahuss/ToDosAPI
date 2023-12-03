import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { AddNewTaskModel, DeleteTaskResponse, EditTaskResponse, ToDoTask } from '../shared/models/auth';
import { TmplAstForLoopBlock } from '@angular/compiler';
import { MatDialog } from '@angular/material/dialog';
import { TodoDialogComponent } from '../routes/todos/todo-dialog/todo-dialog.component';

@Injectable({
  providedIn: 'root',
})
export class TodosService {
  private readonly baseUrl = environment.apiUrl;

  constructor(private http: HttpClient, private dialog: MatDialog) {}

  getUserTodos(userId: number): Observable<ToDoTask[]> {
    const reqHeader = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: 'Bearer ' + localStorage.getItem('token'),
    });
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
  
  deleteTask(task : ToDoTask): Observable<boolean> {
    
    const reqHeader = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: 'Bearer ' + localStorage.getItem('token'),
    });
    return this.http
    .delete<DeleteTaskResponse>(this.baseUrl + 'Tasks/'+task.id,  {
      headers: reqHeader,
    })
    .pipe(
      map((res) => {
        return res.status;
      }),
    );
  }

  addNewTask( task : AddNewTaskModel) : Observable<ToDoTask> {

    const reqHeader = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: 'Bearer ' + localStorage.getItem('token'),
    });
    return this.http.post<ToDoTask>(this.baseUrl + 'tasks', task , {
      headers: reqHeader,
    });

  }


  open() {
    return this.dialog.open(TodoDialogComponent);
  }
}
