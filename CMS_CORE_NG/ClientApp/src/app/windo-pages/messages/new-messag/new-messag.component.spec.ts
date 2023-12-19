import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewMessagComponent } from './new-messag.component';

describe('NewMessagComponent', () => {
  let component: NewMessagComponent;
  let fixture: ComponentFixture<NewMessagComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewMessagComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewMessagComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
