import { Component, OnInit, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../services/auth.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.scss'
})


export class NavComponent{

  isAdmin: boolean = true;

  constructor() { 
  }

  // ngOnInit(): void {
  //    this.initNav();
  // }

  // initNav() {
  //   this.authService.currentUser$.subscribe({
  //     next: (res) => {
  //       this.isAdmin = res!.roles.includes("Admin")
  //       console.log(this.isAdmin)
  //     },
  //   });  
  // }

 


}
