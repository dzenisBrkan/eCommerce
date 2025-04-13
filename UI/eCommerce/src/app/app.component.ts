import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'eCommerce';
  User: any;
  
  constructor(private router: Router) {}

  ngOnInit(): void {
    const userJson = localStorage.getItem('User');
    if (userJson) {
    this.User = JSON.parse(userJson);
    }
    
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('access_token');
  }

  logout() {
    localStorage.removeItem('access_token');
    localStorage.removeItem('User');

    this.router.navigate(['/login']).then(() => {
      window.location.reload();
    });
  }
}
