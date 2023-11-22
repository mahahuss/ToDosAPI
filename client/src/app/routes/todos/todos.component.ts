import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { User, ToDoTask } from '../../shared/models/auth';
import { AuthService } from '../../services/auth.service';
import { TodosService } from '../../services/todos.service';

@Component({
  selector: 'app-todos',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './todos.component.html',
  styleUrl: './todos.component.scss',
})
export class TodosComponent implements OnInit {
  userInfo!: User;
  todos!: ToDoTask[];

  constructor(
    private authService: AuthService,
    private todosService: TodosService
  ) {}

  ngOnInit(): void {
    this.loadTodos();
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
