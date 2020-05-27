import { TestBed } from '@angular/core/testing';

import { AddPatientService } from './add-patient.service';

describe('AddPatientService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AddPatientService = TestBed.get(AddPatientService);
    expect(service).toBeTruthy();
  });
});
