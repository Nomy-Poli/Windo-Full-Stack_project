import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BanersCatalogComponent } from './baners-catalog.component';

describe('BanersCatalogComponent', () => {
  let component: BanersCatalogComponent;
  let fixture: ComponentFixture<BanersCatalogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BanersCatalogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BanersCatalogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
