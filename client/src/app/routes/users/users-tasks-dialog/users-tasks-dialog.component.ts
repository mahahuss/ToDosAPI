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
import { SpinnerComponent } from '../../../core/components/spinner/spinner.component';

@Component({
  selector: 'app-users-tasks-dialog',
  standalone: true,
  templateUrl: './users-tasks-dialog.component.html',
  styleUrl: './users-tasks-dialog.component.scss',
  host: { '(window:keyup.esc)': 'onClose()' },
  imports: [CommonModule, SpinnerComponent],
})
export class UsersTasksDialogComponent implements OnInit, AfterContentChecked {
  @Input() userId: number | undefined;
  @Output() tasksViewClosed = new EventEmitter();
  todos: UserTask[] = [];
  isLoading = true;

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
          this.isLoading = false;
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
