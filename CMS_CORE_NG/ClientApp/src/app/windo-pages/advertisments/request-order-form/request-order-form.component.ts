import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AccountService } from 'src/app/services/account.service';
import { AdvertismentService, RequestOrderServiceVM } from 'src/app/services/advertisment.service';
import { BreadcrumbService } from 'src/app/services/breadcrumb.service';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';

@Component({
  selector: 'app-request-order-form',
  templateUrl: './request-order-form.component.html',
  styleUrls: ['./request-order-form.component.css']
})
export class RequestOrderFormComponent implements OnInit {
  

  constructor(public _wrapperSearchService: WrapperSearchService,
    public _wrapperFuncService:WrapperFuncService,
    private _acct: AccountService,
    private _advertismentService: AdvertismentService,
    private _activatedRoute:ActivatedRoute
  ) {
    // breadcrumbService.setItem([
    //   { label: 'דף הבית', routerLink: ['/']/*, icon: 'pi pi-home'*/ },
    //   { label: 'הזמנת פרסומת', routerLink: ['.'] }
    // ]);
    this._acct.globalStateChanged.subscribe((state) => {
      this._wrapperSearchService.LoginStatus$.next(state.loggedInStatus);
    });
    this._wrapperSearchService.Username$ = this._acct.currentUserName;
    this.makat = parseInt(_activatedRoute.snapshot.params['makat']);
  }
  makat;
  form: FormGroup;
  clientTypes = [];
  bannersList = [];
  ngOnInit(): void {
  
    this.form = new FormGroup({
      ContactName: new FormControl('',[Validators.required,Validators.maxLength(50)]),
      BusinessName: new FormControl('',[Validators.required,Validators.maxLength(50)]),
      Email: new FormControl('',[Validators.pattern('[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$'),Validators.required]),
      LinkForSite: new FormControl(''),
      Phone: new FormControl('',[Validators.required,Validators.pattern('^[0-9]{2,3}[-. ]?[0-9]{7}$')]),
      Phone2: new FormControl('',[Validators.pattern('^[0-9]{2,3}[-. ]?[0-9]{7}$')]),
      ReturningCustomer: new FormControl(''),
      ServiceDate: new FormControl(''),
      Text:new FormControl(''),
      Makat: new FormControl(this.makat)
    })
    this.getClientType();
    this.getBanners();
  }
  getBanners() {
    this._advertismentService.getBanners().subscribe(res=>{
      this.bannersList = res;
    })
  }

  getClientType(){
    this._advertismentService.getClientTypes().subscribe(res=>{
      this.clientTypes = res;
    })
  }

  submitForm(){
    console.log('from='+this.form.value);
    
    if(this.form.valid){
      let request:RequestOrderServiceVM = {
        Id:0,
        Makat: this.form.value.Makat? parseInt(this.form.value.Makat): 111,
        RequestStatus: 0,
        BusinessName: this.form.value.BusinessName,
        ContactName: this.form.value.ContactName,
        Email: this.form.value.Email,
        Phone: this.form.value.Phone,
        Phone2: this.form.value.Phone2,
        LinkForSite: this.form.value.LinkForSite,
        ReturningCustomer: this.form.value.ReturningCustomer? true: false,
        ServiceDate: this.form.value.ServiceDate? new Date(this.form.value.ServiceDate): null,
        Text:this.form.value.Text
        };
      this._advertismentService.createRequsetOrderService(request).subscribe(res=>{
        console.log(res);
        this._wrapperFuncService.closeDialog();
      });
    }
  }

}
