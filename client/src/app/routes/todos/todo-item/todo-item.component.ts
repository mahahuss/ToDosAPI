import {
  AfterViewInit,
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

@Component({
  selector: 'app-todo-item',
  standalone: true,
  templateUrl: './todo-item.component.html',
  styleUrl: './todo-item.component.scss',
  imports: [CommonModule, TodoDialogComponent, FormsModule],
})
export class TodoItemComponent implements OnInit {
  @Input() todoTask: ToDoTask | undefined = undefined;
  @Output() taskUpdated = new EventEmitter<ToDoTask>();
  @Output() deleteStatusChanged = new EventEmitter();
  content = '';
  updateClickStatus = false;
  taskBgColor = 'white';
  @ViewChild('taskDiv') taskDiv?: ElementRef;
  @ViewChild('focusTaskInput') focusTaskInput?: ElementRef;

  constructor(
    private todosService: TodosService,
    private toastr: ToastrService,
  ) {}


  // ngAfterViewInit(): void {
  //   if (this.focusTaskInput) 
  //     this.focusTaskInput.nativeElement.focus();
  //   }

  ngOnInit(): void {
    this.content = this.todoTask!.taskContent;
    this.taskBgColor = this.todoTask!.status ? '#F3F3F3' : 'white';
    if (this.taskDiv)
      this.taskDiv.nativeElement.style.backgroundColor = this.taskBgColor;
    if (this.focusTaskInput) 
      this.focusTaskInput.nativeElement.focus();
  }



  updateStatus(task: ToDoTask) {
    task.status = !task.status;
    this.taskBgColor = task.status ? '#F3F3F3' : 'white';
    if (this.taskDiv) {
      this.taskDiv.nativeElement.style.backgroundColor = this.taskBgColor;
    }
    this.todosService.updateTask(task).subscribe({
      next: () => {
        this.taskUpdated.emit(task);
        this.toastr.success('The task updated successfully');
      },
      error: (res) => {
        console.log(res.error.message);
      },
    });
  }

  toggleEdit() {
    this.updateClickStatus = !this.updateClickStatus;
    if(this.updateClickStatus && this.focusTaskInput) 
    this.focusTaskInput.nativeElement.focus();
  }

  onDeleteClick() {
    this.deleteStatusChanged.emit();
  }

  updateTask(task: ToDoTask) {
    task.taskContent = this.content;
    this.todosService.updateTask(task).subscribe({
      next: () => {
        this.taskUpdated.emit(task);
        this.toastr.success('The task updated successfully');
        this.toggleEdit();
      },
      error: (res) => {
        console.log(res.error.message);
      },
    });
  }
}
