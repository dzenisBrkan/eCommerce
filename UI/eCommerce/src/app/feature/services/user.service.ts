import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = 'https://localhost:7221/api/User';

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
