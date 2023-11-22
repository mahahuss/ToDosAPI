import { Component } from '@angular/core';
import {
    RouterOutlet
} from '@angular/router';
import { NavComponent } from './core/components/nav/nav.component';
import { AuthService } from './services/auth.service';

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
      this.authService.setCurrentUserFromToken();
    }
  }
}
