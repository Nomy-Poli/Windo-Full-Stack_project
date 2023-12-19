import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CollaborationListComponent } from './collaboration-list.component';

describe('CollaborationListComponent', () => {
  let component: CollaborationListComponent;
  let fixture: ComponentFixture<CollaborationListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CollaborationListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CollaborationListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
