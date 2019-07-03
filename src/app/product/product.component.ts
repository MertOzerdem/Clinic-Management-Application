import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AllTypesService } from '../patient/add-service/all-types.service';
import { IServiceTypes } from '../patient/add-service/service-types';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
  serviceTypes: IServiceTypes[] = [];
  error : string;
  deleteItem : number;

  constructor(private route : ActivatedRoute,
    private router: Router,
    private allTypesService: AllTypesService) { }

  ngOnInit() {
    this.allTypesService.getServiceTypes().subscribe(
      serviceTypes => {
        this.serviceTypes = serviceTypes
      },
      error => this.error = <any>error,
    ); 
  }

}
