import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToDoTask } from '../../../shared/models/todo';
import { User, UserInfo } from '../../../shared/models/auth';
import { AuthService } from '../../../services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { AutoCompleteCompleteEvent, AutoCompleteModule } from 'primeng/autocomplete';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-share-task-dialog',
  standalone: true,
  imports: [CommonModule, FormsModule, AutoCompleteModule],
  templateUrl: './share-task-dialog.component.html',
  styleUrl: './share-task-dialog.component.scss',
})
export class ShareTaskDialogComponent implements OnInit {
  @Input() todoTask: ToDoTask | undefined = undefined;
  @Output() shareViewClosed = new EventEmitter();
  users: UserInfo[] = [];
  usersSuggestions: UserInfo[] = [];
  selectedUsers: UserInfo[] = [];

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

  filterUsers($event: any) {
    this.usersSuggestions = this.users.filter(
      (user) => user.fullName.toLocaleLowerCase().search($event.query.toLocaleLowerCase()) > -1,
    );
    console.log('before:' + this.users);

    // this.users = this.users.filter((x) => this.selectedUsers.find((y) => y.fullName != x.fullName));

    console.log('after:' + this.selectedUsers);
  }

  onSelect(event: any) {
    console.log('onSelect' + event);
  }
}
