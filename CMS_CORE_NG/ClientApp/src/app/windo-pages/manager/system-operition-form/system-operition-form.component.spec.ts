import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SystemOperitionFormComponent } from './system-operition-form.component';

describe('SystemOperitionFormComponent', () => {
  let component: SystemOperitionFormComponent;
  let fixture: ComponentFixture<SystemOperitionFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SystemOperitionFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SystemOperitionFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
