import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Review } from '../../models/review';

@Component({
  selector: 'app-review-component',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './review-component.html',
  styleUrl: './review-component.css',
})
export class ReviewComponent {
  @Input() review!: Review;
}
