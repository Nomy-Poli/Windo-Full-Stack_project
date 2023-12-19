import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdOrderFormComponent } from './ad-order-form.component';

describe('AdOrderFormComponent', () => {
  let component: AdOrderFormComponent;
  let fixture: ComponentFixture<AdOrderFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdOrderFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdOrderFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
