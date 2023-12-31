import { CommonModule } from '@angular/common';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../../services/auth.service';
import { TodosService } from '../../../services/todos.service';
import { AddNewTaskModel, ToDoTask } from '../../../shared/models/todo';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-new-task',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './new-task.component.html',
  styleUrl: './new-task.component.scss',
})
export class NewTaskComponent {
  content = '';
  @Output() onTaskCreated = new EventEmitter<ToDoTask>();
  files: File[] = [];

  constructor(
    private authService: AuthService,
    private todoService: TodosService,
    private toastr: ToastrService,
  ) {}

  handleFileInput(event: any) {
    this.files = event.target.files as File[];
  }

  addTask() {
    const formData = new FormData();
    const userid = this.authService.getCurrentUserFromToken()?.nameid;
    if (this.content && userid) {
      formData.append('createdBy', userid.toString());
      formData.append('taskContent', this.content);
      formData.append('createdBy', 'false');
    }

    Array.from(this.files).forEach((element) => {
      formData.append('files', element);
    });

    this.todoService.addNewTask(formData).subscribe({
      next: (res) => {
        this.onTaskCreated.emit(res);
        this.content = '';
        this.files = [];
        this.toastr.success('The task added successfully');
      },
    });
  }
}
