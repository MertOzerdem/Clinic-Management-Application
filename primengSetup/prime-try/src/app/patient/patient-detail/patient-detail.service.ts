import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { IPatient } from '../patient';

@Injectable({
  providedIn: 'root'
})
export class PatientListService {

  private patientUrl = 'http://localhost:59721/api/patients/';
  private _id = 0;

  restorePatientAddress(): void{
    this.patientUrl = 'http://localhost:59721/api/patients/';
  }

  get id(): number {
    return this._id;
  }

  set id(newId: number) {
    this._id = newId;
  } 
  
  
  constructor(private http: HttpClient) { }

  getPatient(): Observable<IPatient> {
    this.patientUrl = 'http://localhost:59721/api/patients/';
    this.patientUrl =  this.patientUrl + `${this._id}`;
    //console.log(this.patientUrl);
    return this.http.get<IPatient>(this.patientUrl).pipe(
      tap(data => console.log('All: ' + JSON.stringify(data))),
          catchError(this.handleError)
    );
  }

  deletePatient(id: number): Observable<{}> {
    this.patientUrl = 'http://localhost:59721/api/patients/';
    const url = this.patientUrl + `${id}`;
    return this.http.delete(url)
      .pipe(
        catchError(this.handleError)
      );
  }
  
  private handleError(err: HttpErrorResponse) {
    let errorMessage = '';
    if (err.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
    }
    console.error(errorMessage);
    return throwError(errorMessage);
  }
}
