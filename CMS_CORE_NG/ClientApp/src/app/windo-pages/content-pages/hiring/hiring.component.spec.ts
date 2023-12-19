import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HiringComponent } from './hiring.component';

describe('HiringComponent', () => {
  let component: HiringComponent;
  let fixture: ComponentFixture<HiringComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HiringComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HiringComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
