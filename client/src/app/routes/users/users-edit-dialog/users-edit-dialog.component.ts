import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../services/auth.service';
import { TodosService } from '../../../services/todos.service';
import { ToastrService } from 'ngx-toastr';
import { UserInfo } from '../../../shared/models/auth';
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

  constructor(
    private authService: AuthService,
    private todosService: TodosService,
    private toastr: ToastrService,
  ) {}
  ngOnInit(): void {}

  onClose() {
    this.editViewClosed.emit();
  }
}
