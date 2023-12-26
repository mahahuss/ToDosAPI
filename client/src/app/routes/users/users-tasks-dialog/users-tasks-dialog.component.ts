import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToDoTask, UserTaskFile } from '../../../shared/models/todo';
import { AuthService } from '../../../services/auth.service';
import { TodosService } from '../../../services/todos.service';
import { ToastrService } from 'ngx-toastr';
import saveAs from 'file-saver';

@Component({
  selector: 'app-users-tasks-dialog',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './users-tasks-dialog.component.html',
  styleUrl: './users-tasks-dialog.component.scss',
})
export class UsersTasksDialogComponent implements OnInit {
  @Input() userId: number | undefined;
  @Output() tasksViewClosed = new EventEmitter();
  todos: ToDoTask[] = [];

  constructor(
    private authService: AuthService,
    private todosService: TodosService,
    private toastr: ToastrService,
  ) {}
  ngOnInit(): void {
    this.loadTodos();
  }

  onClose() {
    this.tasksViewClosed.emit();
  }

  private loadTodos() {
    {
      this.todosService.getUserTodos(this.userId!, 1, 5).subscribe({
        next: (res) => {
          this.todos = res.tasks;
        },
        error: (err) => {
          this.toastr.error(err.error.message);
        },
      });
    }
  }

  getFile(attachment: UserTaskFile) {
    this.todosService.getTaskAttachment(attachment.id).subscribe({
      next: (res) => {
        saveAs.saveAs(res, attachment.fileName);
      },
      error: (err) => {
        this.toastr.error(err.error.message);
      },
    });
  }
}
