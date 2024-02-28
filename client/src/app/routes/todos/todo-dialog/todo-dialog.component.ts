import { Component, EventEmitter, HostListener, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToDoTask } from '../../../shared/models/todo';
import { TodosService } from '../../../services/todos.service';
import { ToastrService } from 'ngx-toastr';
import { SpinnerComponent } from '../../../core/components/spinner/spinner.component';

@Component({
  selector: 'app-todo-dialog',
  standalone: true,
  templateUrl: './todo-dialog.component.html',
  styleUrl: './todo-dialog.component.scss',
  host: { '(window:keyup.esc)': 'onClose()' },
  imports: [CommonModule, SpinnerComponent],
})
export class TodoDialogComponent {
  @Input() todoTask: ToDoTask | undefined = undefined;
  @Output() taskDeleted = new EventEmitter<boolean>();
  isLoading = false;

  constructor(
    private todosService: TodosService,
    private toastr: ToastrService,
  ) {}

  @HostListener('window:keyup.esc')
  onClose() {
    this.taskDeleted.emit(true);
  }
  onDelete() {
    this.isLoading = true;
    this.todosService.deleteTask(this.todoTask!).subscribe({
      next: (res) => {
        this.toastr.success(res.message);
        this.taskDeleted.emit(false);
      },
    });
  }
}
