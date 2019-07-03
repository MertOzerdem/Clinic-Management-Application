import { Component, OnInit } from '@angular/core';
import { Transaction } from './transaction';
import { AddTransactionService } from './add-transaction.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-add-transaction',
  templateUrl: './add-transaction.component.html',
  styleUrls: ['./add-transaction.component.css']
})
export class AddTransactionComponent implements OnInit {
  newTransaction = new Transaction(0, "", 0); // changes with form
  _patientId : number;
  _method : string;
  _amount : number;

  get patientId(): number {
    return this._patientId;
  }
  set patientId(value: number) {
    this._patientId = value;
  }

  get amount(): number {
    return this._amount;
  }
  set amount(value: number) {
    this._amount = value;
  }

  get method(): string {
    return this._method;
  }
  set method(value: string) {
    this._method = value;
  }

  onCreateTransaction(): void{
    this.newTransaction.Amount = this._amount;
    this.newTransaction.method = this._method; 
    let parentId = +this.route.parent.snapshot.paramMap.get('id');
    //console.log("target ID: " + parentId);
    this.addTransactionService.patientId = parentId; // bu ne bilmiyorum allahım çok zor durum insan yazdıgı kodu nasıl unutur
    this.newTransaction.patientId = this.patientId;

    //this.patientService.httpPostExample();
    this.addTransactionService.createTransaction(this.newTransaction).subscribe(
      (val) => {
          console.log("POST call successful value returned in body", val);
          //console.log("target ID: " + parentId);
          this.router.navigate([`/patients/${parentId}`]);
          
      },
      response => {
          console.log("POST call in error", response);
          
      },
      () => {
          console.log("The POST observable is now completed.");
      });
  }

  onBack(){
    let parentId = +this.route.parent.snapshot.paramMap.get('id');
    this.router.navigate([`/patients/${parentId}`]);
  }
  
  constructor(private route : ActivatedRoute,
              private router: Router,
              private addTransactionService : AddTransactionService) { }

  ngOnInit() {
    let parentId = +this.route.parent.snapshot.paramMap.get('id');
    this.newTransaction.patientId = parentId;
    this._patientId = parentId;
  }

}
