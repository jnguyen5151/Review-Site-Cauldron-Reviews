import { Component, computed, inject, signal, ChangeDetectionStrategy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { ReviewService } from '../../services/review-service';
import { Review } from '../../models/review';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-review-details',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './review-details.html',
  changeDetection: ChangeDetectionStrategy.Eager,
  styleUrl: './review-details.css',
})
export class ReviewDetails {
  route: ActivatedRoute = inject(ActivatedRoute);
  private sanitizer = inject(DomSanitizer);
  private reviewService = inject(ReviewService);
  review = signal<Review | null>(null);

  safeContent = computed(() =>
    this.sanitizer.bypassSecurityTrustHtml(this.review()?.content ?? '')
  );

  ngOnInit(): void {
    const reviewId = Number(this.route.snapshot.paramMap.get('reviewId'));

    this.reviewService.getReviewById(reviewId).subscribe((data: Review) => {
      this.review.set(data);
    });

  }

}
