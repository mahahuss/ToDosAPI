import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavComponent } from './core/components/nav/nav.component';
import { AuthService } from './services/auth.service';
import { jwtDecode } from 'jwt-decode';

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  imports: [RouterOutlet, NavComponent],
})
export class AppComponent implements OnInit {
  constructor(private authService: AuthService) {
    this.authService.startRefreshTokenInterval();
    if (this.authService.isTokenValid()) {
      this.authService.refreshToken().subscribe(() => {
        this.authService.getUsersToShare().subscribe();
      });
    }
  }

  ngOnInit(): void {}
}
