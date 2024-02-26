import { AfterContentChecked, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UpdateUser, User, UserInfo } from '../../shared/models/auth';
import { AuthService } from '../../services/auth.service';
import { UsersTasksDialogComponent } from './users-tasks-dialog/users-tasks-dialog.component';
import { UsersEditDialogComponent } from './users-edit-dialog/users-edit-dialog.component';
import { LoaderService } from '../../services/loader.service';
import { SpinnerComponent } from '../../core/components/spinner/spinner.component';
@Component({
  selector: 'app-users',
  standalone: true,
  templateUrl: './users.component.html',
  styleUrl: './users.component.scss',
  imports: [CommonModule, UsersTasksDialogComponent, UsersEditDialogComponent, SpinnerComponent],
})
export class UsersComponent implements OnInit, AfterContentChecked {
  userInfo!: User;
  isAdmin = false;
  tasksViewStatus = false;
  editViewStatus = false;
  user: UserInfo | undefined;
  users: UserInfo[] = [];
  currentUserId: number | undefined;
  constructor(
    private authService: AuthService,
    public loaderService: LoaderService,
    private cdRef: ChangeDetectorRef,
  ) {}

  ngOnInit(): void {
    this.authService.currentUser$.subscribe({
      next: (res) => {
        if (!res) return;

        this.userInfo = res;
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

  ngAfterContentChecked() {
    // this.cdRef.detectChanges();
  }

  changeUserStatus(userId: number, status: boolean) {
    status = status ? false : true;

    this.authService.changeUserStatus(userId, status).subscribe({
      next: () => {
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
    if (indexToUpdate !== -1 && this.users[indexToUpdate].roles) {
      this.users[indexToUpdate].fullName = user.fullName;
      this.users[indexToUpdate].roles.clear();
      for (let role of user.roles) this.users[indexToUpdate].roles.set(role.id, role.userType);
    }
  }
}
