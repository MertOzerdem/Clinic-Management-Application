import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DeleteProductTypeService } from './delete-product-type.service';
import { IService } from 'src/app/patient/patient-detail/patient-detail-list-services/service';
import { IServiceTypes, ServiceType } from 'src/app/patient/add-service/service-types';

@Component({
  selector: 'app-delete-product-type',
  templateUrl: './delete-product-type.component.html',
  styleUrls: ['./delete-product-type.component.css']
})
export class DeleteProductTypeComponent implements OnInit {
  deleteItem : number;
  services: IService[] = [];
  serviceType: IServiceTypes;
  currentServiceType: ServiceType = new ServiceType(0,0,"");
  error : string;

  onClick(){
    let id = +this.route.snapshot.paramMap.get('id');
    //console.log(id);
    this.deleteProductTypeService.deleteServiceType(id).subscribe();
    this.router.navigate([`/products`]);
  }

  onBack(): void{
    this.router.navigate(['/products']);
  }

  constructor(private route : ActivatedRoute,
    private router: Router,
    private deleteProductTypeService: DeleteProductTypeService) { }

  ngOnInit() {
    let productId = +this.route.snapshot.paramMap.get('id');
    this.deleteProductTypeService.getServices(productId).subscribe(
      services => {
        this.services = services
      },
      error => this.error = <any>error,
    );

    this.deleteProductTypeService.getServiceType(productId).subscribe(
      serviceType => {
        this.serviceType = serviceType
        this.currentServiceType = serviceType;
      },
      error => this.error = <any>error,
    );
  }
}
