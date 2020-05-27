import { Component, OnInit } from '@angular/core';
import { IType } from './type';
import { ActivatedRoute, Router } from '@angular/router';
import { PatientDetailListTypesService } from './patient-detail-list-types.service';

@Component({
  selector: 'app-patient-detail-list-types',
  templateUrl: './patient-detail-list-types.component.html',
  styleUrls: ['./patient-detail-list-types.component.css']
})
export class PatientDetailListTypesComponent implements OnInit {
  types: IType[] = [];
  error : string;

  constructor(private route: ActivatedRoute,
              private router: Router,
              private serviceTypes: PatientDetailListTypesService) { }

  ngOnInit() {
    this.serviceTypes.restoreServiceTypeAddress();
    let id = +this.route.snapshot.paramMap.get('id');
    this.serviceTypes.typeId = id;
    
    // console.log("Inside Types id snapshot: " + id);
    // console.log("Inside Types id: ", + this.serviceTypes.typeId);
    this.serviceTypes.getTypes().subscribe(
      types => {
        this.types = types,
        console.log(this.types)
      },
      error => this.error = <any>error,
    );
  }

}
