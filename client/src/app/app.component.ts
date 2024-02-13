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
    if (this.authService.isTokenValid()) {
      setInterval(() => {
        const currentExpiryTime = jwtDecode(localStorage.getItem('token')!).exp!;
        const fiveMinBefore = new Date(currentExpiryTime * 1000 - 30_000);
        const timeNow = new Date();

        if (fiveMinBefore < timeNow) {
          this.authService.refreshToken().subscribe();
        }
      }, 1000 * 30);
      this.authService.refreshToken().subscribe();
    }
  }

  ngOnInit(): void {}
}
