import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { ReviewService } from '../../services/review-service';
import { Review } from '../../models/review';

@Component({
  selector: 'app-review-details',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './review-details.html',
  styleUrl: './review-details.css',
})
export class ReviewDetails {
  route: ActivatedRoute = inject(ActivatedRoute);
  reviewService = inject(ReviewService);
  review = signal<Review | null>(null);

  ngOnInit(): void {
    const reviewId = Number(this.route.snapshot.paramMap.get('reviewId'));

    this.reviewService.getReviewById(reviewId).subscribe((data: Review) => {
      this.review.set(data);
      console.log(this.review);
    });

  }

}
