import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';
import { User } from '../../shared/models/auth';
import { retry } from 'rxjs';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent implements OnInit {
  userFullname: string = ' ';
  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.initHome();
  }

  private initHome() {
    if (!localStorage.getItem('fullname')) return;
    this.userFullname = localStorage.getItem('fullname')!;
  }
}
