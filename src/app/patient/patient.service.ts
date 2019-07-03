import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { IPatient } from './patient';
import { catchError, tap, map } from 'rxjs/operators';
import { IPatientCreate, PatientCreate } from './patient-list/patient-create';


@Injectable({
  providedIn: 'root'
})
export class PatientService {

  private patientUrl = 'http://localhost:59721/api/patients';

  constructor(private http: HttpClient) { }

  public createPatient(newPatient: PatientCreate): Observable<PatientCreate>{
    return this.http.post<PatientCreate>("http://localhost:59721/api/patients/patientadd", newPatient,{
      headers: new HttpHeaders({
        'Content-Type' : 'application/json'
      })
    }).pipe(catchError(this.handleError));
  }

  getPatient(): Observable<IPatient[]> {
    return this.http.get<IPatient[]>(this.patientUrl).pipe(
      tap(data => console.log('All: ' + JSON.stringify(data))),
          catchError(this.handleError)
    );
  }
  
  private handleError(err: HttpErrorResponse) {
    let errorMessage = '';
    if (err.error instanceof ErrorEvent) {
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
    }
    console.error(errorMessage);
    return throwError(errorMessage);
  }
}
