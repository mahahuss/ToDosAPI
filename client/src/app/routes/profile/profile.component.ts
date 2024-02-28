import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { environment } from '../../../environments/environment.development';
import { AuthService } from '../../services/auth.service';
import { User } from '../../shared/models/auth';
import { SpinnerComponent } from '../../core/components/spinner/spinner.component';

@Component({
  selector: 'app-profile',
  standalone: true,
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss',
  imports: [CommonModule, FormsModule, SpinnerComponent],
})
export class ProfileComponent implements OnInit {
  userInfo: User | undefined;
  photoPath = '';
  name = '';
  timeStamp = '';
  updateClickStatus = false;
  fileToUpload: File | undefined = undefined;
  avatarPreview = false;
  previewSrc = '';
  isLoading = false;

  constructor(
    public authService: AuthService,
    private toastr: ToastrService,
  ) {}
  ngOnInit(): void {
    this.initProfile();
  }

  private initProfile() {
    this.authService.currentUser$.subscribe({
      next: (res) => {
        if (!res) return;
        this.userInfo = res;
        this.userInfo.roles = Array.isArray(this.userInfo.roles) ? this.userInfo.roles : [this.userInfo.roles];
        this.setPhotoPath();
      },
    });
  }

  toggleEdit() {
    if (!this.userInfo) return;
    this.avatarPreview = false;
    this.name = this.userInfo.given_name;
    this.updateClickStatus = !this.updateClickStatus;
  }

  handleFileInput(event: any) {
    this.fileToUpload = event.target.files[0] as File;
    this.previewSrc = URL.createObjectURL(this.fileToUpload);
    this.avatarPreview = true;
  }

  editProfile() {
    this.isLoading = true;
    if (!this.userInfo) {
      this.isLoading = false;
      return;
    }
    const formData = new FormData();
    if (this.fileToUpload && this.fileToUpload.size < 200000) {
      formData.append('Image', this.fileToUpload);
    }
    formData.append('Name', this.name);
    this.authService.updateUserProfile(formData).subscribe({
      next: (result) => {
        this.setPhotoPath();
        this.updateClickStatus = false;
        localStorage.setItem('fullname', this.name);
        this.userInfo!.given_name = this.name;
        this.avatarPreview = false;
        this.isLoading = false;
        this.toastr.success(result.message);
      },
    });
  }

  setPhotoPath() {
    if (!this.userInfo) return;

    this.authService.getUserPhoto().subscribe({
      next: (result) => {
        this.photoPath = 'data:image/png;base64,' + result.fileBase64;
      },
    });
  }
}
