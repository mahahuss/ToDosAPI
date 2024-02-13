import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { ApiResponse } from '../shared/models/common';
import { GetUserTasksResponse, ToDoTask, UserTask } from '../shared/models/todo';
import { ShareTask } from '../shared/models/auth';

@Injectable({
  providedIn: 'root',
})
export class TodosService {
  private readonly baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getUserTodos(userId: number, pageNumber: number, pageSize: number): Observable<GetUserTasksResponse> {
    return this.http.get<GetUserTasksResponse>(
      this.baseUrl + 'tasks/usertasks?userId=' + userId + '&pageNumber=' + pageNumber + '&pageSize=' + pageSize,
    );
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

  shareTask(sharedTask: ShareTask): Observable<ToDoTask> {
    return this.http.post<ToDoTask>(this.baseUrl + 'tasks/share', sharedTask);
  }

  getUserTasks(userId: Number): Observable<UserTask[]> {
    return this.http.get<UserTask[]>(this.baseUrl + 'Tasks/usertasksonly/' + userId);
  }
}
