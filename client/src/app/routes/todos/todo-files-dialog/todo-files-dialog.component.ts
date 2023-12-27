import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { TodosService } from '../../../services/todos.service';
import { ToDoTask, UserTaskFile } from '../../../shared/models/todo';
import { ToastrService } from 'ngx-toastr';
import { saveAs } from 'file-saver';

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
    private todosService: TodosService,
    private toastr: ToastrService,
  ) {}

  ngOnInit(): void {}

  onClose() {
    this.filesViewClosed.emit();
  }

  getFile(attachment: UserTaskFile) {
    this.todosService.getTaskAttachment(attachment.id).subscribe({
      next: (res) => {
        saveAs.saveAs(res, attachment.fileName);
      },
    });
  }
}
