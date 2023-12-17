import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';
import { User } from '../../shared/models/auth';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent implements OnInit {
  userInfo!: User;

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.initHome();
  }

  private initHome() {
    this.authService.currentUser$.subscribe({
      next: (res) => {
        this.userInfo = res!;
        this.userInfo.given_name = localStorage.getItem('fullname')!;
      },
    });
  }
}
