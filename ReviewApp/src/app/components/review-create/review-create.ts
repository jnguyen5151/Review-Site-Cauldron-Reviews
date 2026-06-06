import { Component, signal, effect, inject } from '@angular/core';
import { isPlatformServer } from '@angular/common';
import { Field, form } from '@angular/forms/signals';

import { Review, initialData, reviewSchema } from '../../models/review';
import { ReviewService } from '../../services/review-service';

import { QuillModule } from 'ngx-quill'

@Component({
  selector: 'app-review-create',
  imports: [Field, QuillModule],
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
      },
      error: (err: any) => {
        console.error('Failed to create Review: ', err);
      },
    });

  }

  protected quillConfig = {
    toolbar: {
      container: [
        ['clean'],
        ['bold', 'italic', 'underline', 'strike'],
        [{ 'color': [] }],
        [{ 'align': [] }],
        [ { header: 1 }, { header: 2 }],
        [{ 'size': ['small', false, 'large', 'huge'] }],
        [{ list: 'ordered' }, { list: 'bullet' }],
        ['link', 'image', 'video']
      ]
    }
  }

}
