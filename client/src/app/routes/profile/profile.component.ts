import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { User, UserProfile } from '../../shared/models/auth';
import { AuthService } from '../../services/auth.service';
import { environment } from '../../../environments/environment.development';
import { FileTypes } from 'glob/dist/commonjs/glob';
import { FormsModule } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
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

  constructor(private authService: AuthService, private toastr: ToastrService) {}
  ngOnInit(): void {
    this.initProfile();
  }

  private initProfile() {
    this.authService.currentUser$.subscribe({
      next: (res) => {
        this.userInfo = res!;
        console.log("from initprofile: "+res?.given_name);
        
        this.photoPath = environment.apiUrl + 'users/images/' + this.userInfo.nameid;
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
    if (this.fileToUpload && this.fileToUpload.size < 200000 ) {
      formData.append('Image', this.fileToUpload);
    }
    formData.append('Name', this.name);
    this.authService.updateUserProfile(formData).subscribe({
      next: (result) => {
        this.userInfo.given_name=this.name;
        this.authService.updateCurrentUser(this.userInfo!);
        this.updateClickStatus = false;
        this.toastr.success(result.message)
        // this.check();
      },
      error: (res) => {
        this.toastr.success(res.message)
      },
    });
  }




  // check() {
  //   this.authService.currentUser$.subscribe({
  //     next: (res) => {
  //       if (res) {
  //         console.log("after update : "+res.given_name);
  //       }
  //     },
  //     error: (err) => {
  //       console.log(err.message);
  //     },
  //   });
  // }
}
