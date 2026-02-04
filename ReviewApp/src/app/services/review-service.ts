import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';

import { Review } from '../models/review';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ReviewService {

  private url = `${environment.apiUrl}/GameReview`;
  private http = inject(HttpClient);

  getAllReviews(reviewCount: number = 15, page: number = 1): Observable<getReviewsResponse> {
    return this.http.get<getReviewsResponse>(`${this.url}/getAll`, { params: { reviewCount, page } });
  }
  //.pipe(map((reviews: Review[]) => reviews.map((review: Review) => this.httpDecode(review)))
  getReviewById(reviewId: number): Observable<Review> {
    return this.http.get<Review>(`${this.url}/get/${reviewId}`);
  }
  //.pipe(map((review: Review) => this.httpDecode(review)))
  createReview(review: Review): Observable<Review> {
    return this.http.post<Review>(`${this.url}/create`, review);
  }
  //.pipe(map((review: Review) => this.httpEncode(review)))
  updateReview(reviewId: number, review: Review): Observable<Review> {
    return this.http.put<Review>(`${this.url}/update/${reviewId}`, review);
  }
  //.pipe(map((review: Review) => this.httpEncode(review)))
  deleteReview(reviewId: number): Observable<any> {
    return this.http.delete(`${this.url}/delete/${reviewId}`);
  }

  httpEncode(review: Review): Review {
    return {
      ...review,
      content: encodeURIComponent(review.content)
    };
  }

  httpDecode(review: Review): Review {
    return {
      ...review,
      content: decodeURIComponent(review.content)
    };
  }
}

export interface getReviewsResponse {
  reviews: Review[];
  total: number;
}
