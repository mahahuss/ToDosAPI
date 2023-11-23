import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TodoListComponent } from '../../core/components/todo-list/todo-list.component';


@Component({
  selector: 'app-todos',
  standalone: true,
  imports: [CommonModule, TodoListComponent],
  templateUrl: './todos.component.html',
  styleUrl: './todos.component.scss',
})
export class TodosComponent  {

}
