import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ScoreManagementAreaComponent } from './score-management-area.component';

describe('ScoreManagementAreaComponent', () => {
  let component: ScoreManagementAreaComponent;
  let fixture: ComponentFixture<ScoreManagementAreaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ScoreManagementAreaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ScoreManagementAreaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
