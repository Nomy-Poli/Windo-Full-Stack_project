import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NetworkingGroupListComponent } from './networking-group-list.component';

describe('NetworkingGroupListComponent', () => {
  let component: NetworkingGroupListComponent;
  let fixture: ComponentFixture<NetworkingGroupListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NetworkingGroupListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NetworkingGroupListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
