import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = 'https://localhost:7221/api/User';

  private token = localStorage.getItem('access_token');
  private headers = new HttpHeaders().set('Authorization', `Bearer ${this.token}`);
    
  constructor(private http: HttpClient) {}

  registerUser(registerData: any): Observable<any> {
    console.log("Register data", registerData);
    return this.http.post<any>(`${this.apiUrl}/register`, registerData);
  }

  login(loginData: { Username: string; Password: string }): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/login`, loginData);
  }

  getCurrentUserInfo() {
    return this.http.get<any>(`${this.apiUrl}/current-user-info`);
  }
}
