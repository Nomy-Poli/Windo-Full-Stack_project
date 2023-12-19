import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BuisnessScoringDetailComponent } from './buisness-scoring-detail.component';

describe('BuisnessScoringDetailComponent', () => {
  let component: BuisnessScoringDetailComponent;
  let fixture: ComponentFixture<BuisnessScoringDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BuisnessScoringDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BuisnessScoringDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
