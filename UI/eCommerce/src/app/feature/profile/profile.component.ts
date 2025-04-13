import { Component } from '@angular/core';

@Component({
  selector: 'app-profile',
  standalone: false,
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {
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
}
