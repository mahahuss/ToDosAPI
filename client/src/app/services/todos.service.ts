import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { ApiResponse } from '../shared/models/common';
import { AddNewTaskModel, ToDoTask, UserTaskFile } from '../shared/models/todo';

@Injectable({
  providedIn: 'root',
})
export class TodosService {
  private readonly baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getUserTodos(userId: number): Observable<ToDoTask[]> {
    return this.http.get<ToDoTask[]>(this.baseUrl + 'tasks/' + userId);
  }

  updateTask(task: ToDoTask): Observable<ApiResponse> {
    return this.http.put<ApiResponse>(this.baseUrl + 'tasks', task).pipe(
      map((res) => {
        return res;
      }),
    );
  }
  deleteTask(task: ToDoTask): Observable<ApiResponse> {
    return this.http.delete<ApiResponse>(this.baseUrl + 'tasks/' + task.id);
  }

  addNewTask(task: FormData): Observable<ToDoTask> {
    return this.http.post<ToDoTask>(this.baseUrl + 'tasks', task);
  }

  getTaskAttachment(attachmentId: number): Observable<Blob> {
    const httpOptions = {
      responseType: 'blob' as 'json',
    };

    return this.http.get<Blob>(this.baseUrl + 'Tasks/attachments/' + attachmentId, httpOptions);
  }
}
