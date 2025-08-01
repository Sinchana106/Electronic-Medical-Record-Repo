import { Component } from '@angular/core';
import { MatNavList } from '@angular/material/list';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-side-bar',
  imports: [MatNavList],
  templateUrl: './side-bar.component.html',
  styleUrl: './side-bar.component.css'
})
export class SideBarComponent {
  constructor(private _authService: AuthService,private router:Router) { }
  logout() {
    this._authService.logout();
    this.router.navigate(['login']);
  }
}
