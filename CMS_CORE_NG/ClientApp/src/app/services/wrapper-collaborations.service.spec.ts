import { TestBed } from '@angular/core/testing';

import { WrapperCollaborationsService } from './wrapper-collaborations.service';

describe('WrapperCollaborationsService', () => {
  let service: WrapperCollaborationsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WrapperCollaborationsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
