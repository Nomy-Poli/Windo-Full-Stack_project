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
  selector: 'app-windo-site-services-form',
  templateUrl: './windo-site-services-form.component.html',
  styleUrls: ['./windo-site-services-form.component.css']
})
export class WindoSiteServicesFormComponent implements OnInit {
  siteServices:ServiceTypeVM[]=[];
  service:ServiceTypeVM = {} as ServiceTypeVM;
  constructor(
    public _wrapperSearchService: WrapperSearchService,
    public _funcService: WrapperFuncService,
    private _advertismentService: AdvertismentService,
    public _toast: ToastService
  ) { }
  @Output() closed = new EventEmitter();

  form: FormGroup = new FormGroup({
    id: new FormControl(),
    name: new FormControl(),
    description: new FormControl(),
});
  ngOnInit(): void {
    if(this.service){
      this._advertismentService.getWindoSiteServices().subscribe(res=>{
        this.siteServices = res;
    });
      console.log("GG",this.service);
      this.form.patchValue({
      id:this.service.Id,
      name:this.service.Name,
      description:this.service.Description
    });
    }
    else{
      this.service = {} as ServiceTypeVM;
      this._advertismentService.getWindoSiteServices().subscribe(res=>{
        this.siteServices = res;
        let last=res.length;
        this.form.patchValue({
          id: last+1
      });
    });
    }
  }
  submitForm(){
    console.log("service=",this.service);
    if(this.form.valid){
        let formValue = this.form.value;
        this.service.Id = formValue.id;
        this.service.Name=formValue.name;
        this.service.Description=formValue.description;
        console.log("...",this.service);
        this._advertismentService.updateWindoSiteServices(this.service)
        .subscribe((res: boolean) => {
          console.log(res);
          Swal.fire('פרטי הבאנר נשמרו במערכת').then((_val)=>{
            this.closed.emit(this.service);
            this._funcService.closeDialog();
            
          });
        });
    }
  }
    close(){
      this._funcService.closeDialog();
    }
   

}
