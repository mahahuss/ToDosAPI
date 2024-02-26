import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptors, withInterceptorsFromDi } from '@angular/common/http';
import { ApplicationConfig } from '@angular/core';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideRouter } from '@angular/router';
import { provideToastr } from 'ngx-toastr';
import { routes } from './app.routes';
import { authInterceptor } from './core/interceptors/auth.interceptor';
import { exceptionsInterceptor } from './core/interceptors/exception.interceptor';
import { delayInterceptor } from './core/interceptors/delay.interceptor';
import { loadingInterceptor } from './core/interceptors/loading.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(
      withInterceptorsFromDi(),
      withInterceptors([authInterceptor, loadingInterceptor, exceptionsInterceptor, delayInterceptor]),
    ),
    provideAnimations(), //required animations providers
    provideToastr(), // Toastr providers
  ],
};
