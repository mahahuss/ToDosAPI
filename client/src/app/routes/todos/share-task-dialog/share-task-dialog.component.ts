import { Component, EventEmitter, HostListener, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShareTask, ToDoTask } from '../../../shared/models/todo';
import { UserToShare } from '../../../shared/models/auth';
import { AuthService } from '../../../services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { FormsModule } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';
import { TodosService } from '../../../services/todos.service';
import { SpinnerComponent } from '../../../core/components/spinner/spinner.component';

@Component({
  selector: 'app-share-task-dialog',
  standalone: true,
  templateUrl: './share-task-dialog.component.html',
  styleUrl: './share-task-dialog.component.scss',
  host: { '(window:keyup.esc)': 'onClose()' },
  imports: [CommonModule, FormsModule, NgSelectModule, SpinnerComponent],
})
export class ShareTaskDialogComponent implements OnInit {
  @Input() todoTask: ToDoTask | undefined = undefined;
  @Output() shareViewClosed = new EventEmitter();
  @Output() sharedUsersUpdated = new EventEmitter();
  usersToShareWith: UserToShare[] = [];
  selectedUsers: UserToShare[] = [];
  isEditable = false;
  isLoading = false;

  constructor(
    private toastr: ToastrService,
    private todosService: TodosService,
    private authService: AuthService,
  ) {}

  async ngOnInit(): Promise<void> {
    await this.loadUsers();
  }
  async loadUsers() {
    const res = await this.authService.getUsersToShare();
    let sharedWithIds = this.todoTask?.sharedTasks
      ? this.todoTask?.sharedTasks.map(({ sharedWith }) => sharedWith)
      : [];
    this.usersToShareWith = res.filter((item) => !sharedWithIds.includes(item.id));
    this.selectedUsers = res.filter((item) => sharedWithIds.includes(item.id));
  }

  @HostListener('window:keyup.esc')
  onClose() {
    this.shareViewClosed.emit();
  }

  shareTaskWithUsers() {
    this.isLoading = true;

    const sharedtask: ShareTask = {
      taskId: this.todoTask!.id,
      isEditable: this.isEditable,
      sharedWith: this.selectedUsers.map((user) => user.id),
    };

    this.todosService.shareTask(sharedtask).subscribe({
      next: () => {
        this.toastr.success('shared successfully');

        if (sharedtask.sharedWith.length == 0) this.todoTask!.sharedTasks = [];
        else {
          this.todoTask!.sharedTasks = [];
          for (let id of sharedtask.sharedWith) {
            this.todoTask!.sharedTasks.push({
              id: new Date().getTime(),
              taskId: sharedtask.taskId,
              isEditable: sharedtask.isEditable,
              sharedDate: new Date(),
              sharedWith: id,
            });
          }
        }
        this.sharedUsersUpdated.emit(this.todoTask);
      },
    });
  }
}
