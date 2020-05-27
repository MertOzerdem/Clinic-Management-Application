import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { AppComponent } from './app.component';
import { AccordionModule } from 'primeng/accordion';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'
import { TableModule } from 'primeng/table';
import { RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { PatientListComponent } from './patient/patient-list/patient-list.component';
import { HttpClientModule } from '@angular/common/http';
import { PatientDetailComponent } from './patient/patient-detail/patient-detail.component';
import { PatientDetailListTransactionsComponent } from './patient/patient-detail/patient-detail-list-transactions/patient-detail-list-transactions.component';
import {TabMenuModule} from 'primeng/tabmenu';
import {MenuItem} from 'primeng/api';
import { MenuModule } from 'primeng/menu';
import { PatientDetailListServicesComponent } from './patient/patient-detail/patient-detail-list-services/patient-detail-list-services.component';
import { FormsModule } from '@angular/forms';
import { PatientDetailListTypesComponent } from './patient/patient-detail/patient-detail-list-types/patient-detail-list-types.component';
import { AddPatientComponent } from './patient/add-patient/add-patient.component';
import { AddTransactionComponent } from './patient/add-transaction/add-transaction.component';
import { AddServiceComponent } from './patient/add-service/add-service.component';
import { CheckboxModule } from 'primeng/checkbox';
import { ProductComponent } from './product/product.component';
import { AddProductTypeComponent } from './product/add-product-type/add-product-type.component';
import { DeleteProductTypeComponent } from './product/delete-product-type/delete-product-type.component';
import { CartComponent } from './cart/cart.component';
import { InputTextModule } from 'primeng/inputtext';
import { CartDetailComponent } from './cart/cart-detail/cart-detail.component';

@NgModule({
  declarations: [
    AppComponent,
    PatientListComponent,
    HomeComponent,
    PatientDetailComponent,
    PatientDetailListTransactionsComponent,
    PatientDetailListServicesComponent,
    PatientDetailListTypesComponent,
    AddPatientComponent,
    AddTransactionComponent,
    AddServiceComponent,
    ProductComponent,
    AddProductTypeComponent,
    DeleteProductTypeComponent,
    CartComponent,
    CartDetailComponent
  ],
  imports: [
    InputTextModule,
    CheckboxModule,
    FormsModule,
    TableModule,
    BrowserModule,
    ButtonModule,
    AccordionModule,
    TabMenuModule,
    MenuModule,
    BrowserAnimationsModule,
    HttpClientModule,
    RouterModule.forRoot([ 
      { path: 'patients', component: PatientListComponent },
      { path: 'patients/addpatient', component: AddPatientComponent},
      { path: 'patients/:id', component: PatientDetailComponent,
        children: [
          { path: 'addtransaction', component: AddTransactionComponent},
          { path: 'transactions', component: PatientDetailListTransactionsComponent},
          { path: 'addservice', component: AddServiceComponent},
          { path: 'services', component: PatientDetailListServicesComponent,
            children:[
              { path: ':id', component: PatientDetailListTypesComponent}
            ] 
          }
        ] },
      { path: 'products', component: ProductComponent},
      { path: 'products/addservicetype', component: AddProductTypeComponent},
      { path: 'products/:id', component: DeleteProductTypeComponent},
      { path: 'cart', component: CartComponent},
      { path: 'cart/:id', component: CartDetailComponent},
      { path: 'home', component: HomeComponent },
      { path: '**', redirectTo: 'home', pathMatch: 'full'}
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
