import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToDoTask } from '../../../shared/models/todo';
import { UserInfo } from '../../../shared/models/auth';
import { AuthService } from '../../../services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-share-task-dialog',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './share-task-dialog.component.html',
  styleUrl: './share-task-dialog.component.scss',
})
export class ShareTaskDialogComponent implements OnInit {
  @Input() todoTask: ToDoTask | undefined = undefined;
  @Output() shareViewClosed = new EventEmitter();

  users: UserInfo[] = [];

  constructor(
    private authService: AuthService,
    private toastr: ToastrService,
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
}
