// src/app/guards/auth.guard.ts
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> {
    const isAuthenticated = localStorage.getItem('access_token');  // Example: Check for token in localStorage
    
    if (isAuthenticated) {
      // If the user is authenticated, grant access
      return of(true); // Return Observable<boolean> type
    } else {
      // If the user is not authenticated, redirect to login and deny access
      this.router.navigate(['/login']);
      return of(false); // Return Observable<boolean> type
    }
  }
}
