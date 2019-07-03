import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';
import { Service } from './new-service';

@Injectable({
  providedIn: 'root'
})
export class AddServiceService {

  constructor(private http: HttpClient) { }

  private serviceUrl = 'http://localhost:59721/api/services/newservice';
  private _patientId = 0;

  get patientId(): number {
    return this._patientId;
  }

  set patientId(newId: number) {
    this._patientId = newId;
  }

  public createService(newService: Service): Observable<Service>{
    return this.http.post<Service>(this.serviceUrl, newService,{
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
