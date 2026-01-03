import { Component, signal, effect, inject } from '@angular/core';
import { Field, form } from '@angular/forms/signals';

import { Review, initialData, reviewSchema } from '../../models/review';
import { ReviewService } from '../../services/review-service';

@Component({
  selector: 'app-review-create',
  imports: [Field],
  templateUrl: './review-create.html',
  styleUrl: './review-create.css',
})
export class ReviewCreate {

  reviewModel = signal<Review>(initialData);
  reviewForm = form(this.reviewModel, reviewSchema);

  private reviewService = inject(ReviewService);

  submitReview() {

    const newReview = this.reviewModel();

    this.reviewService.createReview(newReview).subscribe({
      next: (created: Review) => {
        console.log('Created review: ', created);
      },
      error: (err: any) => {
        console.error('Failed to create Review: ', err);
      },
    });

  }

}
