import { Component, OnInit } from '@angular/core';
import { Service } from './new-service';
import { Type } from './type';
import { ActivatedRoute, Router } from '@angular/router';
import { AddServiceService } from './add-service.service';
import { IServiceTypes } from './service-types';
import { AllTypesService } from './all-types.service';

@Component({
  selector: 'app-add-service',
  templateUrl: './add-service.component.html',
  styleUrls: ['./add-service.component.css']
})
export class AddServiceComponent implements OnInit {

  selectedTypes: Type[] = [];
  serviceTypes: IServiceTypes[] = [];
  error : string;
  deneme : Type;
  _providedServices : Type[] = [];
  newService = new Service(0,0,"",this._providedServices); // changes with form
  _discount : number = 0;
  _patientId : number;
  _provider : string;

  sType(value : number): string{
    return this.selectedTypes[value].type;
  }

  get patientId(): number {
    return this._patientId;
  }
  set patientId(value: number) {
    this._patientId = value;
  }

  get discount(): number {
    return this._discount;
  }
  set discount(value: number) {
    this._discount = value;
  }

  get provider(): string {
    return this._provider;
  }
  set provider(value: string) {
    this._provider = value;
  }

  get providedServices(): Type[] {
    return this._providedServices;
  }
  set providedServices(value: Type[]) {
    this._providedServices = value;
  }
  
  onCreateService(): void{
    this.newService.discount = this._discount;
    this.newService.provider = this._provider;
    let parentId = +this.route.parent.snapshot.paramMap.get('id');
    //console.log("target ID: " + parentId);
    this.addServiceService.patientId = parentId;
    this.newService.patientId = this.patientId;

    for(let serviceType of this.serviceTypes){
     // console.log("service Types: " + serviceType.type);
      for(let selectedType of this.selectedTypes){
        let temp = selectedType.toString(); // BU TAMAMEN SAÇMALIKTIR RESMEN PRİMENGNİN BUGU YÜZÜNDEN 
        //console.log("selected Services: " + temp); // 4 SAAT UĞRAŞTIM. 
        if(serviceType.type === temp){
          //console.log("inside: " + selectedType);
          // 8 kere basıyo hallet
          let length = this.newService.providedServices.push(selectedType); 
        }
      } 
    }
    // console.log("provided services: "+ this.newService.providedServices);
    // console.log(JSON.stringify(this.newService));

    this.addServiceService.createService(this.newService).subscribe(
      (val) => {
          console.log("POST call successful value returned in body", val);
          console.log("target ID: " + parentId);
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
              private addServiceService : AddServiceService,
              private allTypesService: AllTypesService) { }

  ngOnInit() {
    let parentId = +this.route.parent.snapshot.paramMap.get('id');
    this.newService.patientId = parentId;
    this._patientId = parentId;
    

    this.allTypesService.getServiceTypes().subscribe(
      serviceTypes => {
        this.serviceTypes = serviceTypes
      },
      error => this.error = <any>error,
    ); 
  }
  
  onDummy(){
    console.log(this.selectedTypes);
  }
}
