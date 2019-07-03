import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IPatient, Patient } from '../patient';
import {MenuItem} from 'primeng/api';
import { PatientListService } from './patient-detail.service';


@Component({
  selector: 'app-patient-detail',
  templateUrl: './patient-detail.component.html',
  styleUrls: ['./patient-detail.component.css']
})
export class PatientDetailComponent implements OnInit {
  pageTitle: string = 'Patient Id: ';
  patient: IPatient = null;
  currentPatient : Patient = new Patient(0,"","","",0); 
  error : string;
  items: MenuItem[];

  constructor(private route: ActivatedRoute,
              private router: Router,
              private patientListService: PatientListService) { }

  ngOnInit() {
    this.patientListService.restorePatientAddress();
    let id = +this.route.snapshot.paramMap.get('id');
    this.patientListService.id = id;
    // console.log("this is the patients id snapshot: " + id);
    // console.log("this is the patients id parent: ", + this.patientListService.id);

    // get desired patient
    this.patientListService.getPatient().subscribe(
      patient => {
        this.patient = patient,
        this.currentPatient = patient;
      },
      error => this.error = <any>error,
    );
    this.items = [
      {label: 'List Transactions', icon: 'fa fa-fw fa-bar-chart', routerLink: ['/patients', id, 'transactions']},
      {label: 'List Services', icon: 'fa fa-fw fa-bar-chart', routerLink: ['/patients', id, 'services']},
    ];
  }

  onDelete(){
    let id = +this.route.snapshot.paramMap.get('id');
    //console.log(id);
    this.patientListService.deletePatient(id).subscribe();
    this.router.navigate([`/patients`]);
  }

  onBack(): void{
    //console.log("clicked onBack");
    this.router.navigate(['/patients']);
  }

}
