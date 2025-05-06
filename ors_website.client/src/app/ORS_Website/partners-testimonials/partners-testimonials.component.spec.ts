import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PartnersTestimonialsComponent } from './partners-testimonials.component';

describe('PartnersTestimonialsComponent', () => {
  let component: PartnersTestimonialsComponent;
  let fixture: ComponentFixture<PartnersTestimonialsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PartnersTestimonialsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PartnersTestimonialsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
