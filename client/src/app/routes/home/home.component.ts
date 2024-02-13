import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent implements OnInit {
  userFullname: string = ' ';
  ngOnInit(): void {
    this.initHome();
  }

  private initHome() {
    if (!localStorage.getItem('fullname')) return;
    this.userFullname = localStorage.getItem('fullname')!;
  }
}
