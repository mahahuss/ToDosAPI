import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToDoTask, UserTaskFile } from '../../../shared/models/todo';
import { TodosService } from '../../../services/todos.service';
import { environment } from '../../../../environments/environment.development';
import { DomSanitizer } from '@angular/platform-browser';

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
  attachmentInfo: any;
  constructor(
    private todosService: TodosService,
    private sanitizer: DomSanitizer,
  ) {}

  ngOnInit(): void {}

  onClose() {
    this.filesViewClosed.emit();
  }

  getFile(attachment: UserTaskFile) {
    this.todosService.getTaskAttachment(attachment.id).subscribe({
      next: (res) => {
        new Blob([res], { type: 'application/' + attachment.fileName.split('.').pop()?.toUpperCase() });
        var downloadURL = window.URL.createObjectURL(res);
        var link = document.createElement('a');
        link.href = downloadURL;
        link.download = attachment.fileName;
        link.click();
      },
      error: (res) => {
        console.log('error: ' + res.error.message);
      },
    });
  }
}
