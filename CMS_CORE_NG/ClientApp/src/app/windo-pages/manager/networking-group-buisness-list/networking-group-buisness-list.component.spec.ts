import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NetworkingGroupBuisnessListComponent } from './networking-group-buisness-list.component';

describe('NetworkingGroupBuisnessListComponent', () => {
  let component: NetworkingGroupBuisnessListComponent;
  let fixture: ComponentFixture<NetworkingGroupBuisnessListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NetworkingGroupBuisnessListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NetworkingGroupBuisnessListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
