import { Component, OnInit } from '@angular/core';
import { AddPatientService } from './add-patient.service';
import { PatientCreate } from '../patient-list/patient-create';
import { NewPatient } from './new-patient';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-add-patient',
  templateUrl: './add-patient.component.html',
  styleUrls: ['./add-patient.component.css']
})
export class AddPatientComponent implements OnInit {

  newPatient = new NewPatient("adbülkadir", "un kapanı"); // change with form
  _name : string = "";
  _surname : string = "";
  _email : string = "";
  targetId : number = 0;

  get name(): string {
    return this._name;
  }
  set name(value: string) {
    this._name = value;
  }

  get surname(): string {
    return this._surname;
  }
  set surname(value: string) {
    this._surname = value;
  }

  get email(): string {
    return this._email;
  }
  set email(value: string) {
    this._email = value;
  }



  onCreateNewPatient(): void{
    this.newPatient.Name = this._name;
    this.newPatient.Surname = this._surname;
    //this.patientService.httpPostExample();
   
    this.addPatientService.createPatient(this.newPatient).subscribe(
      (val) => {
          console.log("POST call successful value returned in body", val);
          this.targetId = val.patientId;
          //console.log("target ID: " + this.targetId);
          this.router.navigate([`/patients/${this.targetId}`]);
          
      },
      response => {
          console.log("POST call in error", response);
          
      },
      () => {
          console.log("The POST observable is now completed.");
      });
      
  }

  onBack(): void{
    //console.log("clicked onBack");
    this.router.navigate(['/patients']);
  }


  constructor(private addPatientService : AddPatientService, 
              private route : ActivatedRoute,
              private router: Router) { }

  ngOnInit() {
  }

}