import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToDoTask } from '../../../shared/models/todo';
import { ShareTask, UserToShare } from '../../../shared/models/auth';
import { AuthService } from '../../../services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { FormsModule } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';
import { TodosService } from '../../../services/todos.service';

@Component({
  selector: 'app-share-task-dialog',
  standalone: true,
  imports: [CommonModule, FormsModule, NgSelectModule],
  templateUrl: './share-task-dialog.component.html',
  styleUrl: './share-task-dialog.component.scss',
})
export class ShareTaskDialogComponent implements OnInit {
  @Input() todoTask: ToDoTask | undefined = undefined;
  @Output() shareViewClosed = new EventEmitter();
  usersToShareWith: UserToShare[] = [];
  selectedUsers: UserToShare[] = [];
  isEditable = false;

  constructor(
    private toastr: ToastrService,
    private TodosService: TodosService,
  ) {}

  ngOnInit(): void {
    this.loadUsers();
  }
  loadUsers() {}

  onClose() {
    this.shareViewClosed.emit();
  }

  shareTaskWithUsers() {
    const sharedtask: ShareTask = {
      taskId: this.todoTask!.id,
      isEditable: this.isEditable,
      sharedWith: this.selectedUsers.map((user) => user.id),
    };

    this.TodosService.shareTask(sharedtask).subscribe({
      next: () => {
        this.onClose();
        this.toastr.success('shared successfully');
      },
    });
  }
}
