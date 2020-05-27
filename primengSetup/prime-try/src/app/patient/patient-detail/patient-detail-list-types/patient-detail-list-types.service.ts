import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { IType } from './type';
import { Observable, throwError } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PatientDetailListTypesService {
  private serviceTypeUrl = 'http://localhost:59721/api/services/';
  private serviceTypeExtension ='/servicetypes';

  private _typeId = 0;

  restoreServiceTypeAddress(): void{
    this.serviceTypeUrl = 'http://localhost:59721/api/services/';
  }

  get typeId(): number {
    return this._typeId;
  }

  set typeId(newId: number) {
    this._typeId = newId;
  } 

  constructor(private http: HttpClient) { }

  getTypes(): Observable<IType[]> {
    this.serviceTypeUrl =  this.serviceTypeUrl + `${this._typeId}` + this.serviceTypeExtension;
    //console.log("This is the URL" + this.serviceTypeUrl);
    
    return this.http.get<IType[]>(this.serviceTypeUrl).pipe(
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
