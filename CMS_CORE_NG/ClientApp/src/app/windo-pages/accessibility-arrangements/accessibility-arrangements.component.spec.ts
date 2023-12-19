import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AccessibilityArrangementsComponent } from './accessibility-arrangements.component';

describe('AccessibilityArrangementsComponent', () => {
  let component: AccessibilityArrangementsComponent;
  let fixture: ComponentFixture<AccessibilityArrangementsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AccessibilityArrangementsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AccessibilityArrangementsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
