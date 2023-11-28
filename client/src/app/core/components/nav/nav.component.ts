import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
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
  isLoggedin = false;

  constructor(
    public authService: AuthService,
    private router: Router,
  ) {}

  ngOnInit(): void {
    this.initNav();
  }

  initNav() {
    // const user = this.authService.getCurrentUserFromToken();
    // if (user) {
    //   this.isAdmin = user.roles.includes('admin');
    // }
    
    this.authService.currentUser$.subscribe({
      next: (res) => {
        this.isAdmin = res!.roles.includes('Admin');
        console.log(this.isAdmin)
        this.isLoggedin = true;
      
      },
    });
  }

  Logout() {
    localStorage.removeItem('token');
    this.isLoggedin = false;
    this.router.navigate(['/login']);
  }
}
