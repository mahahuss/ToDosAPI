import { Routes } from '@angular/router';
import { LoginComponent } from './routes/login/login.component';
import { HomeComponent } from './routes/home/home.component';
import { guardAuthGuard } from './services/guard-auth.guard';

export const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent,
  },
  
  {
    path: 'home',
    component: HomeComponent,
    canActivate: [guardAuthGuard]
  },
];
