import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManualScoringComponent } from './manual-scoring.component';

describe('ManualScoringComponent', () => {
  let component: ManualScoringComponent;
  let fixture: ComponentFixture<ManualScoringComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManualScoringComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManualScoringComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
