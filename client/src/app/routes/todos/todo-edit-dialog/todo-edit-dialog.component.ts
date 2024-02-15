import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ToDoTask } from '../../../shared/models/todo';

@Component({
  selector: 'app-todo-edit-dialog',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './todo-edit-dialog.component.html',
  styleUrl: './todo-edit-dialog.component.scss',
})
export class TodoEditDialogComponent {
  @Input() todoTask: ToDoTask | undefined = undefined;
  @Output() editViewClosed = new EventEmitter();
  words = [
    'hjgsajfvjvfjavfja',
    'hjgsajfvjvfjavegfewgfwwrqfja',
    'qwdwqqdwqfqfwq',
    'hjgwfwfwfwfsajfvjvfjavfja',
    'hjgsajfvwffjvfjavfja',
  ];

  content = '';
  onClose() {
    this.editViewClosed.emit();
  }
}
