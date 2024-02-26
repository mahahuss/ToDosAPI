import { Component, EventEmitter, HostListener, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToDoTask } from '../../../shared/models/todo';
import { TodosService } from '../../../services/todos.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-todo-dialog',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './todo-dialog.component.html',
  styleUrl: './todo-dialog.component.scss',
  host: { '(window:keyup.esc)': 'onClose()' },
})
export class TodoDialogComponent {
  @Input() todoTask: ToDoTask | undefined = undefined;
  @Output() taskDeleted = new EventEmitter<boolean>();

  constructor(
    private todosService: TodosService,
    private toastr: ToastrService,
  ) {}

  @HostListener('window:keyup.esc')
  onClose() {
    this.taskDeleted.emit(true);
  }
  onDelete() {
    this.todosService.deleteTask(this.todoTask!).subscribe({
      next: (res) => {
        this.taskDeleted.emit(false);
        this.toastr.success(res.message);
      },
    });
  }
}
