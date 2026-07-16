import { Component } from '@angular/core';
import { RouterOutlet, RouterLink, RouterLinkActive,Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink, RouterLinkActive],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'SK Hardware';
  currentYear = new Date().getFullYear();
  isSidebarOpen = true;
  
  constructor(public router: Router,
    private authService: AuthService
  ) {}
  

  toggleSidebar() {
    this.isSidebarOpen = !this.isSidebarOpen;
  }
  onLogout() {
    this.authService.logout();           // Clears localStorage tokens and resets the user stream
    this.router.navigate(['/login']);    // Redirects the user instantly back to the login screen
  }
}
