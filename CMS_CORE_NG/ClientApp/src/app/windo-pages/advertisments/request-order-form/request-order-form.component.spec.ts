import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RequestOrderFormComponent } from './request-order-form.component';

describe('RequestOrderFormComponent', () => {
  let component: RequestOrderFormComponent;
  let fixture: ComponentFixture<RequestOrderFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RequestOrderFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RequestOrderFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
