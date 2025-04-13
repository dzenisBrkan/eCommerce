import { Component, OnInit } from '@angular/core';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-profile',
  standalone: false,
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit{
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

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.userService.getCurrentUserInfo().subscribe({
      next: (data) => {
        this.user.name = data.name;
        this.user.surname = data.surname;
        this.user.email = data.email;
        // optional: fill more fields if backend provides them
      },
      error: (err) => {
        console.error('Error loading user info:', err);
      }
    });
  }
}
