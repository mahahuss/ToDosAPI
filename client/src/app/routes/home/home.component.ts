import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';

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
  constructor() {}

  private initHome() {
    if (!localStorage.getItem('fullname')) return;
    this.userFullname = localStorage.getItem('fullname')!;
  }
}
