import { Component, OnInit } from '@angular/core';
import { UserService } from '../services/user.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-profile',
  standalone: false,
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit {
  user = {
    name: '',
    surname: '',
    email: '',
    password: '',
    dob: '',
    location: '',
    phone: '',
    address: '',
  };

  constructor(
    private userService: UserService,  // Assuming you have a service to get user data
    private http: HttpClient
  ) {}

  ngOnInit() {
    this.loadUserProfile();
  }

  loadUserProfile() {
    const token = localStorage.getItem('access_token'); // Get token from localStorage

    if (!token) {
      console.error('User not authenticated.');
      return;
    }

    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

      // this.userService.getCurrentUserInfo().subscribe(
    
    this.http.get<any>('https://localhost:7221/api/User/current-user-info', { headers }).subscribe(
      (response) => {
        console.log("Renose", response);
        this.user = response;  // Assuming the backend returns user details
      },
      (error) => {
        console.error('Error loading user profile:', error);
      }
    );
  }
}
