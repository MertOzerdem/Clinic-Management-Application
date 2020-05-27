import { TestBed } from '@angular/core/testing';

import { AddProductTypeService } from './add-product-type.service';

describe('AddProductTypeService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AddProductTypeService = TestBed.get(AddProductTypeService);
    expect(service).toBeTruthy();
  });
});
