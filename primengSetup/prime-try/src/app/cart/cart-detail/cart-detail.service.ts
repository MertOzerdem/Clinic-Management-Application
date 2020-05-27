import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { ICart } from '../cart';
import { Observable, throwError } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CartDetailService {
  private paymentUrl = 'http://localhost:59721/api/payments/';
  private deleteCartUrl = 'http://localhost:59721/api/payments/';

  constructor(private http: HttpClient) { }

  getPayment(id: number): Observable<ICart> {
    const url = this.paymentUrl + `${id}`;
    //console.log(this.patientUrl);
    return this.http.get<ICart>(url).pipe(
      tap(data => console.log('All: ' + JSON.stringify(data))),
          catchError(this.handleError)
    );
  }

  deleteTransaction(id : number): Observable<{}>{
    const url = this.deleteCartUrl + `${id}`;
    return this.http.delete(url)
      .pipe(
        catchError(this.handleError)
      );
  }

  patchTransaction(id : number, amount : number, method : string): Observable<ICart>{
    const url = this.deleteCartUrl + `${id}`;  
    return this.http.patch<ICart>(url,[
        {
          "op": "replace",
          "path": "/amount",
          "value": amount
        },
        {
          "op": "replace",
          "path": "/method",
          "value": `${method}`
        }
      ],
      {
        headers: new HttpHeaders({
          'Content-Type' : 'application/json'
        })
      })
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
