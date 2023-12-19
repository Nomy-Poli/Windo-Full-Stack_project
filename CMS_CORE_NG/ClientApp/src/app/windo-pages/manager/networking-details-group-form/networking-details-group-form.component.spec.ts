import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NetworkingDetailsGroupFormComponent } from './networking-details-group-form.component';

describe('NetworkingDetailsGroupFormComponent', () => {
  let component: NetworkingDetailsGroupFormComponent;
  let fixture: ComponentFixture<NetworkingDetailsGroupFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NetworkingDetailsGroupFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NetworkingDetailsGroupFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
