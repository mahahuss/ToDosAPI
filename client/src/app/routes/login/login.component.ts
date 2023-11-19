import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators
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

  constructor(private authService: AuthService, private route: Router) {}

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
        next: () => {
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
