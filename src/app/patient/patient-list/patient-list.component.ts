import { Component, OnInit } from '@angular/core';
import { IPatient } from '../patient';
import { PatientService } from '../patient.service';
import { IPatientCreate, PatientCreate } from './patient-create';


@Component({
  selector: 'app-patient-list',
  templateUrl: './patient-list.component.html',
  styleUrls: ['./patient-list.component.css']
})
export class PatientListComponent implements OnInit {

  patients : IPatient[] = [];
  error : string;
  filterDecider: number = 0;
  filterDeciderName: string = "By Id ...";
  constructor(private patientService: PatientService ) { 
    this.filteredPatients = this.patients;
    this.listFilter = '';
  }

  filteredPatients: IPatient[] = [];
  _listFilter = '';
  get listFilter(): string {
    //console.log(this._listFilter);
    return this._listFilter;
  }
  set listFilter(value: string) {
    //console.log(this._listFilter);
    this._listFilter = value;
    if (this.filterDecider == 0){
      //console.log("inside ID filter");
      this.filteredPatients = this.listFilter ? this.performFilterId(this.listFilter) : this.patients;
    }
    if(this.filterDecider == 1){
      //console.log("inside name filter");
      this.filteredPatients = this.listFilter ? this.performFilterName(this.listFilter) : this.patients;
    } 
  }

  performFilterName(filterBy: string): IPatient[] {
    //console.log(this._listFilter);
    filterBy = filterBy.toLocaleLowerCase();
    return this.patients.filter((patient: IPatient) =>
      (patient.name + ' ' + patient.surname).toLocaleLowerCase().indexOf(filterBy) !== -1);
  }

  performFilterId(filterBy :string): IPatient[]{
    filterBy = filterBy.toLocaleLowerCase();
    return this.patients.filter((patient: IPatient) =>
      patient.patientId.toString().indexOf(filterBy) !== -1);
  }

  onClickId(): void{
    this.filterDecider = 0;
    this.filterDeciderName = "By Id ...";
  }

  onClickName(): void{
    this.filterDecider = 1;
    this.filterDeciderName = "By Name/Surname ...";
  }

  public newPatient = new PatientCreate("ad", "soyad"); 
  onCreateNewPatient(): void{
    this.patientService.createPatient(this.newPatient).subscribe(
      (val) => {
          console.log("POST call successful value returned in body", val);
      },
      response => {
          console.log("POST call in error", response);
      },
      () => {
          console.log("The POST observable is now completed.");
      });
  }

  ngOnInit() : void {
    this.patientService.getPatient().subscribe(
      patients => {
        this.patients = patients,
        this.filteredPatients = this.patients;
        //console.log(this.patients)
      },
      error => this.error = <any>error,
    );
    
  }

}
