import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class LoaderService {
  isLoading$ = signal(false);

  constructor() {}

  loader(val: boolean) {
    this.isLoading$.set(val);
  }
}
