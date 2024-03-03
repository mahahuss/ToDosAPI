import {
  AfterViewInit,
  AfterContentChecked,
  ChangeDetectorRef,
  Component,
  ElementRef,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToDoTask } from '../../../shared/models/todo';
import { TodosService } from '../../../services/todos.service';
import { ToastrService } from 'ngx-toastr';
import { TodoDialogComponent } from '../todo-dialog/todo-dialog.component';
import { FormsModule } from '@angular/forms';
import { TodoFilesDialogComponent } from '../todo-files-dialog/todo-files-dialog.component';
import { ShareTaskDialogComponent } from '../share-task-dialog/share-task-dialog.component';
import { TodoEditDialogComponent } from '../todo-edit-dialog/todo-edit-dialog.component';
import { UserInfo, UserToShare } from '../../../shared/models/auth';
import { AuthService } from '../../../services/auth.service';
import { LoaderService } from '../../../services/loader.service';
import { SpinnerComponent } from '../../../core/components/spinner/spinner.component';

@Component({
  selector: 'app-todo-item',
  standalone: true,
  templateUrl: './todo-item.component.html',
  styleUrl: './todo-item.component.scss',
  imports: [
    CommonModule,
    TodoDialogComponent,
    FormsModule,
    TodoFilesDialogComponent,
    ShareTaskDialogComponent,
    TodoEditDialogComponent,
    SpinnerComponent,
  ],
})
export class TodoItemComponent implements OnInit, AfterContentChecked {
  @Input({ required: true }) todoTask!: ToDoTask;
  @Input({ required: true }) users!: UserInfo[];
  @Input({ required: true }) currentUserId!: number;
  @Output() taskUpdated = new EventEmitter<ToDoTask>();
  @Output() deleteStatusChanged = new EventEmitter();

  content = '';
  updateClickStatus = false;
  taskFilesExistence = false;
  filesClickStatus = false;
  shareClickStatus = false;
  editTodoStatus = false;
  sharedby = '';
  isLoading = false;
  allUsers: UserToShare[] = [];
  @ViewChild('focusTaskInput') focusTaskInput?: ElementRef;

  constructor(
    private authService: AuthService,
    private todosService: TodosService,
    private toastr: ToastrService,
    private cdRef: ChangeDetectorRef,
  ) {}

  async ngOnInit(): Promise<void> {
    this.taskFilesExistence = this.todoTask!.files.length > 0 ? true : false;
    const res = await this.authService.getUsersToShare();
    if (res.length === 0) return;

    this.allUsers = res;

    if (this.currentUserId !== this.todoTask.createdBy) {
      this.sharedby = this.allUsers.find((x) => x.id == this.todoTask.createdBy)!.fullName;
    }
  }

  ngAfterContentChecked() {
    this.cdRef.detectChanges();
  }

  updateStatus(task: ToDoTask) {
    this.isLoading = true;
    const formData = new FormData();
    task.status = !task.status;
    formData.append('task', JSON.stringify(task));
    this.todosService.updateTask(formData).subscribe({
      next: () => {
        this.isLoading = false;
        this.taskUpdated.emit(task);
        this.toastr.success('The task updated successfully');
      },
      error: () => {
        this.isLoading = false;
      },
    });
  }

  toggleEdit() {
    this.editTodoStatus = !this.editTodoStatus;
  }

  onDeleteClick() {
    this.deleteStatusChanged.emit();
  }

  updatedTask(task: ToDoTask) {
    this.todoTask = task;
    this.taskUpdated.emit(task);
    this.editViewClosed();
  }

  viewFiles() {
    this.filesClickStatus = true;
  }
  shareTask() {
    this.shareClickStatus = true;
  }
  editTask() {
    this.editTodoStatus = true;
  }
  editViewClosed() {
    this.editTodoStatus = false;
  }

  sharedUsersUpdated(task: ToDoTask) {
    this.todoTask = task;
    this.shareClickStatus = false;
  }
}
