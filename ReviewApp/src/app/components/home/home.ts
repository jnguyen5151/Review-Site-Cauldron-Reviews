import { Component, inject, signal, OnDestroy, PLATFORM_ID } from '@angular/core';
import { CommonModule, isPlatformBrowser } from '@angular/common';
import { Router, ActivatedRoute, NavigationEnd, Event as RouterEvent } from '@angular/router';
import { filter, takeUntil, Subject } from 'rxjs';

import { ReviewComponent } from '../review-component/review-component';
import { reviewCard } from '../../models/review';
import { ReviewService, getReviewsResponse } from '../../services/review-service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, ReviewComponent],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class HomeComponent {

  // Service Injections
  private reviewService: ReviewService = inject(ReviewService);
  private route: ActivatedRoute = inject(ActivatedRoute);
  private router: Router = inject(Router); 

  reviewList = signal<reviewCard[]>([]);

  reviewCount: number = 15;
  page: number = 1;
  totalReviews: number = 0;
  isLoading = signal<boolean>(false);
  isLastPage: boolean = false;
  private platformId = inject(PLATFORM_ID);

  ngOnInit(): void {

    this.route.paramMap.subscribe(params => {
      this.page = +(params.get('page') ?? 1);
      if (isPlatformBrowser(this.platformId)) {
        this.fetchReviews();
      }
    });

  }

  fetchReviews() {
    if (this.isLoading()) return;
    this.isLoading.set(true);

    this.reviewService.getAllReviews(this.reviewCount, this.page)
      .subscribe({
        next: (data: getReviewsResponse) => {

          this.reviewList.set(data.reviews);
          this.totalReviews = data.total;

          const totalPages = Math.ceil(data.total / this.reviewCount);
          this.isLastPage = this.page >= totalPages;
        },
        error: (err) => {
          console.log('Error getting reviews', err);
          this.isLoading.set(false);
        },
        complete: () => {
          this.isLoading.set(false);
        }
      });

  }

  prevPage() {
    if (this.page <= 1) return;
    this.router.navigate(['/home', this.page - 1]);
  }

  nextPage() {
    this.router.navigate(['/home', this.page + 1]);
  }

}
