import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BusinessTableComponent } from './business-table.component';

describe('BusinessTableComponent', () => {
  let component: BusinessTableComponent;
  let fixture: ComponentFixture<BusinessTableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BusinessTableComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BusinessTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
