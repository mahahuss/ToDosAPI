import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UpdateUser, User, UserInfo } from '../../shared/models/auth';
import { AuthService } from '../../services/auth.service';
import { UsersTasksDialogComponent } from './users-tasks-dialog/users-tasks-dialog.component';
import { ToastrService } from 'ngx-toastr';
import { UsersEditDialogComponent } from './users-edit-dialog/users-edit-dialog.component';
@Component({
  selector: 'app-users',
  standalone: true,
  imports: [CommonModule, UsersTasksDialogComponent, UsersEditDialogComponent],
  templateUrl: './users.component.html',
  styleUrl: './users.component.scss',
})
export class UsersComponent implements OnInit {
  userInfo!: User;
  isAdmin = false;
  tasksViewStatus = false;
  editViewStatus = false;
  user: UserInfo | undefined;
  users: UserInfo[] = [];
  currentUserId: number | undefined;
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
    });
  }

  changeUserStatus(userId: number, status: boolean) {
    status = status ? false : true;

    this.authService.changeUserStatus(userId, status).subscribe({
      next: (res) => {
        const indexToUpdate = this.users.findIndex((user) => user.id === userId);
        if (indexToUpdate !== -1) this.users[indexToUpdate].status = status;
      },
    });
  }

  viewTasks(userId: number) {
    this.tasksViewStatus = !this.tasksViewStatus;
    this.currentUserId = userId;
  }

  tasksViewClosed() {
    this.tasksViewStatus = false;
  }

  editViewClosed() {
    this.editViewStatus = false;
  }

  openEditDialog(user: UserInfo) {
    this.editViewStatus = !this.editViewStatus;
    this.user = user;
  }

  userUpdated(user: UpdateUser) {
    const indexToUpdate = this.users.findIndex((user) => user.id === user.id);
    if (indexToUpdate !== -1) {
      this.users[indexToUpdate].fullName = user.fullname;
      this.users[indexToUpdate].roles.clear();
      user.roles.forEach((role) => this.users[indexToUpdate].roles.set(role.id, role.userType));
    }
  }
}
