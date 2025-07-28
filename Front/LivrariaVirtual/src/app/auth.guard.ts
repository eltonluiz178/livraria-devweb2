// src/app/auth.guard.ts
import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private router: Router) {}

  canActivate(): boolean {
    const token = this.getTokenFromCookie();

    if (!token || this.isTokenExpired(token)) {
      this.router.navigate(['/login']);
      return false;
    }

    return true;
  }

  private getTokenFromCookie(): string | null {
    const matches = document.cookie.match(/(?:^|; )jwtToken=([^;]*)/);
    return matches ? decodeURIComponent(matches[1]) : null;
  }

  private isTokenExpired(token: string): boolean {
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      const exp = payload.exp;
      return Date.now() >= exp * 1000;
    } catch {
      return true; // token inválido ou erro ao decodificar
    }
  }
}
