import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent implements OnInit {
  profile = this.authService.currentUser$;

  
  constructor(private authService: AuthService) { } 

  ngOnInit(): void {
    this.initHome();
  }

  private initHome() {
    // console.log("in home "+ localStorage.getItem('token'))
    // this.authService.checkTokenExpiry();
  }
}
