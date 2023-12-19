import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CaseStudyCardesComponent } from './case-study-cardes.component';

describe('CaseStudyCardesComponent', () => {
  let component: CaseStudyCardesComponent;
  let fixture: ComponentFixture<CaseStudyCardesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CaseStudyCardesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CaseStudyCardesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
