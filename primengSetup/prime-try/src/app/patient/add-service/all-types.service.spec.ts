import { TestBed } from '@angular/core/testing';

import { AllTypesService } from './all-types.service';

describe('AllTypesService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AllTypesService = TestBed.get(AllTypesService);
    expect(service).toBeTruthy();
  });
});
