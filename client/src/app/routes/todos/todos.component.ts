import { Component, OnInit, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToDoTask, User } from '../../shared/models/auth';
import { AuthService } from '../../services/auth.service';
import { TodosService } from '../../services/todos.service';
import { TodoItemComponent } from './todo-item/todo-item.component';
import { NewTaskComponent } from './new-task/new-task.component';
import { TodoDialogComponent } from './todo-dialog/todo-dialog.component';

@Component({
  selector: 'app-todos',
  standalone: true,
  templateUrl: './todos.component.html',
  styleUrl: './todos.component.scss',
  imports: [CommonModule, TodoItemComponent, NewTaskComponent, TodoDialogComponent],
})
export class TodosComponent implements OnInit {
  userInfo!: User;
  todos: ToDoTask[] = [];
  toDoTask: ToDoTask | undefined;

  constructor(
    private authService: AuthService,
    private todosService: TodosService,
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
        this.todosService.getUserTodos(this.userInfo.nameid).subscribe({
          next: (res) => {
            this.todos = res;
          },
          error: (res) => {
            console.log(res.error.message);
          },
        });
      },
    });
  }
}
