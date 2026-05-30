import { Component, HostListener, inject, signal, ElementRef, ViewChild } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Dialog } from '@angular/cdk/dialog';
import { CommonModule } from '@angular/common';
import { toObservable } from '@angular/core/rxjs-interop';

import { AuthService } from '../../services/auth-service';
import { SearchService } from '../../services/search-service';
import { Login } from '../login/login';
import { Warning } from '../warning/warning';
import { debounceTime, filter, switchMap, tap } from 'rxjs/operators';
import { CardModel } from '../../models/game-search';

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
  private searchService = inject(SearchService);

  isLoggedIn = this.authService.isLoggedIn;
  roles = this.authService.roles;

  protected openModal() {
    this.dialog.open(Login);
  }

  protected openWarning() {
    if (typeof window !== 'undefined') {
      if (localStorage.getItem('warning') != 'true') {
        this.dialog.open(Warning);
        localStorage.setItem('warning', 'true');
      }
    }
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

  searchString = signal<string>('');
  private searchString$ = toObservable(this.searchString);

  private searchResults$ = this.searchString$.pipe(
    filter((search: string) => search.length > 0),
    debounceTime(300),
    switchMap((search: string) => this.searchService.gameSearch(search))
  );

  searchResults = signal<CardModel[]>([]);
  constructor() {
    this.searchResults$.subscribe((results: CardModel[]) => this.searchResults.set(results));
  }

  @ViewChild('searchWrapper') searchWrapper!: ElementRef;

  @HostListener('document:click', ['$event.target'])
  onClick(target: EventTarget | null) {
    if (this.searchResults().length === 0) return;
    if (!this.searchWrapper.nativeElement.contains(target)) this.searchResults.set([]);
  }

  ngOnInit() {
    this.openWarning();
    this.authService.getAccount();
  }

}
