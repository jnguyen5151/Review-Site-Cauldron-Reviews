import { Injectable, inject, signal, computed } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject, tap } from 'rxjs';

import { environment } from '../../environments/environment';
import { RegisterModel, LoginModel, UserModel } from '../models/user-account';

@Injectable({
  providedIn: 'root',
})
export class AuthService {

  private apiUrl = `${environment.apiUrl}/account`;
  private http = inject(HttpClient);

  public $user = signal<UserModel | null>(null);
  isLoggedIn = computed(() => !!this.$user());
  roles = computed(() => this.$user()?.role ?? []);

  constructor() {
    this.loadUserFromStorage();
  }

  register(dto: RegisterModel): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, dto);
  }

  verifyEmail(userId: string, token: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/confirmEmail`, { params: { userId, token }, responseType: 'text' });
  }

  login(dto: LoginModel): Observable<any> {
    return this.http.post(`${this.apiUrl}/login`, dto).pipe(
      tap((user: any) => {
        this.$user.set(user);
        if (typeof window !== 'undefined') {
          localStorage.setItem('current-user', JSON.stringify(user));
        }
      })
    );
  }

  loadUserFromStorage(): void {
    if (typeof window !== 'undefined')
    {
      const stored = localStorage.getItem('current-user');
      if (stored) {
        this.$user.set(JSON.parse(stored));
      }
    }

  }

  refresh(): Observable<any> {
    return this.http.post(`${this.apiUrl}/refresh`, {}, { withCredentials: true });
  }

  logout(): Observable<any> {
    this.$user.set(null);
    if (typeof window !== 'undefined') {
      localStorage.removeItem('current-user');
    }
    return this.http.post(`${this.apiUrl}/logout`, {}, { withCredentials: true });
  }

  getAccount(): Observable<any> {
    return this.http.get(`${this.apiUrl}/get`, { withCredentials: true });
  }
}
