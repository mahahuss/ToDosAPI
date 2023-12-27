import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { saveAs } from 'file-saver';
import { TodosService } from '../../../services/todos.service';
import { ToDoTask, UserTaskFile } from '../../../shared/models/todo';

@Component({
  selector: 'app-todo-files-dialog',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './todo-files-dialog.component.html',
  styleUrl: './todo-files-dialog.component.scss',
})
export class TodoFilesDialogComponent implements OnInit {
  @Input() todoTask: ToDoTask | undefined = undefined;
  @Output() filesViewClosed = new EventEmitter();
  constructor(
    private todosService: TodosService
  ) {}

  ngOnInit(): void {}

  onClose() {
    this.filesViewClosed.emit();
  }

  getFile(attachment: UserTaskFile) {
    this.todosService.getTaskAttachment(attachment.id).subscribe({
      next: (res) => {
        saveAs(res, attachment.fileName);
      },
    });
  }
}
