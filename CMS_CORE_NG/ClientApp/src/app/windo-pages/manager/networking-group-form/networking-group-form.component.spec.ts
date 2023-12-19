import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NetworkingGroupFormComponent } from './networking-group-form.component';

describe('NetworkingGroupFormComponent', () => {
  let component: NetworkingGroupFormComponent;
  let fixture: ComponentFixture<NetworkingGroupFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NetworkingGroupFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NetworkingGroupFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
