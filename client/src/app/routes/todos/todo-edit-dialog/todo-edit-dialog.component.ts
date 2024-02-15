import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ToDoTask, UserTaskFile } from '../../../shared/models/todo';
import { ToastrService } from 'ngx-toastr';
import { TodosService } from '../../../services/todos.service';

@Component({
  selector: 'app-todo-edit-dialog',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './todo-edit-dialog.component.html',
  styleUrl: './todo-edit-dialog.component.scss',
})
export class TodoEditDialogComponent implements OnInit {
  @Input() todoTask: ToDoTask | undefined = undefined;
  @Output() editViewClosed = new EventEmitter();
  @Output() taskUpdated = new EventEmitter();
  todoTaskCopy: ToDoTask | undefined;
  files: File[] = [];
  errorMessage = false;

  constructor(
    private toastr: ToastrService,
    private todosService: TodosService,
  ) {}

  ngOnInit(): void {
    this.todoTaskCopy = JSON.parse(JSON.stringify(this.todoTask));
  }

  handleFileInput(event: any) {
    this.files = event.target.files as File[];
    for (let file of this.files) {
      this.todoTaskCopy?.files.push({
        id: new Date().getTime(),
        taskId: this.todoTaskCopy.id,
        fileName: file.name,
      });
    }
  }

  removeFile(file: UserTaskFile) {
    this.todoTaskCopy!.files = this.todoTaskCopy!.files.filter((item) => item.id !== file!.id);
  }

  editTask() {
    if (this.todoTaskCopy?.taskContent.length == 0) {
      this.errorMessage = true;
      return;
    }
    this.errorMessage = false;
    const formData = new FormData();
    formData.append('id', String(this.todoTaskCopy!.id));
    formData.append('status', String(this.todoTaskCopy!.status));
    formData.append('createdBy', String(this.todoTaskCopy!.createdBy));
    formData.append('taskContent', this.todoTaskCopy!.taskContent);
    for (let file of this.files) formData.append('files', file);

    this.todosService.updateTask(formData).subscribe({
      next: () => {
        this.toastr.success('The task updated successfully');
        this.taskUpdated.emit(this.todoTaskCopy);
      },
    });
  }

  onClose() {
    this.todoTaskCopy = JSON.parse(JSON.stringify(this.todoTask));
    this.editViewClosed.emit();
  }
}
