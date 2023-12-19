import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SystemOperationsListComponent } from './system-operations-list.component';

describe('SystemOperationsListComponent', () => {
  let component: SystemOperationsListComponent;
  let fixture: ComponentFixture<SystemOperationsListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SystemOperationsListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SystemOperationsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
