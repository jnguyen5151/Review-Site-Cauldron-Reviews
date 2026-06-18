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
  appId: number
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
  appId: 0
}

export const reviewSchema = schema<Review>((root) => {
  required(root.gameName, { message: 'Please select a Game' });
  disabled(root.gameName, { when: ({valueOf}) => valueOf(root.gameName) !== ''});
  required(root.rating, { message: 'Rating is Required' });
  min(root.rating, 0, { message: 'Rating must be from 0 - 100' });
  max(root.rating, 100, {message: 'Rating must be from 0 - 100'});
  required(root.content, { message: 'Review Content is Required' });
  required(root.title, { message: 'Title is Required' });
});

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
