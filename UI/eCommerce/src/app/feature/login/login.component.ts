import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { first } from 'rxjs';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  submitted = false;
  errorMessage = '';

  constructor(
    private formBuilder: FormBuilder,
    private authService: UserService,
    private router: Router
  ) {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }

  ngOnInit() {
    
  }

  // Getter for easy access to form fields using bracket notation
  get f() {
    return this.loginForm.controls;
  }

  // On form submission
  onSubmit() {
    this.submitted = true;

    if (this.loginForm.invalid) {
      return;
    }

    const loginData = {
      Username: this.f['email'].value,
      Password: this.f['password'].value,
    };

    this.authService.login(loginData).pipe(first()).subscribe(
      (response) => {
        localStorage.setItem('access_token', response.accessToken);
        this.router.navigate(['/home']);
      },
      (error) => {
        console.error('Login error:', error);
        this.errorMessage = error.error;
      }
    );
  }
}