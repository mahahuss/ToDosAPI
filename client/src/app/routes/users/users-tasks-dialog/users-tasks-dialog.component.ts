import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToDoTask, UserTaskFile } from '../../../shared/models/todo';
import { AuthService } from '../../../services/auth.service';
import { TodosService } from '../../../services/todos.service';

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
  ) {}
  ngOnInit(): void {
    this.loadTodos();
  }

  onClose() {
    this.tasksViewClosed.emit();
  }

  private loadTodos() {
    {
      this.todosService.getUserTodos(this.userId!).subscribe({
        next: (res) => {
          this.todos = res;
        },
        error: (res) => {
          console.log(res.error.message);
        },
      });
    }
  }

  getFile(attachment: UserTaskFile) {
    this.todosService.getTaskAttachment(attachment.id).subscribe({
      next: (res) => {
        new Blob([res], { type: 'application/' + attachment.fileName.split('.').pop()?.toUpperCase() });
        var downloadURL = window.URL.createObjectURL(res);
        var link = document.createElement('a');
        link.href = downloadURL;
        link.download = attachment.fileName;
        link.click();
      },
      error: (res) => {
        console.log('error: ' + res.error.message);
      },
    });
  }
}
