import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountModal } from './account-modal';

describe('AccountModal', () => {
  let component: AccountModal;
  let fixture: ComponentFixture<AccountModal>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AccountModal]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AccountModal);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
