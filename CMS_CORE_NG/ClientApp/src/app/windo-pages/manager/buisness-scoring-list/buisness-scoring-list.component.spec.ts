import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BuisnessScoringListComponent } from './buisness-scoring-list.component';

describe('BuisnessScoringListComponent', () => {
  let component: BuisnessScoringListComponent;
  let fixture: ComponentFixture<BuisnessScoringListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BuisnessScoringListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BuisnessScoringListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
