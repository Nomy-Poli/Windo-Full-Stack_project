import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BannerFormComponent } from './banner-form.component';

describe('BannerFormComponent', () => {
  let component: BannerFormComponent;
  let fixture: ComponentFixture<BannerFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BannerFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BannerFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
