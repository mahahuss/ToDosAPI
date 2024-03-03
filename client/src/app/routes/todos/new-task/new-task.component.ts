import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../../services/auth.service';
import { TodosService } from '../../../services/todos.service';
import { ToDoTask } from '../../../shared/models/todo';
import { ToastrService } from 'ngx-toastr';
import { SpinnerComponent } from '../../../core/components/spinner/spinner.component';
@Component({
  selector: 'app-new-task',
  standalone: true,
  templateUrl: './new-task.component.html',
  styleUrl: './new-task.component.scss',
  imports: [CommonModule, FormsModule, SpinnerComponent],
})
export class NewTaskComponent {
  content = '';
  @Output() onTaskCreated = new EventEmitter<ToDoTask>();
  files: File[] = [];
  isLoading = false;

  constructor(
    private authService: AuthService,
    private todoService: TodosService,
    private toastr: ToastrService,
  ) {}

  handleFileInput(event: any) {
    this.files = event.target.files as File[];
  }

  addTask() {
    this.isLoading = true;
    const formData = new FormData();
    const userid = this.authService.getCurrentUserFromToken()?.nameid;
    if (this.content && userid) {
      formData.append('taskContent', this.content);
    }

    for (let file of this.files) formData.append('files', file);

    this.todoService.addNewTask(formData).subscribe({
      next: (res) => {
        this.onTaskCreated.emit(res);
        this.content = '';
        this.files = [];
        this.isLoading = false;
        this.toastr.success('The task added successfully');
      },
      error: () => {
        this.isLoading = false;
      },
    });
  }
}
