import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PatientDetailListTransactionsService } from './patient-detail-list-transactions.service';
import { IPayment } from './payment';

@Component({
  selector: 'app-patient-detail-list-transactions',
  templateUrl: './patient-detail-list-transactions.component.html',
  styleUrls: ['./patient-detail-list-transactions.component.css']
})
export class PatientDetailListTransactionsComponent implements OnInit {
  payments: IPayment[] = [];
  error : string;

  constructor(private route: ActivatedRoute,
              private router: Router,
              private patientTransactions: PatientDetailListTransactionsService) { }

  ngOnInit() {
    this.patientTransactions.restorePaymentAddress();
    let parentId = +this.route.parent.snapshot.paramMap.get('id');
    this.patientTransactions.patientId = parentId;
    
    // console.log("this is the patients id snapshot: " + parentId);
    // console.log("this is the patients id parent: ", + this.patientTransactions.patientId);
    this.patientTransactions.getPayments().subscribe(
      payments => {
        this.payments = payments,
        console.log(this.payments)
      },
      error => this.error = <any>error,
    );
  }

}
