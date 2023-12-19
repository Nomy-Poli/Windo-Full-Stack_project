import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportCollaborationFormComponent } from './report-collaboration-form.component';

describe('ReportCollaborationFormComponent', () => {
  let component: ReportCollaborationFormComponent;
  let fixture: ComponentFixture<ReportCollaborationFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReportCollaborationFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReportCollaborationFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
