import { TestBed } from '@angular/core/testing';

import { WrapperSearchService } from './wrapper-search.service';

describe('WrapperSearchService', () => {
  let service: WrapperSearchService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WrapperSearchService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
