import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { RouterModule } from '@angular/router';

import { TopBar } from './components/top-bar/top-bar';
import { BotBar } from './components/bot-bar/bot-bar';
import { AuthService } from './services/auth-service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterModule, TopBar, BotBar],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  protected readonly title = signal('ReviewApp');

  constructor(private authService: AuthService) {
    this.authService.loadUserFromStorage();
  }
}
