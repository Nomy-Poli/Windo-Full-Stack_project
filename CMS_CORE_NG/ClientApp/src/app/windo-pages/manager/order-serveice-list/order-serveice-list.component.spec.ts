import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OrderServeiceListComponent } from './order-serveice-list.component';

describe('OrderServeiceListComponent', () => {
  let component: OrderServeiceListComponent;
  let fixture: ComponentFixture<OrderServeiceListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OrderServeiceListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OrderServeiceListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
