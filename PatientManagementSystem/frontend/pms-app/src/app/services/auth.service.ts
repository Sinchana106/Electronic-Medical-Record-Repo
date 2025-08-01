import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})

  export class AuthService {
  private baseUrl = 'https://localhost:7057/api/auth';

  constructor(private http: HttpClient) { }

  login(model: any) {
    return this.http.post(`${this.baseUrl}/login`, model);
  }

  register(model: any) {
    return this.http.post(`${this.baseUrl}/register`, model);
  }

  logout() {
    localStorage.removeItem('token');
  }

  isLoggedIn() {
    return !!localStorage.getItem('token');
  }
}

