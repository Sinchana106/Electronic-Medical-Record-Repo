import { TestBed } from '@angular/core/testing';

import { InsurerService } from './insurer.service';

describe('InsurerService', () => {
  let service: InsurerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InsurerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
