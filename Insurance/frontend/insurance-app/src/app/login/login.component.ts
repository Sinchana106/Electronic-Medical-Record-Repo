import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { User } from '../dtos/user';

@Component({
  selector: 'app-login',
  imports: [MatButtonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginUser: User = {
    username: '',
    password: ''
  };
  constructor(private _authService: AuthService, private router: Router) { }
  login() {
    this._authService.login(this.loginUser).subscribe({
      next: (res: any) => {
        console.log(this.loginUser)
        localStorage.setItem('token', res.token);
        this.router.navigate(['/']);
      },
      error: err => {
        console.error(err)
        alert('Login failed. Please check your credentials.')
      }
    });
    console.log('Login function called with model:', this.loginUser);
  }


}
