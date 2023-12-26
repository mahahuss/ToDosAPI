import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToDoTask } from '../../../shared/models/todo';
import { ShareTask, UserInfo } from '../../../shared/models/auth';
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
  isChangeToggle(arg0: string) {
    throw new Error('Method not implemented.');
  }
  @Input() todoTask: ToDoTask | undefined = undefined;
  @Output() shareViewClosed = new EventEmitter();
  users: UserInfo[] = [];
  selectedUsers: UserInfo[] = [];
  isEditable = false;

  constructor(
    private authService: AuthService,
    private toastr: ToastrService,
    private TodosService: TodosService,
  ) {}

  ngOnInit(): void {
    this.loadUsers();
  }
  loadUsers() {
    this.authService.getUsers().subscribe({
      next: (res) => {
        this.users = res;
      },
      error: (res) => {
        this.toastr.error(res.error.message);
      },
    });
  }

  onClose() {
    this.shareViewClosed.emit();
  }

  shareTaskWithUsers() {
    const sharedtask: ShareTask = {
      taskId: this.todoTask!.id,
      isEditable: this.isEditable,
      sharedTo: this.selectedUsers.map((user) => user.id),
    };

    this.TodosService.shareTask(sharedtask).subscribe({
      next: (res) => {
        console.log(res);
        this.onClose();
        this.toastr.success('shred successfully');
      },
      error: (res) => {
        console.log(res.error.message);
        this.toastr.error('faild to share: ' + res.error.message);
      },
    });
  }
}
