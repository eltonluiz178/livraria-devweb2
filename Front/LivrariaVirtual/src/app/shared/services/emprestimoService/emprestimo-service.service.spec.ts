import { TestBed } from '@angular/core/testing';

import { EmprestimoServiceService } from './emprestimo-service.service';

describe('EmprestimoServiceService', () => {
  let service: EmprestimoServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmprestimoServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
