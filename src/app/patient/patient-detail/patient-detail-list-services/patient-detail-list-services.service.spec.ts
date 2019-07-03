import { TestBed } from '@angular/core/testing';

import { PatientDetailListServicesService } from './patient-detail-list-services.service';

describe('PatientDetailListServicesService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PatientDetailListServicesService = TestBed.get(PatientDetailListServicesService);
    expect(service).toBeTruthy();
  });
});
