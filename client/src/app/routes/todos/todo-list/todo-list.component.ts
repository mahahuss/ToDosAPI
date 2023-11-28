import { Component, OnInit, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { User, ToDoTask } from '../../../shared/models/auth';
import { AuthService } from '../../../services/auth.service';
import { TodosService } from '../../../services/todos.service';

@Component({
  selector: 'app-todo-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './todo-list.component.html',
  styleUrl: './todo-list.component.scss'
})
export class TodoListComponent {

  

@Input() todoTask: ToDoTask | undefined = undefined;
@Input() myname : string="";
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
