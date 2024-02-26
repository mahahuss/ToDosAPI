import { Component, EventEmitter, HostListener, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ToDoTask, UserTaskFile, UserFile } from '../../../shared/models/todo';
import { ToastrService } from 'ngx-toastr';
import { TodosService } from '../../../services/todos.service';

@Component({
  selector: 'app-todo-edit-dialog',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './todo-edit-dialog.component.html',
  styleUrl: './todo-edit-dialog.component.scss',
  host: { '(window:keyup.esc)': 'onClose()' },
})
export class TodoEditDialogComponent implements OnInit {
  @Input() todoTask: ToDoTask | undefined = undefined;
  @Output() editViewClosed = new EventEmitter();
  @Output() taskUpdated = new EventEmitter();
  todoTaskCopy: ToDoTask | undefined;
  files: UserFile[] = [];
  errorMessage = false;

  constructor(
    private toastr: ToastrService,
    private todosService: TodosService,
  ) {}

  ngOnInit(): void {
    this.todoTaskCopy = structuredClone(this.todoTask);
    this.files = this.todoTaskCopy!.files.map((f) => ({
      fileName: f.fileName,
      id: f.id,
      taskId: f.taskId,
      isOld: true,
    }));
  }

  handleFileInput(event: any) {
    const files = event.target.files as File[];
    for (let file of files) {
      this.files.push({
        id: new Date().getTime(),
        taskId: this.todoTaskCopy!.id,
        fileName: file.name,
        file,
        isOld: false,
      });
    }
  }

  removeFile(file: UserTaskFile) {
    this.todoTaskCopy!.files = this.todoTaskCopy!.files.filter((item) => item.id !== file!.id);
    this.files = this.files.filter((item) => item.id !== file!.id); // I guess ?
  }

  editTask() {
    if (this.todoTaskCopy?.taskContent.length == 0) {
      this.errorMessage = true;
      return;
    }
    this.errorMessage = false;
    const formData = new FormData();
    var todoTaskBeforeFilter = structuredClone(this.todoTaskCopy);
    todoTaskBeforeFilter!.files = this.files.map((f) => ({
      id: f.id,
      taskId: f.taskId,
      fileName: f.fileName,
    }));

    this.todoTaskCopy!.files = this.files
      .filter((f) => f.isOld)
      .map((f) => ({
        id: f.id,
        taskId: f.taskId,
        fileName: f.fileName,
      }));
    formData.append('task', JSON.stringify(this.todoTaskCopy));
    for (let file of this.files) {
      if (file.isOld || !file.file) continue;

      formData.append('files', file.file);
    }

    this.todosService.updateTask(formData).subscribe({
      next: () => {
        this.toastr.success('The task updated successfully');
        this.taskUpdated.emit(todoTaskBeforeFilter);
      },
    });
  }

  @HostListener('window:keyup.esc')
  onClose() {
    this.todoTaskCopy = structuredClone(this.todoTask);
    this.editViewClosed.emit();
  }
}
