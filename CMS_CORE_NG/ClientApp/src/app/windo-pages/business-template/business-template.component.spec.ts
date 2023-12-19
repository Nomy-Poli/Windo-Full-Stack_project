import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BusinessTemplateComponent } from './business-template.component';

describe('BusinessTemplateComponent', () => {
  let component: BusinessTemplateComponent;
  let fixture: ComponentFixture<BusinessTemplateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BusinessTemplateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BusinessTemplateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
