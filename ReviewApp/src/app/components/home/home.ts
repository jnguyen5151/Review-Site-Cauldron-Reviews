import { Component, inject, signal, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, ActivatedRoute, NavigationEnd, Event as RouterEvent } from '@angular/router';
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

  // Service Injections
  private reviewService: ReviewService = inject(ReviewService);
  private route: ActivatedRoute = inject(ActivatedRoute);
  private router: Router = inject(Router); 

  reviewList = signal<Review[]>([]);

  reviewCount: number = 15;
  page: number = 1;
  totalReviews: number = 0;
  isLoading: boolean = false;
  isLastPage: boolean = false;


  ngOnInit(): void {

    console.log('int');
    this.route.paramMap.subscribe(params => {
      this.page = +(params.get('page') ?? 1);
      this.fetchReviews();
    });

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
    this.router.navigate(['/home', this.page - 1]);
  }

  nextPage() {
    this.router.navigate(['/home', this.page + 1]);
  }

}
