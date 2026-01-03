import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BotBar } from './bot-bar';

describe('BotBar', () => {
  let component: BotBar;
  let fixture: ComponentFixture<BotBar>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BotBar]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BotBar);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
