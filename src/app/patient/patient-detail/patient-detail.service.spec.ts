import { TestBed } from '@angular/core/testing';

import { PatientDetailService } from './patient-detail.service';

describe('PatientDetailService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PatientDetailService = TestBed.get(PatientDetailService);
    expect(service).toBeTruthy();
  });
});
