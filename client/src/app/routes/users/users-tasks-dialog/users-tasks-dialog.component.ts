import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import saveAs from 'file-saver';
import { TodosService } from '../../../services/todos.service';
import { UserTask, UserTaskFile } from '../../../shared/models/todo';

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
  todos: UserTask[] = [];

  constructor(private todosService: TodosService) {}
  ngOnInit(): void {
    this.loadTodos();
  }

  onClose() {
    this.tasksViewClosed.emit();
  }

  private loadTodos() {
    {
      this.todosService.getUserTasks(this.userId!).subscribe({
        next: (res) => {
          this.todos = res;
        },
      });
    }
  }

  getFile(attachment: UserTaskFile) {
    this.todosService.getTaskAttachment(attachment.id).subscribe({
      next: (res) => {
        saveAs(res, attachment.fileName);
      },
    });
  }
}
