import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WindoSiteServicesFormComponent } from './windo-site-services-form.component';

describe('WindoSiteServicesFormComponent', () => {
  let component: WindoSiteServicesFormComponent;
  let fixture: ComponentFixture<WindoSiteServicesFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WindoSiteServicesFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WindoSiteServicesFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
