import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { TodosService } from '../../services/todos.service';
import { User } from '../../shared/models/auth';
import { GetUserTasksResponse, ToDoTask } from '../../shared/models/todo';
import { NewTaskComponent } from './new-task/new-task.component';
import { TodoDialogComponent } from './todo-dialog/todo-dialog.component';
import { TodoFilesDialogComponent } from './todo-files-dialog/todo-files-dialog.component';
import { TodoItemComponent } from './todo-item/todo-item.component';

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
  todos: ToDoTask[] = [];
  toDoTask: ToDoTask | undefined;
  tasksResponse: GetUserTasksResponse | undefined;
  pages: number[] = [];
  currentpage = 1;
  currentUserId!: number;

  constructor(
    private authService: AuthService,
    private todosService: TodosService,
  ) {}

  ngOnInit(): void {
    this.currentUserId = this.authService.getCurrentUserFromToken()?.nameid ?? -1;
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
    this.todosService.getUserTodos(this.currentUserId, this.currentpage, 5).subscribe({
      next: (res) => {
        this.tasksResponse = res;
        this.todos = res.tasks;
        this.pages = Array.from(new Array(res.totalPages), (x, i) => i + 1);
        this.currentpage = res.pageNumber;
      },
    });
  }

  changePage(pageNumber: number) {
    this.currentpage = pageNumber;
    this.loadTodos();
  }
}
