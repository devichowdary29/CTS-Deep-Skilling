import { Injectable } from '@angular/core';

@Injectable() // Intentionally not providedIn: 'root' to demonstrate component-level injection
export class NotificationService {
  private message = '';

  setMessage(msg: string) {
    this.message = msg;
  }

  getMessage(): string {
    return this.message;
  }
}
