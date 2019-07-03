import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { IPayment } from './payment';

@Injectable({
  providedIn: 'root'
})
export class PatientDetailListTransactionsService {
  
  private paymentUrl = 'http://localhost:59721/api/patients/';
  private paymentExtension ='/payments';

  private _patientId = 0;

  restorePaymentAddress(): void{
    this.paymentUrl = 'http://localhost:59721/api/patients/';
  }

  get patientId(): number {
    return this._patientId;
  }

  set patientId(newId: number) {
    this._patientId = newId;
  } 

  constructor(private http: HttpClient) { }

  getPayments(): Observable<IPayment[]> {
    this.paymentUrl =  this.paymentUrl + `${this._patientId}` + this.paymentExtension;
    //console.log(this.paymentUrl);
    
    return this.http.get<IPayment[]>(this.paymentUrl).pipe(
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
