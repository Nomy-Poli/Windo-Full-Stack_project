import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AlphonUsersComponent } from './alphon-users.component';

describe('AlphonUsersComponent', () => {
  let component: AlphonUsersComponent;
  let fixture: ComponentFixture<AlphonUsersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AlphonUsersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlphonUsersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
