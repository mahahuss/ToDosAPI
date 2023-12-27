import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../../../services/auth.service';
import { Role, UpdateUser, UserInfo } from '../../../shared/models/auth';

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

  constructor(
    private authService: AuthService,
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
      },
    });
  }

  loadRoles() {
    this.authService.getRoles().subscribe({
      next: (res) => {
        this.roles = res;
      },
    });
  }

  onClose() {
    this.editViewClosed.emit();
  }

  updateUser() {
    const userInfo: UpdateUser = {
      id: this.user!.id,
      fullname: this.user!.fullName,
      roles: this.userRoles,
    };

    this.authService.updateUser(userInfo).subscribe({
      next: () => {
        this.toastr.success('updated successfully');
        this.userUpdated.emit(userInfo);
        this.onClose();
      },
    });
  }
}
