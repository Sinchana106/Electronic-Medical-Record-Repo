import { Component } from '@angular/core';
import { MatNavList } from '@angular/material/list';

@Component({
  selector: 'app-side-bar',
  imports: [MatNavList],
  templateUrl: './side-bar.component.html',
  styleUrl: './side-bar.component.css'
})
export class SideBarComponent {
  logout() {
    // Implement logout functionality here
    console.log('Logout function called');
    // You might want to clear user session, tokens, etc.
    // For example:
    localStorage.removeItem('token');
    // Redirect to login page or home page
    window.location.href = '/login';
  }
}
