import { CommonModule } from '@angular/common';
import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  HostListener,
  ChangeDetectorRef,
  AfterContentChecked,
} from '@angular/core';
import saveAs from 'file-saver';
import { TodosService } from '../../../services/todos.service';
import { UserTask, UserTaskFile } from '../../../shared/models/todo';

@Component({
  selector: 'app-users-tasks-dialog',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './users-tasks-dialog.component.html',
  styleUrl: './users-tasks-dialog.component.scss',
  host: { '(window:keyup.esc)': 'onClose()' },
})
export class UsersTasksDialogComponent implements OnInit, AfterContentChecked {
  @Input() userId: number | undefined;
  @Output() tasksViewClosed = new EventEmitter();
  todos: UserTask[] = [];

  constructor(
    private todosService: TodosService,
    private cdRef: ChangeDetectorRef,
  ) {}
  ngOnInit(): void {
    this.loadTodos();
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
  ngAfterContentChecked() {
    this.cdRef.detectChanges();
  }
  @HostListener('window:keyup.esc')
  onClose() {
    this.tasksViewClosed.emit();
  }

  getFile(attachment: UserTaskFile) {
    this.todosService.getTaskAttachment(attachment.id).subscribe({
      next: (res) => {
        saveAs(res, attachment.fileName);
      },
    });
  }
}
