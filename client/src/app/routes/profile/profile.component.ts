import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { User } from '../../shared/models/auth';
import { AuthService } from '../../services/auth.service';
import { environment } from '../../../environments/environment.development'; 
// import path from 'path';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent implements OnInit{

  userInfo!: User;
  photoPath = "";

  constructor(private authService: AuthService) {}
  ngOnInit(): void {
    this.initProfile();
  }

  private initProfile(){
    this.authService.currentUser$.subscribe({
      next: (res) => {
       this.userInfo=res!;
       this.photoPath = environment.apiUrl+"users/"+ this.userInfo.nameid;
      },
    });
  }
}
