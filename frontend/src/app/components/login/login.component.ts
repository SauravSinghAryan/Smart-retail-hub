import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  private authService = inject(AuthService);
  private router = inject(Router);

  // Footer Year
  currentYear = new Date().getFullYear();

  // UI State
  isLoginMode = true;
  isLoading = false;

  errorMessage = '';
  successMessage = '';

  // Login Model
  loginData = {
    username: '',
    password: ''
  };

  // Registration Model
  signupData = {
    name: '',
    username: '',
    password: '',
    role: 'Cashier'
  };

  constructor() {

    // Redirect if already authenticated
    if (this.authService.isAuthenticated()) {
      this.router.navigate(['/dashboard']);
    }

  }

  // ===============================
  // Switch Login / Signup
  // ===============================

  toggleMode(): void {

    this.isLoginMode = !this.isLoginMode;

    this.clearMessages();

  }

  // ===============================
  // Login
  // ===============================

  onLogin(): void {

    if (!this.loginData.username.trim()) {

      this.errorMessage = 'Please enter your username.';
      return;

    }

    if (!this.loginData.password.trim()) {

      this.errorMessage = 'Please enter your password.';
      return;

    }

    this.isLoading = true;

    this.clearMessages();

    this.authService.login(this.loginData).subscribe({

      next: () => {

        this.successMessage = 'Login successful. Redirecting...';

        setTimeout(() => {

          this.router.navigate(['/dashboard']);

        }, 600);

      },

      error: (err) => {

        this.isLoading = false;

        this.errorMessage =
          err.error?.error ??
          'Invalid username or password.';

      }

    });

  }

  // ===============================
  // Signup
  // ===============================

  onSignup(): void {

    if (!this.signupData.name.trim()) {

      this.errorMessage = 'Please enter full name.';
      return;

    }

    if (!this.signupData.username.trim()) {

      this.errorMessage = 'Please enter username.';
      return;

    }

    if (!this.signupData.password.trim()) {

      this.errorMessage = 'Please enter password.';
      return;

    }

    this.isLoading = true;

    this.clearMessages();

    this.authService.signup(this.signupData).subscribe({

      next: () => {

        this.isLoading = false;

        this.successMessage =
          'User registered successfully.';

        this.resetSignupForm();

        this.isLoginMode = true;

      },

      error: (err) => {

        this.isLoading = false;

        this.errorMessage =
          err.error?.error ??
          'Registration failed.';

      }

    });

  }

  // ===============================
  // Reset Signup
  // ===============================

  private resetSignupForm(): void {

    this.signupData = {

      name: '',

      username: '',

      password: '',

      role: 'Cashier'

    };

  }

  // ===============================
  // Clear Messages
  // ===============================

  private clearMessages(): void {

    this.errorMessage = '';

    this.successMessage = '';

  }

}