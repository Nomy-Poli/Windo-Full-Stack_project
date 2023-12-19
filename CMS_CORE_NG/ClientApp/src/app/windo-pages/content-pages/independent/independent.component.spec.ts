import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IndependentComponent } from './independent.component';

describe('PracticeComponent', () => {
  let component: IndependentComponent;
  let fixture: ComponentFixture<IndependentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IndependentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IndependentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
