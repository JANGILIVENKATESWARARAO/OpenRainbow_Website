import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrsWebsiteComponent } from './ors-website.component';

describe('OrsWebsiteComponent', () => {
  let component: OrsWebsiteComponent;
  let fixture: ComponentFixture<OrsWebsiteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [OrsWebsiteComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OrsWebsiteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
