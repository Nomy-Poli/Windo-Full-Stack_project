import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchDomainComponent } from './search-domain.component';

describe('SearchDomainComponent', () => {
  let component: SearchDomainComponent;
  let fixture: ComponentFixture<SearchDomainComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SearchDomainComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchDomainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
