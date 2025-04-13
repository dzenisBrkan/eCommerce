import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-user-registration',
  standalone: false,
  templateUrl: './user-registration.component.html',
  styleUrl: './user-registration.component.css'
})
export class UserRegistrationComponent {
  registrationForm: FormGroup;
  submitted = false;
  errorMessage: string | null = null;

  constructor(
    private formBuilder: FormBuilder,
    private userService: UserService,
    private router: Router
  ) 
  {   
    this.registrationForm = this.formBuilder.group({
    Name: ['', Validators.required],
    Surname: ['', Validators.required],
    Email: ['', [Validators.required, Validators.email]],
    Password: ['', Validators.required],
    PhoneNumber: ['', Validators.required],
    Location: ['', Validators.required],
    Address: ['', Validators.required]
  });}

  ngOnInit(): void {
  }

  get f() {
    return this.registrationForm.controls;
  }

  onSubmit(): void {
    if (this.registrationForm.invalid) {
      return;
    }
  
    const registerData = this.registrationForm.value;
    
    this.userService.registerUser(registerData).subscribe(
      (response) => {
        this.router.navigate(['/login']);
      },
      (error) => {
        console.error('Registration error:', error);
    
        if (Array.isArray(error.error)) {
          this.errorMessage = error.error.join('\n');
        } else if (typeof error.error === 'string') {
          this.errorMessage = error.error;
        } else {
          this.errorMessage = 'Registration failed. Please try again.';
        }
      }
    );
  }
  
}
