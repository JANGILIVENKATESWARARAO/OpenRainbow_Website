import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminCareerComponent } from './admin-career.component';

describe('AdminCareerComponent', () => {
  let component: AdminCareerComponent;
  let fixture: ComponentFixture<AdminCareerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AdminCareerComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminCareerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
