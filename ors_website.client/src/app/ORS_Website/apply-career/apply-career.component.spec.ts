import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApplyCareerComponent } from './apply-career.component';

describe('ApplyCareerComponent', () => {
  let component: ApplyCareerComponent;
  let fixture: ComponentFixture<ApplyCareerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ApplyCareerComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ApplyCareerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
