import { Injectable } from '@angular/core';
import { CanActivate, Router, UrlTree } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  private isLoggedIn = true; // Hardcoded for now

  constructor(private router: Router) {}

  canActivate(): boolean | UrlTree {
    if (this.isLoggedIn) {
      return true;
    }
    return this.router.parseUrl('/');
  }
}
