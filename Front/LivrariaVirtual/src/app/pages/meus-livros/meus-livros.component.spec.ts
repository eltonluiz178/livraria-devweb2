import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MeusLivrosComponent } from './meus-livros.component';

describe('MeusLivrosComponent', () => {
  let component: MeusLivrosComponent;
  let fixture: ComponentFixture<MeusLivrosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MeusLivrosComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MeusLivrosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
