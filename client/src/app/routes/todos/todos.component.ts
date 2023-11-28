import { Component, OnInit , Input} from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToDoTask, User } from '../../shared/models/auth';
import { AuthService } from '../../services/auth.service';
import { TodosService } from '../../services/todos.service';
import { TodolistComponent } from './todolist/todolist.component';


@Component({
  selector: 'app-todos',
  standalone: true,
  imports: [CommonModule, TodolistComponent],
  templateUrl: './todos.component.html',
  styleUrl: './todos.component.scss',
})
export class TodosComponent  implements OnInit {
  userInfo!: User;
  todos!: ToDoTask[];
  name : string ="maha";
  

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
            console.log(res);
          },
          error: (res) => {
            console.log(res.error.message);
          },
        });
      },
    });
  }
}
