import {
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
import { UserInfo } from '../../../shared/models/auth';

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
  ],
})
export class TodoItemComponent implements OnInit {
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
  @ViewChild('focusTaskInput') focusTaskInput?: ElementRef;

  constructor(
    private todosService: TodosService,
    private toastr: ToastrService,
    private cdRef: ChangeDetectorRef,
  ) {}

  ngOnInit(): void {
    this.taskFilesExistence = this.todoTask!.files.length > 0 ? true : false;
  }

  updateStatus(task: ToDoTask) {
    const formData = new FormData();
    task.status = !task.status;
    formData.append('taskContent', task.taskContent);
    formData.append('status', String(task.status));
    formData.append('id', String(task.id));
    this.todosService.updateTask(formData).subscribe({
      next: () => {
        this.taskUpdated.emit(task);
        this.toastr.success('The task updated successfully');
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
  filesViewClosed() {
    this.filesClickStatus = false;
  }
  shareViewClosed() {
    this.shareClickStatus = false;
  }
  editViewClosed() {
    this.editTodoStatus = false;
  }
}
