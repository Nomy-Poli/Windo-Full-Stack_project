import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManagerAreaComponent } from './manager-area.component';

describe('ManagerAreaComponent', () => {
  let component: ManagerAreaComponent;
  let fixture: ComponentFixture<ManagerAreaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManagerAreaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManagerAreaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
