import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { User, UserProfile } from '../../shared/models/auth';
import { AuthService } from '../../services/auth.service';
import { environment } from '../../../environments/environment.development';
import { FileTypes } from 'glob/dist/commonjs/glob';
import { FormsModule } from '@angular/forms';
// import path from 'path';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss',
})
export class ProfileComponent implements OnInit {
  userInfo!: User;
  photoPath = '';
  name = '';
  updateClickStatus = false;
  fileToUpload: File | undefined = undefined;

  constructor(private authService: AuthService) {}
  ngOnInit(): void {
    this.initProfile();
  }

  private initProfile() {
    this.authService.currentUser$.subscribe({
      next: (res) => {
        this.userInfo = res!;
        this.photoPath = environment.apiUrl + 'users/' + this.userInfo.nameid;
      },
    });
  }

  toggleEdit() {
    this.name = this.userInfo.given_name;
    this.updateClickStatus = !this.updateClickStatus;
  }

  handleFileInput(event: any) {
    this.fileToUpload = event.target.files[0] as File;
  }

  editProfile() {
    const formData = new FormData();
    if (this.fileToUpload) {
      formData.append('Image', this.fileToUpload);
    }
    formData.append('Name', this.name);
    this.authService.updateUserProfile(formData).subscribe({
      next: () => {},
      error: (res) => {
        console.log(res.error.message);
      },
    });
  }
}
