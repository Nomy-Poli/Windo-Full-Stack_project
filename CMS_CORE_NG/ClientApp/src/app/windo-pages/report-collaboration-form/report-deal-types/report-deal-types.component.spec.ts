import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportDealTypesComponent } from './report-deal-types.component';

describe('ReportDealTypesComponent', () => {
  let component: ReportDealTypesComponent;
  let fixture: ComponentFixture<ReportDealTypesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReportDealTypesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReportDealTypesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
