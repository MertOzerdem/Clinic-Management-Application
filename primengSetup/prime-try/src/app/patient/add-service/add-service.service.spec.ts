import { TestBed } from '@angular/core/testing';

import { AddServiceService } from './add-service.service';

describe('AddServiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AddServiceService = TestBed.get(AddServiceService);
    expect(service).toBeTruthy();
  });
});
