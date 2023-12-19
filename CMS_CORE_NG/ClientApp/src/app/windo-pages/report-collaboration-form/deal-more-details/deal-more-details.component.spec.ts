import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DealMoreDetailsComponent } from './deal-more-details.component';

describe('DealMoreDetailsComponent', () => {
  let component: DealMoreDetailsComponent;
  let fixture: ComponentFixture<DealMoreDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DealMoreDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DealMoreDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
