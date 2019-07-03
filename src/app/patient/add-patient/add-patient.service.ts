import { Injectable } from '@angular/core';
import { catchError, tap, map } from 'rxjs/operators';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { NewPatient } from './new-patient';

@Injectable({
  providedIn: 'root'
})
export class AddPatientService {

  constructor(private http: HttpClient) { }

  public createPatient(newPatient: NewPatient): Observable<NewPatient>{
    return this.http.post<NewPatient>("http://localhost:59721/api/patients/patientadd", newPatient,{
      headers: new HttpHeaders({
        'Content-Type' : 'application/json'
      })
    }).pipe(catchError(this.handleError));
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
