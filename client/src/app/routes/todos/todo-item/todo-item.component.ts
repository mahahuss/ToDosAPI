import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToDoTask } from '../../../shared/models/auth';
import { TodosService } from '../../../services/todos.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-todo-item',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './todo-item.component.html',
  styleUrl: './todo-item.component.scss'
})
export class TodoItemComponent {

  @Input() todoTask: ToDoTask | undefined = undefined;
  @Output() updateTask = new EventEmitter<ToDoTask >();
  @Output() deleteTask = new EventEmitter<ToDoTask >();

  constructor(private todosService: TodosService, private toastr: ToastrService) {}

  onTaskClick(task: ToDoTask) {
    this.todosService.updateTask(task).subscribe({
      next: (res) => {
        if (res){
        this.updateTask.emit(task);
        this.toastr.success("The task updated successfully")
      }
      },
      error: (res) => {
        console.log(res.error.message);
      },
    });
  }

  onDeleteClick(task: ToDoTask) {
    this.todosService.deleteTask(task).subscribe({
      next: (res) => {
        if (res){
        this.deleteTask.emit(task);
        this.toastr.success("The task deleted successfully")
        }
      },
      error: (res) => {
        console.log(res.error.message);
      },
    });
  
  }
}


