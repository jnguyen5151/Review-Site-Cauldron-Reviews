import { Component, ChangeDetectionStrategy, inject, signal, PLATFORM_ID } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { ReviewService, getReviewsResponse } from '../../services/review-service';
import { SearchService } from '../../services/search-service';
import { CardModel } from '../../models/game-search';
import { isPlatformBrowser, CommonModule } from '@angular/common';
import { reviewCard } from '../../models/review';
import { ReviewComponent } from '../review-component/review-component';

@Component({
  selector: 'app-search-results',
  imports: [ReviewComponent, CommonModule],
  templateUrl: './search-results.html',
  changeDetection: ChangeDetectionStrategy.Eager,
  styleUrl: './search-results.css',
})
export class SearchResults {
  route: ActivatedRoute = inject(ActivatedRoute);
  private reviewService = inject(ReviewService);
  private searchService = inject(SearchService);
  private platformId = inject(PLATFORM_ID);
  private gameId = 0;
  

  gameResult = signal<CardModel[]>([]);

  ngOnInit(): void {

    this.gameId = Number(this.route.snapshot.paramMap.get('gameId'));
    if (this.gameId != 0) {
      this.searchService.gameSearch({ steamAppId: this.gameId }).subscribe((results: CardModel[]) => {
        this.gameResult.set(results);
        console.log(results);
      });
    }

    this.route.paramMap.subscribe(params => {
      this.page = +(params.get('page') ?? 1);
      if (isPlatformBrowser(this.platformId)) {
        this.fetchReviews();
      }
    });

  }

  isLoading = signal<boolean>(false);
  reviewCount: number = 15;
  totalReviews: number = 0;
  page: number = 1;
  reviewList = signal<reviewCard[]>([]);
  isLastPage: boolean = false;

  fetchReviews() {
    if (this.isLoading()) return;
    this.isLoading.set(true);

    this.reviewService.getReviewByGame(this.gameId, this.reviewCount, this.page)
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
}
