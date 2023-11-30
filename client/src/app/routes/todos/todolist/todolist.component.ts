import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToDoTask } from '../../../shared/models/auth';

@Component({
  selector: 'app-todolist',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './todolist.component.html',
  styleUrl: './todolist.component.scss',
})
export class TodolistComponent {
  @Input() todoTask: ToDoTask | undefined = undefined;
  @Output() updateTask = new EventEmitter<{ task: ToDoTask }>();
  @Output() deleteTask = new EventEmitter<{ task: ToDoTask }>();

  constructor() {}

  onTaskClick(task: ToDoTask) {
    this.updateTask.emit({ task: task });
  }

  onDeleteClick(task: ToDoTask) {
    this.deleteTask.emit({ task: task });
  }
}
