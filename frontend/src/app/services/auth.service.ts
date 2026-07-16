import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject, tap } from 'rxjs';

export interface User {
  username: string;
  name: string;
  role: string;
}

export interface AuthResponse {
  message: string;
  token: string;
  user: User;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private http = inject(HttpClient);

  // ASP.NET Core Backend
  private baseUrl = 'https://localhost:7168/api/auth';

  private currentUserSubject = new BehaviorSubject<User | null>(null);

  currentUser$ = this.currentUserSubject.asObservable();

  constructor() {

    const savedUser = localStorage.getItem('auth_user');

    if (savedUser) {
      try {
        this.currentUserSubject.next(JSON.parse(savedUser));
      } catch {
        localStorage.removeItem('auth_user');
      }
    }
  }

  signup(signUpData: any): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(
      `${this.baseUrl}/signup`,
      signUpData
    ).pipe(
      tap(response => this.handleAuthentication(response))
    );
  }

  login(loginData: any): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(
      `${this.baseUrl}/login`,
      loginData
    ).pipe(
      tap(response => this.handleAuthentication(response))
    );
  }

  logout(): void {
    localStorage.removeItem('auth_token');
    localStorage.removeItem('auth_user');

    this.currentUserSubject.next(null);
  }

  getToken(): string | null {
    return localStorage.getItem('auth_token');
  }

  isAuthenticated(): boolean {
    return !!this.getToken();
  }

  getCurrentUser(): User | null {
    return this.currentUserSubject.value;
  }

  private handleAuthentication(response: AuthResponse): void {

    localStorage.setItem('auth_token', response.token);
    localStorage.setItem('auth_user', JSON.stringify(response.user));

    this.currentUserSubject.next(response.user);
  }
}