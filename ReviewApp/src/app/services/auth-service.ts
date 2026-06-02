import { Injectable, inject, signal, computed } from '@angular/core';
import { HttpClient, HttpBackend } from '@angular/common/http';
import { Observable, BehaviorSubject, tap } from 'rxjs';

import { environment } from '../../environments/environment';
import { RegisterModel, LoginModel, UserModel } from '../models/user-account';

@Injectable({
  providedIn: 'root',
})
export class AuthService {

  private apiUrl = `${environment.apiUrl}/api/account`;
  private http = inject(HttpClient);
  private rawHttp = new HttpClient(inject(HttpBackend));

  public $user = signal<UserModel | null>(null);
  isLoggedIn = computed(() => !!this.$user());
  roles = computed(() => this.$user()?.role ?? []);

  constructor() {
    this.relog();
  }

  register(dto: RegisterModel): Observable<any> {
    return this.rawHttp.post(`${this.apiUrl}/register`, dto);
  }

  verifyEmail(userId: string, token: string): Observable<any> {
    return this.rawHttp.get(`${this.apiUrl}/confirmEmail`, { params: { userId, token }, responseType: 'text' });
  }

  login(dto: LoginModel): Observable<any> {
    return this.rawHttp.post(`${this.apiUrl}/login`, dto, { withCredentials: true }).pipe(
      tap((user: any) => {
        this.$user.set(user);
      })
    );
  }

  refresh(): Observable<any> {
    return this.rawHttp.post(`${this.apiUrl}/refresh`, {}, { withCredentials: true });
  }

  logout(): Observable<any> {
    this.$user.set(null);
    return this.rawHttp.post(`${this.apiUrl}/logout`, {}, { withCredentials: true });
  }

  getAccount(): Observable<any> {
    return this.rawHttp.get(`${this.apiUrl}/get`, { withCredentials: true });
  }

  relog(): void {
    this.rawHttp.get(`${this.apiUrl}/relog`, { withCredentials: true }).pipe(
      tap((user: any) => {
        if (user) {
          this.$user.set(user);
        }
      })
    ).subscribe();
  }
}
