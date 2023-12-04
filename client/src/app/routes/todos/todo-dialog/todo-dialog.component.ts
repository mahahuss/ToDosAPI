import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToDoTask } from '../../../shared/models/auth';
import { TodosService } from '../../../services/todos.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-todo-dialog',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './todo-dialog.component.html',
  styleUrl: './todo-dialog.component.scss',
})
export class TodoDialogComponent {
  @Input() todoTask: ToDoTask | undefined = undefined;
  @Output() taskDeleted = new EventEmitter<boolean>();

  constructor(
    private todosService: TodosService,
    private toastr: ToastrService,
  ) {}

  onClose() {
    this.taskDeleted.emit(true);
  }
  onDelete() {
    this.todosService.deleteTask(this.todoTask!).subscribe({
      next: (res) => {
        console.log(res);

        this.taskDeleted.emit(false);
        this.toastr.success(res.message);
      },
      error: (res) => {
        console.log(res.error.message);
      },
    });
  }
}
