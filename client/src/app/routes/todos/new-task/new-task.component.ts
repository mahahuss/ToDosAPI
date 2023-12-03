import { CommonModule } from '@angular/common';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../../services/auth.service';
import { TodosService } from '../../../services/todos.service';
import { AddNewTaskModel, ToDoTask } from '../../../shared/models/auth';
@Component({
  selector: 'app-new-task',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './new-task.component.html',
  styleUrl: './new-task.component.scss'
})
export class NewTaskComponent {
  content = '';
  @Output() onTaskCreated = new EventEmitter<ToDoTask>();

  constructor(
    private authService: AuthService,
    private todoService: TodosService,
  ) {}

  addTask() {
    const userid = this.authService.getCurrentUserFromToken()?.nameid;
    console.log(this.content)
    if (this.content && userid) {
      const task: AddNewTaskModel = {
        createdBy: userid,
        taskContent: this.content,
        status: 0,
      };
      this.todoService.addNewTask(task).subscribe({
        next: (res) => {
          this.onTaskCreated.emit(res);
          this.content = '';
        },
        error: (res) => {
          console.log(res.error.message);
        },
      });
    }
  }
}
