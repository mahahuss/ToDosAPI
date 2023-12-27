import { Component, OnInit, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { User, UserInfo } from '../../shared/models/auth';
import { GetUserTasksResponse, ToDoTask } from '../../shared/models/todo';
import { AuthService } from '../../services/auth.service';
import { TodosService } from '../../services/todos.service';
import { TodoItemComponent } from './todo-item/todo-item.component';
import { NewTaskComponent } from './new-task/new-task.component';
import { TodoDialogComponent } from './todo-dialog/todo-dialog.component';
import { TodoFilesDialogComponent } from './todo-files-dialog/todo-files-dialog.component';
import { ToastrService } from 'ngx-toastr';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-todos',
  standalone: true,
  templateUrl: './todos.component.html',
  styleUrl: './todos.component.scss',
  imports: [
    CommonModule,
    TodoItemComponent,
    NewTaskComponent,
    TodoDialogComponent,
    TodoFilesDialogComponent,
    FormsModule,
  ],
})
export class TodosComponent implements OnInit {
  userInfo!: User;
  todos: ToDoTask[] = [];
  toDoTask: ToDoTask | undefined;
  tasksResponse: GetUserTasksResponse | undefined;
  pages: number[] = [];
  currentpage = 1;

  constructor(
    private authService: AuthService,
    private todosService: TodosService,
    private toastr: ToastrService,
  ) {}

  ngOnInit(): void {
    this.loadTodos();
  }

  taskAdded(createdTask: ToDoTask) {
    this.todos.push(createdTask);
  }

  taskUpdated(updatedtask: ToDoTask) {
    const indexToUpdate = this.todos.findIndex((item) => item.id === updatedtask.id);
    if (indexToUpdate !== -1) this.todos[indexToUpdate] = updatedtask;
  }

  taskDeleted(canceled: boolean) {
    if (canceled) this.toDoTask = undefined;
    else {
      this.todos = this.todos.filter((item) => item.id !== this.toDoTask!.id);
      this.toDoTask = undefined;
    }
  }

  deleteStatusChanged(toDoTask: ToDoTask) {
    this.toDoTask = toDoTask;
  }

  private loadTodos() {
    this.authService.currentUser$.subscribe({
      next: (res) => {
        this.userInfo = res!;
        this.todosService.getUserTodos(this.userInfo.nameid, this.currentpage, 5).subscribe({
          next: (res) => {
            this.tasksResponse = res;
            this.todos = res.tasks;
            this.pages = Array.from(new Array(res.totalPages), (x, i) => i + 1);
            this.currentpage = res.pageNumber;
          },
          error: (res) => {
            this.toastr.error(res.error.message);
          },
        });
      },
    });
  }

  changePage(pageNumber: number) {
    this.currentpage = pageNumber;
    this.loadTodos();
  }
}
