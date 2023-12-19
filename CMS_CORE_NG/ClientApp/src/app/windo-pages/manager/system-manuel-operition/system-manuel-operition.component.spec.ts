import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SystemManuelOperitionComponent } from './system-manuel-operition.component';

describe('SystemManuelOperitionComponent', () => {
  let component: SystemManuelOperitionComponent;
  let fixture: ComponentFixture<SystemManuelOperitionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SystemManuelOperitionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SystemManuelOperitionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
