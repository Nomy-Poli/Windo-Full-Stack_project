import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PartnerConfirmedComponent } from './partner-confirmed.component';

describe('PartnerConfirmedComponent', () => {
  let component: PartnerConfirmedComponent;
  let fixture: ComponentFixture<PartnerConfirmedComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PartnerConfirmedComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PartnerConfirmedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
