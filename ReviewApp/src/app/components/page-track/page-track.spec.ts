import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PageTrack } from './page-track';

describe('PageTrack', () => {
  let component: PageTrack;
  let fixture: ComponentFixture<PageTrack>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PageTrack]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PageTrack);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
