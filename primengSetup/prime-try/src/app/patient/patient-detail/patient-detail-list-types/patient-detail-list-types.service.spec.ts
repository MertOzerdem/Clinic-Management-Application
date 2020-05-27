import { TestBed } from '@angular/core/testing';

import { PatientDetailListTypesService } from './patient-detail-list-types.service';

describe('PatientDetailListTypesService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PatientDetailListTypesService = TestBed.get(PatientDetailListTypesService);
    expect(service).toBeTruthy();
  });
});
