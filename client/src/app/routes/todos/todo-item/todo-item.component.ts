import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  ElementRef,
  EventEmitter,
  Input,
  OnInit,
  Output,
  Renderer2,
  ViewChild,
  ViewChildren,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToDoTask } from '../../../shared/models/todo';
import { TodosService } from '../../../services/todos.service';
import { ToastrService } from 'ngx-toastr';
import { TodoDialogComponent } from '../todo-dialog/todo-dialog.component';
import { FormsModule } from '@angular/forms';
import { TodoFilesDialogComponent } from '../todo-files-dialog/todo-files-dialog.component';
import { ShareTaskDialogComponent } from '../share-task-dialog/share-task-dialog.component';
import { User } from '../../../shared/models/auth';

@Component({
  selector: 'app-todo-item',
  standalone: true,
  templateUrl: './todo-item.component.html',
  styleUrl: './todo-item.component.scss',
  imports: [CommonModule, TodoDialogComponent, FormsModule, TodoFilesDialogComponent, ShareTaskDialogComponent],
})
export class TodoItemComponent implements OnInit {
  @Input() todoTask: ToDoTask | undefined = undefined;
  @Input() currentUser: User | undefined = undefined;
  @Output() taskUpdated = new EventEmitter<ToDoTask>();
  @Output() deleteStatusChanged = new EventEmitter();
  content = '';
  updateClickStatus = false;
  taskFilesExistence = false;
  filesClickStatus = false;
  shareClickStatus = false;
  @ViewChild('focusTaskInput') focusTaskInput?: ElementRef;

  constructor(
    private todosService: TodosService,
    private toastr: ToastrService,
    private cdRef: ChangeDetectorRef,
  ) {}

  ngOnInit(): void {
    this.content = this.todoTask!.taskContent;
    this.taskFilesExistence = this.todoTask!.files.length > 0 ? true : false;
  }

  updateStatus(task: ToDoTask) {
    task.status = !task.status;
    this.todosService.updateTask(task).subscribe({
      next: () => {
        this.taskUpdated.emit(task);
        this.toastr.success('The task updated successfully');
      },
    });
  }

  toggleEdit() {
    this.updateClickStatus = !this.updateClickStatus;

    if (this.updateClickStatus) {
      this.cdRef.detectChanges();
      this.focusTaskInput!.nativeElement.focus();
    }
  }

  onDeleteClick() {
    this.deleteStatusChanged.emit();
  }

  updateTask(task: ToDoTask) {
    task.taskContent = this.content;
    this.todosService.updateTask(task).subscribe({
      next: () => {
        this.toggleEdit();
        this.taskUpdated.emit(task);
        this.toastr.success('The task updated successfully');
      },
    });
  }

  viewFiles() {
    this.filesClickStatus = true;
  }
  shareTask() {
    this.shareClickStatus = true;
  }
  filesViewClosed() {
    this.filesClickStatus = false;
  }
  shareViewClosed() {
    this.shareClickStatus = false;
  }
}
