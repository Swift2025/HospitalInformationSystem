import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientFiltersComponent } from './patient-filters.component';

describe('PatientFiltersComponent', () => {
  let component: PatientFiltersComponent;
  let fixture: ComponentFixture<PatientFiltersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PatientFiltersComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PatientFiltersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
