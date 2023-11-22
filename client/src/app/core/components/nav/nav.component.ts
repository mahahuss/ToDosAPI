import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.scss',
})
export class NavComponent {
  isAdmin = false;

  constructor(public authService: AuthService) {}

  ngOnInit(): void {
    this.initNav();
  }

  initNav() {
    const user = this.authService.getCurrentUserFromToken();
    if (user) this.isAdmin = user.roles.includes('admin');
  }
}
