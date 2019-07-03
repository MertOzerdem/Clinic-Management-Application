import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CartDetailService } from './cart-detail.service';
import { ICart, Cart } from '../cart';

@Component({
  selector: 'app-cart-detail',
  templateUrl: './cart-detail.component.html',
  styleUrls: ['./cart-detail.component.css']
})
export class CartDetailComponent implements OnInit {
  payment: ICart;
  currentPayment: Cart = new Cart(0,0,"",0);
  error : string;
  _newMethod : string;
  _newAmount : number;

  get newMethod(): string {
    return this._newMethod;
  }
  set newMethod(value: string) {
    this._newMethod = value;
  }

  get newAmount(): number {
    return this._newAmount;
  }
  set newAmount(value: number) {
    this._newAmount = value;
  }

  onUpdate(){
    this.cartDetailService.patchTransaction(this.currentPayment.paymentId,
                                            this._newAmount,
                                            this._newMethod).subscribe();
  }

  onCancel(){
    if(this.currentPayment.paymentId > 0){
      this.cartDetailService.deleteTransaction(this.currentPayment.paymentId).subscribe();
      this.router.navigate([`/patients/${this.currentPayment.patientId}`]);
    }
  }

  onBack(){
      this.router.navigate(['/cart']);
  }

  constructor(private route: ActivatedRoute,
    private router: Router,
    private cartDetailService: CartDetailService) { }

  ngOnInit() {
    let id = +this.route.snapshot.paramMap.get('id');
    // get desired patient
    this.cartDetailService.getPayment(id).subscribe(
      payment => {
        this.payment = payment,
        this.currentPayment = payment,
        this.newMethod = payment.method,
        this.newAmount = payment.amount
      },
      error => this.error = <any>error,
    );
  }

}
