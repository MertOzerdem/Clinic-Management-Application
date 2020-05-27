import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';
import { IService } from 'src/app/patient/patient-detail/patient-detail-list-services/service';
import { IServiceTypes } from 'src/app/patient/add-service/service-types';

@Injectable({
  providedIn: 'root'
})
export class DeleteProductTypeService {
  private deleteServiceTypeUrl = 'http://localhost:59721/api/services/servicetypes/';
  private deleteServiceTypeUrlExtension = '/relatedservices';

  constructor(private http: HttpClient) { }

  deleteServiceType(id: number): Observable<{}> {
    const url = this.deleteServiceTypeUrl + `/${id}`;
    return this.http.delete(url)
      .pipe(
        catchError(this.handleError)
      );
  }

  getServices(id : number): Observable<IService[]> {
    const url = this.deleteServiceTypeUrl + id + this.deleteServiceTypeUrlExtension;
    //console.log(url);
    
    return this.http.get<IService[]>(url).pipe(
      tap(data => console.log('All: ' + JSON.stringify(data))),
          catchError(this.handleError)
    );
  }

  getServiceType(id : number): Observable<IServiceTypes>{
    const url = this.deleteServiceTypeUrl + `${id}`;

    return this.http.get<IServiceTypes>(url).pipe(
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
