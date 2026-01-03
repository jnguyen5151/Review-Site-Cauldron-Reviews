import { Component, inject, signal, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, NavigationEnd, Event as RouterEvent } from '@angular/router';
import { filter, takeUntil, Subject } from 'rxjs';

import { ReviewComponent } from '../review-component/review-component';
import { Review } from '../../models/review';
import { ReviewService, getReviewsResponse } from '../../services/review-service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, ReviewComponent],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class HomeComponent {

  reviewList = signal<Review[]>([]);
  reviewService: ReviewService = inject(ReviewService);
  router: Router = inject(Router);
  private sDestroy = new Subject<void>();

  reviewCount: number = 15;
  page: number = 1;
  totalReviews: number = 0;
  isLoading: boolean = false;
  isLastPage: boolean = false;


  ngOnInit(): void {
    this.fetchReviews();

    this.router.events
      .pipe(
        filter((event: RouterEvent) => event instanceof NavigationEnd),
        takeUntil(this.sDestroy)
      )
      .subscribe(() => {
        this.fetchReviews();
      });
  }

  ngOnDestroy(): void {
    this.sDestroy.next();
    this.sDestroy.complete();
  }

  fetchReviews() {
    if (this.isLoading) return;
    this.isLoading = true;

    this.reviewService.getAllReviews(this.reviewCount, this.page)
      .subscribe({
        next: (data: getReviewsResponse) => {
          this.reviewList.set(data.reviews);
          this.totalReviews = data.total;

          const totalPages = Math.ceil(data.total / this.reviewCount);
          this.isLastPage = this.page >= totalPages;

          console.log(data);
        },
        error: (err) => {
          console.log('Error getting reviews', err);
          this.isLoading = false;
        },
        complete: () => {
          this.isLoading = false;
        }
      });

  }

  prevPage() {
    if (this.page <= 1) return;
    this.page = this.page - 1;
    this.fetchReviews();
  }

  nextPage() {
    this.page = this.page + 1;
    this.fetchReviews();
  }

}
