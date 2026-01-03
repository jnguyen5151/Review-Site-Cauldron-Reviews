import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';

import { AuthService } from '../../services/auth-service';

@Component({
  selector: 'app-verify-email',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './verify-email.html',
  styleUrl: './verify-email.css',
})
export class VerifyEmail {
  status = signal<'pending' | 'success' | 'error'>('pending');

  private route: ActivatedRoute = inject(ActivatedRoute);
  private authService = inject(AuthService);

  constructor() {
    this.verifyEmail();
  }

  private verifyEmail() {
    const userId = this.route.snapshot.paramMap.get('userId');
    const token = this.route.snapshot.paramMap.get('token');

    if (!userId || !token) {
      this.status.set('error');
      return;
    }

    this.authService.verifyEmail(userId, token).subscribe({
      next: () => this.status.set('success'),
      error: () => this.status.set('error')
    });

  }

}
