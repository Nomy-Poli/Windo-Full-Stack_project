import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SendEmailAgainComponent } from './send-email-again.component';

describe('SendEmailAgainComponent', () => {
  let component: SendEmailAgainComponent;
  let fixture: ComponentFixture<SendEmailAgainComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SendEmailAgainComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SendEmailAgainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
