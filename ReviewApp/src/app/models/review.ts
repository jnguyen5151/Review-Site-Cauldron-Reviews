import { schema, required, min, max, disabled } from '@angular/forms/signals';

export interface Review {
  authorName: string,
  reviewId: number,
  gameName: string,
  rating: number,
  createdAt: string,
  content: string,
  title: string,
  likes: number,
  dislikes: number,
  commentNumber: number,
  steamAppId: number
}

export const initialData: Review = {
  authorName: 'Anonymous',
  reviewId: 0,
  gameName: '',
  rating: NaN,
  createdAt: new Date().toISOString(),
  content: '',
  title: '',
  likes: 0,
  dislikes: 0,
  commentNumber: 0,
  steamAppId: 0
}

export interface reviewCard {
  authorName: string,
  reviewId: number,
  gameName: string,
  rating: number,
  createdAt: string,
  title: string,
  likes: number,
  dislikes: number,
  commentNumber: number
}
