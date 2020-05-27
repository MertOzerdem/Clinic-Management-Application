import { TestBed } from '@angular/core/testing';

import { DeleteProductTypeService } from './delete-product-type.service';

describe('DeleteProductTypeService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DeleteProductTypeService = TestBed.get(DeleteProductTypeService);
    expect(service).toBeTruthy();
  });
});
