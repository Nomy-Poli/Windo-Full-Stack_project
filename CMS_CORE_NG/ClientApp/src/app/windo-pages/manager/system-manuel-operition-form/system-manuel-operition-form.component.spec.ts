import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SystemManuelOperitionFormComponent } from './system-manuel-operition-form.component';

describe('SystemManuelOperitionFormComponent', () => {
  let component: SystemManuelOperitionFormComponent;
  let fixture: ComponentFixture<SystemManuelOperitionFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SystemManuelOperitionFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SystemManuelOperitionFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
