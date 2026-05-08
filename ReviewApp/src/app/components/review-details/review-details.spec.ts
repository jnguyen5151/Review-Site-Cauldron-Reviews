import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideRouter } from '@angular/router';
import { provideHttpClient } from '@angular/common/http';
import { ReviewDetails } from './review-details';

describe('ReviewDetails', () => {
  let component: ReviewDetails;
  let fixture: ComponentFixture<ReviewDetails>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReviewDetails],
      providers: [
        provideRouter([]),
        provideHttpClient()
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ReviewDetails);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
