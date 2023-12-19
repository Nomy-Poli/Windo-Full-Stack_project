import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdvertismentAreaComponent } from './advertisment-area.component';

describe('AdvertismentAreaComponent', () => {
  let component: AdvertismentAreaComponent;
  let fixture: ComponentFixture<AdvertismentAreaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdvertismentAreaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdvertismentAreaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
