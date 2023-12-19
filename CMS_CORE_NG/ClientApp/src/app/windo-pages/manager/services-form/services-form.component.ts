
import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, Inject, OnInit, Optional, Output } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { AdvertismentService, CatalogServiceVM, ServiceType, ServiceTypeVM } from 'src/app/services/advertisment.service';
import { BreadcrumbService } from 'src/app/services/breadcrumb.service';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';
import { MessageService as ToastService } from 'primeng/api';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-services-form',
  templateUrl: './services-form.component.html',
  styleUrls: ['./services-form.component.css']
})
export class ServicesFormComponent implements OnInit {
  catalogServices: CatalogServiceVM[]=[];
  service : CatalogServiceVM = {} as CatalogServiceVM;
  serviceType:ServiceTypeVM[]=[] ;
  // newService:CatalogServiceVM={};
  constructor(
    public _wrapperSearchService: WrapperSearchService,
        private _httpClient: HttpClient,
        private _acct: AccountService,
        public _funcService: WrapperFuncService,
        private _advertismentService: AdvertismentService,
        public _toast: ToastService,
        @Optional() @Inject('API_BASE_URL') apiBaseUrl?: string
  ) { 
    // this.apiBaseUrl = apiBaseUrl ? apiBaseUrl : '';
  }
  @Output() closed = new EventEmitter();
  
  form: FormGroup = new FormGroup({
    Makat: new FormControl({value:'',disabled: true}),
    typeOfService: new FormControl(),
    name: new FormControl(),
    description: new FormControl(),
});

  ngOnInit(): void {
    this._advertismentService.getWindoSiteServices().subscribe(res=>{
      this.serviceType = res;
      console.log("serviceType",this.serviceType);
      
    });
    if(this.service)
    {
      console.log("GG",this.service);
        this.form.patchValue({
          Makat: this.service.Makat,
          typeOfService: this.service.Id,
          name:this.service.ServiceType.Name,
          description:this.service.ServiceType.Description
      });
    }
    else{
      // this.catalogServices_Filter=this.catalogServices.filter(x=>x.ServiceTypeId ==x.ServiceType.Id);
      this.service = {} as CatalogServiceVM;
      this._advertismentService.getCatalogServices().subscribe(res=>{
        this.catalogServices = res;
        let lest=res.length;
        this.form.patchValue({
          Makat: res[lest-1].Makat+1
      });
    });

    }  
 }


 submitForm(){
  console.log("service=",this.service);
  if(this.form.valid){
      let formValue = this.form.value;
      this.service.Makat = formValue.Makat;
      this.service.Description=formValue.typeOfService;
      this.service.ServiceType.Name=formValue.name;
      this.service.ServiceType.Description=formValue.description;
      console.log("...",this.service);
      
      this._advertismentService.updateCatalogService(this.service);
        Swal.fire('פרטי המוצר נשמרו במערכת').then((val)=>{
          this.closed.emit(this.service);
          this._funcService.closeDialog();
        });
  }
}
  close(){
    this._funcService.closeDialog();
  }
 
}
