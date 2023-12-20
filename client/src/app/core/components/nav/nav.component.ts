import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../../services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.scss',
})
export class NavComponent {
  isAdmin = false;
  isModerator = false;
  isLoggedin = false;

  constructor(
    public authService: AuthService,
    private router: Router,
    private toastr: ToastrService,
  ) {}

  ngOnInit(): void {
    this.initNav();
  }

  initNav() {
    this.authService.currentUser$.subscribe({
      next: (res) => {
        if (res) {
          this.isAdmin = res!.roles.includes('Admin');
          this.isModerator = res!.roles.includes('Moderator');
          this.isLoggedin = true;
        }
      },
      error: (err) => {
        this.toastr.error(err.error.message);
      },
    });
  }

  Logout() {
    localStorage.removeItem('token');
    this.isLoggedin = false;
    this.router.navigate(['/login']);
  }
}
