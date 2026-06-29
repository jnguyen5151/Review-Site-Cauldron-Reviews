import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';

import { Review, reviewCard } from '../models/review';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ReviewService {

  private url = `${environment.apiUrl}/api/GameReview`;
  private http = inject(HttpClient);

  getAllReviews(reviewCount: number = 15, page: number = 1): Observable<getReviewsResponse> {
    return this.http.get<getReviewsResponse>(`${this.url}/getAll`, { params: { reviewCount, page } });
  }

  getReviewById(reviewId: number): Observable<Review> {
    return this.http.get<Review>(`${this.url}/get/${reviewId}`);
  }

  getReviewByGame(gameId: number, reviewCount: number = 15, page: number = 1): Observable<getReviewsResponse> {
    return this.http.get<getReviewsResponse>(`${this.url}/reviewByGame/${gameId}`, { params: { reviewCount, page } });
  }
  
  createReview(review: Review): Observable<Review> {
    return this.http.post<Review>(`${this.url}/create`, review);
  }
  
  updateReview(reviewId: number, review: Review): Observable<Review> {
    return this.http.put<Review>(`${this.url}/update/${reviewId}`, review);
  }
  
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
  reviews: reviewCard[];
  total: number;
}
