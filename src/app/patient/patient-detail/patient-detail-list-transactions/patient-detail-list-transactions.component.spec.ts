import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientDetailListTransactionsComponent } from './patient-detail-list-transactions.component';

describe('PatientDetailListTransactionsComponent', () => {
  let component: PatientDetailListTransactionsComponent;
  let fixture: ComponentFixture<PatientDetailListTransactionsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PatientDetailListTransactionsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PatientDetailListTransactionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
