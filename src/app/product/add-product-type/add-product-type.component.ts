import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AddProductTypeService } from './add-product-type.service';
import { Product } from './product';

@Component({
  selector: 'app-add-product-type',
  templateUrl: './add-product-type.component.html',
  styleUrls: ['./add-product-type.component.css']
})
export class AddProductTypeComponent implements OnInit {

  _fee : number;
  _type : string = "";
  newProduct = new Product(0, "");

  get fee(): number {
    return this._fee;
  }
  set fee(value: number) {
    this._fee = value;
  }

  get type(): string {
    return this._type;
  }
  set type(value: string) {
    this._type = value;
  }

  onCreateProduct(): void{
    this.newProduct.Fee = this._fee;
    this.newProduct.Type = this._type;

    this.addProductTypeService.createProduct(this.newProduct).subscribe(
      (val) => {
          console.log("POST call successful value returned in body", val);
          this.router.navigate([`/products`]);
      },
      response => {
          console.log("POST call in error", response);
          
      },
      () => {
          console.log("The POST observable is now completed.");
      });
  }

  onBack(){
    this.router.navigate([`/products`]);
  }

  constructor(private route : ActivatedRoute,
              private router: Router,
              private addProductTypeService: AddProductTypeService) { }

  ngOnInit() {
  }

}
