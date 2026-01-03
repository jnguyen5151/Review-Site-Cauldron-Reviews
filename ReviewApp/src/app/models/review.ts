import { schema, required, min, max } from '@angular/forms/signals';

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
  commentNumber: number
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
  commentNumber: 0
}

export const reviewSchema = schema<Review>((root) => {
  required(root.gameName, { message: 'Please Select the Game you are Reviewing' });
  required(root.rating, { message: 'Rating is Required' });
  min(root.rating, 0, { message: 'Rating must be from 0 - 100' });
  max(root.rating, 100, {message: 'Rating must be from 0 - 100'});
  required(root.content, { message: 'Review Content is Required' });
  required(root.title, { message: 'Title is Required' });
});
