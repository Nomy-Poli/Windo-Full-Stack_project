import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OrderServiceRequestsListComponent } from './order-service-requests-list.component';

describe('OrderServiceRequestsListComponent', () => {
  let component: OrderServiceRequestsListComponent;
  let fixture: ComponentFixture<OrderServiceRequestsListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OrderServiceRequestsListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OrderServiceRequestsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
