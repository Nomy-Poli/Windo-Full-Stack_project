import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NetworkingManagmentAreaComponent } from './networking-managment-area.component';

describe('NetworkingManagmentAreaComponent', () => {
  let component: NetworkingManagmentAreaComponent;
  let fixture: ComponentFixture<NetworkingManagmentAreaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NetworkingManagmentAreaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NetworkingManagmentAreaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
