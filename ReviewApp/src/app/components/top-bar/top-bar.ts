import { Component, inject, signal } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Dialog } from '@angular/cdk/dialog';
import { CommonModule } from '@angular/common';

import { AuthService } from '../../services/auth-service';
import { Login } from '../login/login';

@Component({
  selector: 'app-top-bar',
  standalone: true,
  imports: [RouterModule, CommonModule],
  templateUrl: './top-bar.html',
  styleUrl: './top-bar.css',
})
export class TopBar {

  private dialog = inject(Dialog);
  private authService = inject(AuthService);

  isLoggedIn = this.authService.isLoggedIn;
  roles = this.authService.roles;

  protected openModal() {
    this.dialog.open(Login);
  }

  isSidebarOpen = signal<'open' | 'closed'>('closed');

  toggleSidebar() {
    this.isSidebarOpen.update((state: 'open' | 'closed') => state === 'closed' ? 'open' : 'closed');
    console.log("sidebar toggled ", this.isSidebarOpen());
  }

  logout() {
    this.authService.logout().subscribe({
      next: () => {
        console.log('User Logged Out');
      },
      error: (err: any) => {
        console.log('Error: ' + JSON.stringify(err.error ?? err));
      }
    });
  }

}
