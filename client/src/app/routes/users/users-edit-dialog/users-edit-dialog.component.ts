import { CommonModule } from '@angular/common';
import { Component, EventEmitter, HostListener, Input, OnInit, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../../../services/auth.service';
import { Role, UpdateUser, UserInfo } from '../../../shared/models/auth';
import { SpinnerComponent } from '../../../core/components/spinner/spinner.component';

@Component({
  selector: 'app-users-edit-dialog',
  standalone: true,
  templateUrl: './users-edit-dialog.component.html',
  styleUrl: './users-edit-dialog.component.scss',
  host: { '(window:keyup.esc)': 'onClose()' },
  imports: [CommonModule, FormsModule, NgSelectModule, SpinnerComponent],
})
export class UsersEditDialogComponent implements OnInit {
  @Input() user: UserInfo | undefined;
  @Output() editViewClosed = new EventEmitter();
  @Output() userUpdated = new EventEmitter();

  roles: Role[] = [];
  userSelectedRoles: Role[] = [];
  userRoles: Role[] = [];
  errorMessage = false;
  isLoading = true;

  constructor(
    private authService: AuthService,
    private toastr: ToastrService,
  ) {}

  ngOnInit(): void {
    this.loadRoles();
  }

  loadUserRoles() {
    this.authService.getUserRoles(this.user!.id).subscribe({
      next: (res) => {
        this.userRoles = res;
        this.userSelectedRoles = res;
        this.isLoading = false;
      },
      error: () => {
        this.isLoading = false;
      },
    });
  }

  loadRoles() {
    this.authService.getRoles().subscribe({
      next: (res) => {
        this.roles = res;
        this.loadUserRoles();
      },
    });
  }

  editUserRoles() {
    if (this.userRoles.length == 0) {
      this.errorMessage = true;
    } else {
      this.userSelectedRoles = this.userRoles;
      this.errorMessage = false;
    }
  }

  @HostListener('window:keyup.esc')
  onClose() {
    this.editViewClosed.emit();
  }

  updateUser() {
    this.isLoading = true;
    const userInfo: UpdateUser = {
      id: this.user!.id,
      fullName: this.user!.fullName,
      roles: this.userSelectedRoles,
    };

    this.authService.updateUser(userInfo).subscribe({
      next: () => {
        this.toastr.success('updated successfully');
        this.userUpdated.emit(userInfo);
        this.onClose();
      },
      error: () => {
        this.isLoading = false;
      },
    });
  }
}
