import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CartService } from './cart.service';
import { ICart } from './cart';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  cartObject: ICart[] = [];
  error : string;
  targetId : number;
  patientToReturn : number = 0;
  
  constructor(
    private route : ActivatedRoute,
    private router: Router,
    private cartService: CartService) { }

  ngOnInit() {
    this.cartService.getCartItems().subscribe(
      cartObject => {
        this.cartObject = cartObject
      },
      error => this.error = <any>error,
    );
  }

}
