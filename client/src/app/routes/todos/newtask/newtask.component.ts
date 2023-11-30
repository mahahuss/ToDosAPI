import { Component, EventEmitter, Output, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddNewTaskModel } from '../../../shared/models/auth';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-newtask',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './newtask.component.html',
  styleUrl: './newtask.component.scss'
})
export class NewtaskComponent {
  task : AddNewTaskModel = {TaskContent:"", CreatedBy:0, status:0};
  @Output() addNewTask = new EventEmitter<{ task: AddNewTaskModel }>();

  constructor(
    private authService: AuthService,
  ) {}

  
AddTask() {
  const Input = document.getElementById('taskContent') as HTMLInputElement | null;
  const content = Input?.value
  console.log("content "+content)
  const userid = this.authService.getCurrentUserFromToken()?.nameid;
  if (content && userid){
  this.task!.TaskContent=content! as string;
  this.task!.CreatedBy = userid!;
  this.task!.status = 0;
  this.addNewTask.emit( { task: this.task! });
  (document.getElementById('taskContent') as HTMLInputElement).value="";
  }
}

// ngOnChanges(changes: SimpleChanges) {
//   console.log("changes here : "+ changes['todoTask'])
// }


}
