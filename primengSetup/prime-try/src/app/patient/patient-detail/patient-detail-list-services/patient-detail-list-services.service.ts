import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { IService } from './service';


@Injectable({
  providedIn: 'root'
})
export class PatientDetailListServicesService {
  
  private serviceUrl = 'http://localhost:59721/api/patients/';
  private serviceExtension ='/services';

  private _patientId = 0;

  restoreServiceAddress(): void{
    this.serviceUrl = 'http://localhost:59721/api/patients/';
  }

  get patientId(): number {
    return this._patientId;
  }

  set patientId(newId: number) {
    this._patientId = newId;
  } 

  constructor(private http: HttpClient) { }

  getServices(): Observable<IService[]> {
    this.serviceUrl =  this.serviceUrl + `${this._patientId}` + this.serviceExtension;
    //console.log(this.serviceUrl);
    
    return this.http.get<IService[]>(this.serviceUrl).pipe(
      tap(data => console.log('All: ' + JSON.stringify(data))),
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
