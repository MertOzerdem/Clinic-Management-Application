import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IService } from './service';
import { PatientDetailListServicesService } from './patient-detail-list-services.service';


@Component({
  selector: 'app-patient-detail-list-services',
  templateUrl: './patient-detail-list-services.component.html',
  styleUrls: ['./patient-detail-list-services.component.css']
})
export class PatientDetailListServicesComponent implements OnInit {
  services: IService[] = [];
  error : string;

  constructor(private route: ActivatedRoute,
              private router: Router,
              private patientServices: PatientDetailListServicesService) { }

  ngOnInit() {
    this.patientServices.restoreServiceAddress();
    let parentId = +this.route.parent.snapshot.paramMap.get('id');
    this.patientServices.patientId = parentId;
    
    // console.log("this is the patients id snapshot: " + parentId);
    // console.log("this is the patients id parent: ", + this.patientServices.patientId);
    this.patientServices.getServices().subscribe(
      services => {
        this.services = services,
        console.log(this.services)
      },
      error => this.error = <any>error,
    );
  }

}
