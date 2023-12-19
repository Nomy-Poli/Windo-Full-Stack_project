import { TestBed } from '@angular/core/testing';

import { WrapperFuncService } from './wrapper-func.service';

describe('WrapperFuncService', () => {
  let service: WrapperFuncService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WrapperFuncService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
