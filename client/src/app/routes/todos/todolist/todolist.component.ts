import { Component, OnInit, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { User, ToDoTask } from '../../../shared/models/auth';
import { AuthService } from '../../../services/auth.service';
import { TodosService } from '../../../services/todos.service';

@Component({
  selector: 'app-todolist',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './todolist.component.html',
  styleUrl: './todolist.component.scss'
})
export class TodolistComponent {


@Input() todoTask: ToDoTask | undefined = undefined;

constructor(
    private authService: AuthService,
    private todosService: TodosService
  ) {}

   updateStatus(task : ToDoTask){

    this.todosService.updateTask(task).subscribe({
      next: (res) => {
        console.log(res);
        //this.loadTodos();
      },
      error: (res) => {
        console.log(res.error.message);
      },
    });

  }

}
