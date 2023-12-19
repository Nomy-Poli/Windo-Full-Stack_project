import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CaseStudyCardComponent } from './case-study-form.component';

describe('CaseStudyCardComponent', () => {
  let component: CaseStudyCardComponent;
  let fixture: ComponentFixture<CaseStudyCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CaseStudyCardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CaseStudyCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
