import { Routes } from '@angular/router';
import { LoginComponent } from './routes/login/login.component';
import { HomeComponent } from './routes/home/home.component';
import { guardAuthGuard } from './core/gaurd/guard-auth.guard';
import { loggedInAuthGuardGuard } from './core/gaurd/logged-in-auth-guard.guard';
import { ProfileComponent } from './routes/profile/profile.component';
import { TodosComponent } from './routes/todos/todos.component';
import { AppComponent } from './app.component';


export const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent,
    canActivate: [loggedInAuthGuardGuard]
  },
  {
    path: 'home',
    component: HomeComponent,
    canActivate: [guardAuthGuard]
  },
  {
    path: 'profile',
    component: ProfileComponent,
    canActivate: [guardAuthGuard]

  },
  {
    path: 'todos',
    component: TodosComponent,
    canActivate: [guardAuthGuard]

  },
  { path: '**', redirectTo: 'login' }
];
