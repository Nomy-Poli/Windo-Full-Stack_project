import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WithoutBussinessComponent } from './without-bussiness.component';

describe('WithoutBussinessComponent', () => {
  let component: WithoutBussinessComponent;
  let fixture: ComponentFixture<WithoutBussinessComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WithoutBussinessComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WithoutBussinessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
