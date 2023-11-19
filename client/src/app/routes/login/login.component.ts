import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';

import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup<LoginForm>;

  constructor(private authService: AuthService, private jwtHelper :JwtHelperService, private route : Router) {}

  get f() {
    return this.loginForm.controls;
  }

  ngOnInit(): void {
    this.initForm();
  }

  login() {
    this.authService
      .login(this.f.username.value, this.f.password.value)
      .subscribe({
        next: (res) => {
          localStorage.setItem('name', this.jwtHelper.decodeToken(localStorage.getItem("token")!).given_name);
          this.route.navigate(['/home']);
        },
        error: (res) => {
          console.log(res.error.message);
        },
      });
  }

  private initForm() {
    this.loginForm = new FormGroup<LoginForm>({
      username: new FormControl<string>('', {
        validators: [Validators.required, Validators.minLength(3)],
        nonNullable: true,
      }),
      password: new FormControl<string>('', {
        validators: [Validators.required],
        nonNullable: true,
      }),
    });
  }
}

type LoginForm = {
  username: FormControl<string>;
  password: FormControl<string>;
};
