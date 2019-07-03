import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientDetailListTypesComponent } from './patient-detail-list-types.component';

describe('PatientDetailListTypesComponent', () => {
  let component: PatientDetailListTypesComponent;
  let fixture: ComponentFixture<PatientDetailListTypesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PatientDetailListTypesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PatientDetailListTypesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
