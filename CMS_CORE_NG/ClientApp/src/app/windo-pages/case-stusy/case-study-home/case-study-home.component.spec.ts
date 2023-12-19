import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CaseStudyHomeComponent } from './case-study-home.component';

describe('CaseStudyHomeComponent', () => {
  let component: CaseStudyHomeComponent;
  let fixture: ComponentFixture<CaseStudyHomeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CaseStudyHomeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CaseStudyHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
