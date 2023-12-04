import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToDoTask } from '../../../shared/models/auth';
import { TodosService } from '../../../services/todos.service';
import { ToastrService } from 'ngx-toastr';
import { TodoDialogComponent } from '../todo-dialog/todo-dialog.component';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-todo-item',
  standalone: true,
  templateUrl: './todo-item.component.html',
  styleUrl: './todo-item.component.scss',
  imports: [CommonModule, TodoDialogComponent, FormsModule],
})
export class TodoItemComponent implements OnInit {
  @Input() todoTask: ToDoTask | undefined = undefined;
  @Output() taskUpdated = new EventEmitter<ToDoTask>();
  @Output() deleteStatusChanged = new EventEmitter();
  content = '';
  updateClickStatus = false;

  constructor(
    private todosService: TodosService,
    private toastr: ToastrService,
  ) {}

  ngOnInit(): void {
    this.content = this.todoTask!.taskContent;
  }

  updateStatus(task: ToDoTask) {
    task.status = !task.status;
    this.todosService.updateTask(task).subscribe({
      next: () => {
        this.taskUpdated.emit(task);
        this.toastr.success('The task updated successfully');
      },
      error: (res) => {
        console.log(res.error.message);
      },
    });
  }

  toggleEdit() {
    this.updateClickStatus = !this.updateClickStatus;
  }

  onDeleteClick() {
    this.deleteStatusChanged.emit();
  }

  updateTask(task: ToDoTask) {
    task.taskContent = this.content;
    this.todosService.updateTask(task).subscribe({
      next: () => {
        this.taskUpdated.emit(task);
        this.toastr.success('The task updated successfully');
        this.toggleEdit();
      },
      error: (res) => {
        console.log(res.error.message);
      },
    });
  }
}
