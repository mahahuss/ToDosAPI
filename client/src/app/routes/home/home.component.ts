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
  name = '';
  
  ngOnInit(): void {
    this.initForm();
  }

  private initForm() {
    this.name = localStorage.getItem('name')!;
  }
}
