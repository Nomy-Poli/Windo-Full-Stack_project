import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BarterListComponent } from './barter-list.component';

describe('BarterListComponent', () => {
  let component: BarterListComponent;
  let fixture: ComponentFixture<BarterListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BarterListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BarterListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
