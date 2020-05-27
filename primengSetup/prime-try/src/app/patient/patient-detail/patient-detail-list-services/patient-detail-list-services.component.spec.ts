import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientDetailListServicesComponent } from './patient-detail-list-services.component';

describe('PatientDetailListServicesComponent', () => {
  let component: PatientDetailListServicesComponent;
  let fixture: ComponentFixture<PatientDetailListServicesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PatientDetailListServicesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PatientDetailListServicesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
