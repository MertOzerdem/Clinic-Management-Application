import { TestBed } from '@angular/core/testing';

import { PatientDetailListTransactionsService } from './patient-detail-list-transactions.service';

describe('PatientDetailListTransactionsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PatientDetailListTransactionsService = TestBed.get(PatientDetailListTransactionsService);
    expect(service).toBeTruthy();
  });
});
