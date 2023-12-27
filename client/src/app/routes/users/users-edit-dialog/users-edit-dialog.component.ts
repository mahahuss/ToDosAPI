import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../services/auth.service';
import { TodosService } from '../../../services/todos.service';
import { ToastrService } from 'ngx-toastr';
import { Role, UpdateUser, UserInfo } from '../../../shared/models/auth';
import { FormsModule } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';

@Component({
  selector: 'app-users-edit-dialog',
  standalone: true,
  imports: [CommonModule, FormsModule, NgSelectModule],
  templateUrl: './users-edit-dialog.component.html',
  styleUrl: './users-edit-dialog.component.scss',
})
export class UsersEditDialogComponent implements OnInit {
  @Input() user: UserInfo | undefined;
  @Output() editViewClosed = new EventEmitter();
  @Output() userUpdated = new EventEmitter();

  roles: Role[] = [];
  userRoles: Role[] = [];
  userInfo: UpdateUser | undefined = undefined;

  constructor(
    private authService: AuthService,
    private todosService: TodosService,
    private toastr: ToastrService,
  ) {}
  ngOnInit(): void {
    this.loadRoles();
    this.loadUserRoles();
  }
  loadUserRoles() {
    this.authService.getUserRoles(this.user!.id).subscribe({
      next: (res) => {
        this.userRoles = res;
        console.log(res);
      },
      error: (err) => {
        console.log(err.error.message);
      },
    });
  }
  loadRoles() {
    this.authService.getRoles().subscribe({
      next: (res) => {
        this.roles = res;
        console.log(res);
      },
      error: (err) => {
        console.log(err.error.message);
      },
    });
  }

  onClose() {
    this.editViewClosed.emit();
  }

  updateUser() {
    this.userInfo = {
      id: this.user!.id,
      fullname: this.user!.fullName,
      roles: this.userRoles,
    };
    console.log(this.userInfo);

    this.authService.updateUser(this.userInfo).subscribe({
      next: (res) => {
        this.userUpdated.emit(this.userInfo);
        this.onClose();
      },
      error: (err) => {
        console.log(err.error.message);
      },
    });
  }
}
