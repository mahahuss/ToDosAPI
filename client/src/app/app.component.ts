import { Component } from '@angular/core';
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
export class AppComponent {
  constructor(private authService: AuthService) {
    if (this.authService.isTokenValid()) {
      //  this.authService.setCurrentUserFromToken();
      // this.authService.refreshToken()
      this.authService.refreshToken().subscribe({});
    }
  }

  timer = setInterval(() => {
    var currentExpiryTime = jwtDecode(localStorage.getItem('token')!).exp!;
    var fiveMinBefore = new Date(currentExpiryTime - 300000);
    var timeNow = new Date(Date.now());

    if (fiveMinBefore > timeNow) this.authService.refreshToken().subscribe({});
  }, 1000);
}
