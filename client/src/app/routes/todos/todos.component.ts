import { Component, OnInit , Input} from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddNewTaskModel, ToDoTask, User } from '../../shared/models/auth';
import { AuthService } from '../../services/auth.service';
import { TodosService } from '../../services/todos.service';
import { TodolistComponent } from './todolist/todolist.component';
import { NewtaskComponent } from "./newtask/newtask.component";


@Component({
    selector: 'app-todos',
    standalone: true,
    templateUrl: './todos.component.html',
    styleUrl: './todos.component.scss',
    imports: [CommonModule, TodolistComponent, NewtaskComponent]
})
export class TodosComponent  implements OnInit {


  userInfo!: User;
  todos!: ToDoTask[];
  

  constructor(
    private authService: AuthService,
    private todosService: TodosService
  ) {}

  ngOnInit(): void {
    this.loadTodos();
  }

  addNewTask(eventData: { task : AddNewTaskModel }) {
    this.todosService.addNewTask(eventData.task).subscribe({
      next: (res) => {
        this.todos.push(res)
      },
      error: (res) => {
        console.log(res.error.message);
      },
    });
  }

  updateTask(eventData: { task : ToDoTask }){

    let indexToUpdate = this.todos.findIndex(item => item.id === eventData.task.id);
    if (indexToUpdate !== -1) 
    this.todos[indexToUpdate] = eventData.task;
  
    this.todosService.updateTask(eventData.task).subscribe({
      next: (res) => {
        console.log(res);
        //this.loadTodos();
      },
      error: (res) => {
        console.log(res.error.message);
      },
    });

  }


  deleteTask(eventData: { task : ToDoTask }){

    let indexToUpdate = this.todos.findIndex(item => item.id === eventData.task.id);
    if (indexToUpdate !== -1) {
      this.todos.splice(indexToUpdate, 1);
    
    this.todosService.deleteTask(eventData.task).subscribe({
      next: (res) => {
        console.log(res);
      },
      error: (res) => {
        console.log(res.error.message);
      },
    });
  }
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
