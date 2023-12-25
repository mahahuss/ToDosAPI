import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { User, UserInfo } from '../../shared/models/auth';
import { AuthService } from '../../services/auth.service';
import { UsersTasksDialogComponent } from './users-tasks-dialog/users-tasks-dialog.component';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-users',
  standalone: true,
  imports: [CommonModule, UsersTasksDialogComponent],
  templateUrl: './users.component.html',
  styleUrl: './users.component.scss',
})
export class UsersComponent implements OnInit {
  userInfo!: User;
  isAdmin = false;
  tasksViewStatus = false;
  users: UserInfo[] = [];
  crrrentUserId: number | undefined;
  constructor(
    private authService: AuthService,
    private toastr: ToastrService,
  ) {}

  ngOnInit(): void {
    this.authService.currentUser$.subscribe({
      next: (res) => {
        this.userInfo = res!;
        this.isAdmin = this.userInfo!.roles.includes('Admin');
      },
    });
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

  changeUserStatus(userId: number, status: boolean) {
    status = status ? false : true;

    this.authService.changeUserStatus(userId, status).subscribe({
      next: (res) => {
        const indexToUpdate = this.users.findIndex((user) => user.id === userId);
        if (indexToUpdate !== -1) this.users[indexToUpdate].status = status;
      },
      error: (res) => {
        this.toastr.error(res.error.message);
      },
    });
  }

  viewTasks(userId: number) {
    this.tasksViewStatus = !this.tasksViewStatus;
    this.crrrentUserId = userId;
  }

  tasksViewClosed() {
    this.tasksViewStatus = false;
  }
}
