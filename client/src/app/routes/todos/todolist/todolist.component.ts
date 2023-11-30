import { Component, EventEmitter, Input, Output, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { User, ToDoTask } from '../../../shared/models/auth';
import { AuthService } from '../../../services/auth.service';
import { TodosService } from '../../../services/todos.service';
import { ConfirmBoxInitializer } from '@costlydeveloper/ngx-awesome-popup';

@Component({
  selector: 'app-todolist',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './todolist.component.html',
  styleUrl: './todolist.component.scss'
})
export class TodolistComponent {


@Input() todoTask: ToDoTask | undefined = undefined;
@Output() updateTask = new EventEmitter<{ task: ToDoTask }>();
@Output() deleteTask = new EventEmitter<{ task: ToDoTask }>();


constructor(
    private authService: AuthService,
    private todosService: TodosService
  ) {}

   onTaskClick(task : ToDoTask){
    this.updateTask.emit( { task: task })
  }

  onDeleteClick(task : ToDoTask){
   this.deleteTask.emit( { task: task })
  }
  

  
  // ngOnChanges(changes: SimpleChanges) {
  //   console.log("changes here : "+ changes['todoTask'])

  // }

}
