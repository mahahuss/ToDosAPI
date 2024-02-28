import { CommonModule } from '@angular/common';
import { Component, EventEmitter, HostListener, Input, OnInit, Output } from '@angular/core';
import { saveAs } from 'file-saver';
import { TodosService } from '../../../services/todos.service';
import { ToDoTask, UserTaskFile } from '../../../shared/models/todo';
import { SpinnerComponent } from '../../../core/components/spinner/spinner.component';

@Component({
  selector: 'app-todo-files-dialog',
  standalone: true,
  templateUrl: './todo-files-dialog.component.html',
  styleUrl: './todo-files-dialog.component.scss',
  host: { '(window:keyup.esc)': 'onClose()' },
  imports: [CommonModule, SpinnerComponent],
})
export class TodoFilesDialogComponent implements OnInit {
  @Input() todoTask: ToDoTask | undefined = undefined;
  @Output() filesViewClosed = new EventEmitter();
  isLoading = false;
  fileIds: Map<number, boolean> = new Map();
  constructor(private todosService: TodosService) {}

  ngOnInit(): void {}

  @HostListener('window:keyup.esc')
  onClose() {
    this.filesViewClosed.emit();
  }

  createPerson() {
    const person = {
      name: 'Maha',
      age: 24,
      height: 12,
    };

    return person;
  }

  getFile(attachment: UserTaskFile) {
    this.fileIds.set(attachment.id, true);
    this.isLoading = true;
    this.todosService.getTaskAttachment(attachment.id).subscribe({
      next: (res) => {
        saveAs(res, attachment.fileName);
        this.isLoading = false;
        this.fileIds.set(attachment.id, false);
      },
      error: () => {
        this.isLoading = false;
        this.fileIds.set(attachment.id, false);
      },
    });
  }
}
