import { Component } from '@angular/core';

@Component({
  selector: 'app-user-registration',
  standalone: false,
  templateUrl: './user-registration.component.html',
  styleUrl: './user-registration.component.css'
})
export class UserRegistrationComponent {
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
