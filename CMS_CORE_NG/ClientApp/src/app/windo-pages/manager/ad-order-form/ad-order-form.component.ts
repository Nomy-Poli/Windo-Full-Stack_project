import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, Inject, Input, OnInit, Optional, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AccountService } from 'src/app/services/account.service';
import { AdvertismentService, OrderServiceVM, OrderServiceWithAdDetailsVM, RequestOrderServiceVM } from 'src/app/services/advertisment.service';
import { BreadcrumbService } from 'src/app/services/breadcrumb.service';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';
import Swal from 'sweetalert2';
import {MessageService as ToastService} from 'primeng/api';


@Component({
  selector: 'app-ad-order-form',
  templateUrl: './ad-order-form.component.html',
  styleUrls: ['./ad-order-form.component.scss']
})
export class AdOrderFormComponent implements OnInit {

 
  constructor(public _wrapperSearchService: WrapperSearchService,
    private _httpClient: HttpClient,
    private _acct: AccountService,
    public _funcService: WrapperFuncService,
    public _wrapperFuncService:WrapperFuncService,
    public breadcrumbService: BreadcrumbService,
    private _advertismentService: AdvertismentService,
    public _toast: ToastService,
    @Optional() @Inject('API_BASE_URL') apiBaseUrl?: string,
  ) {
    // breadcrumbService.setItem([
    //   { label: 'דף הבית', routerLink: ['/']/*, icon: 'pi pi-home'*/ },
    //   { label: 'הזמנת פרסומת', routerLink: ['.'] }
    // ]);
    // this._acct.globalStateChanged.subscribe((state) => {
    //   this._wrapperSearchService.LoginStatus$.next(state.loggedInStatus);
    // });
    // this._wrapperSearchService.Username$ = this._acct.currentUserName;
    this.apiBaseUrl = apiBaseUrl ? apiBaseUrl : "";

  }

  MAX_SIZE: number = 307200;
  newDate = new Date();
  minDateValue = new Date();
  makat;
  orderId: number;
   requestId: number
   @Output() closed = new EventEmitter();
   apiBaseUrl: String;
  order: OrderServiceWithAdDetailsVM;
  statuses = [];
  hasImg = false;
  pictureFile = null;
  form: FormGroup = new FormGroup({
    Makat: new FormControl({value:'',disabled: true}),
    ContactName :new FormControl({value:'',disabled: true}),
    BusinessName :new FormControl({value:'',disabled: true}),
    Email :new FormControl({value:'',disabled: true}),
    Phone :new FormControl({value:'',disabled: true}),
    adFromDate :new FormControl(null),
    adTillDate :new FormControl(null),
    LinkToSite: new FormControl('', Validators.minLength(5)),
    StatusOrderId: new FormControl('',Validators.required)
  });
  get adFromDate(){ return this.form.get('adFromDate') as FormControl; }
  get adTillDate(){ return this.form.get('adTillDate') as FormControl; }

  ngOnInit(): void {
   
      if(this.requestId){
        this.getOrderObjectByRequest(this.requestId);
      }
      else if(this.orderId){
        this.getOrder(this.orderId);
      }
      else if(this.makat){
        
      }
      this.adFromDate.valueChanges.subscribe((values)=>{
        this.checkDate(values);
      });
      this.adTillDate.valueChanges.subscribe((values)=>{
        this.checkDate(values);
      });
     this.getStatuses();
    // this.getClientType();
  }
  
  checkDate(values){
    console.log(values);
    if(this.adFromDate.value && this.adTillDate.value){
      let objToSend = {adFromDate:this.adFromDate.value, adTillDate:this.adTillDate.value,OrderId: this.order.Id,Makat: this.order.Makat,ClientId: this.order.ClientId};
      this._advertismentService.checkIfAvalibleDate(objToSend).subscribe(res=>{
        if(res==true){
          this.adTillDate.setErrors({ notAvalible:true });
        }
        else{
          this.adTillDate.setErrors(null);
        }
      })
    }
  }
  getOrderObjectByRequest(requestId){
    this._advertismentService.newOrderServiceByRequest(requestId).subscribe(res=>{
      this.order = res;
      this.setFormValues(this.order);
    });
  }
  getOrder(orderId){
    this._advertismentService.getOrderServiceById(orderId).subscribe((res: OrderServiceWithAdDetailsVM)=>{
      this.order = res;
      if(this.order.AdvertismentServiceOrder.PicGuid && this.order.AdvertismentServiceOrder.PicGuid!="00000000-0000-0000-0000-000000000000"){
        this.hasImg = true;
        setTimeout(() => {
          $('#img').attr('src', `../../../../../assets/advertisments/${this.order.Makat}/${this.order.AdvertismentServiceOrder.PicGuid}.jpg`);
        }, 1000);
      }
      this.setFormValues(this.order)
    });
  }
 

  getStatuses(){
    this._advertismentService.getOrderStatuses().subscribe(res=>{
      this.statuses = res;
    })
  }
  setFormValues(order: OrderServiceWithAdDetailsVM){
    this.form.patchValue({
      Makat: order.Makat,
      ContactName: order.Client.ContactName,
      BusinessName: order.Client.BusinessName,
      Email: order.Client.Email,
      Phone: order.Client.Phone,
      adFromDate: order.AdvertismentServiceOrder.adFromDate? new Date(order.AdvertismentServiceOrder.adFromDate): null ,
      adTillDate: order.AdvertismentServiceOrder.adTillDate? new Date(order.AdvertismentServiceOrder.adTillDate): null,
      LinkToSite: order.AdvertismentServiceOrder.LinkToSite,
      StatusOrderId: order.StatusOrderId,  
    });
    // this.form = new FormGroup({


      
      // ContactName: new FormControl('',[Validators.required,Validators.maxLength(50)]),
      // BusinessName: new FormControl('',[Validators.required,Validators.maxLength(50)]),
      // Email: new FormControl('',[Validators.pattern('[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$'),Validators.required]),
      // LinkForSite: new FormControl(''),
      // Phone: new FormControl('',[Validators.required,Validators.pattern('^[0-9]{2,3}[-. ]?[0-9]{7}$')]),
      // Phone2: new FormControl('',[Validators.pattern('^[0-9]{2,3}[-. ]?[0-9]{7}$')]),
      // ReturningCustomer: new FormControl(''),
      // ServiceDate: new FormControl(''),
      // Text:new FormControl('')
    // });
  }


  checkIfAvalibleDates(fromDate, tillDate){
    
  }
  filesDropped(event) { 
      if (event[0].file.size < this.MAX_SIZE){
        this.hasImg = true;
        console.log(event);
        let reader = new FileReader();
        this.pictureFile = event[0].file;
        reader.readAsDataURL(event[0].file); 
        reader.onload = () => {
          $('#img').attr('src', reader.result as string);
        };   
   } 
   else{
    this._toast.add({severity:'error', detail: 'יש להעלות קובץ ששוקל עד 300KB'})
   }

  }
  //#region upload image
  onWorkFileChanged(event) {
    if (event.target.files && event.target.files[0]) {
      if (event.target.files[0].size < this.MAX_SIZE) {
        this.hasImg = true;
        let reader = new FileReader();
        this.pictureFile = event.target.files[0];
        reader.readAsDataURL(event.target.files[0]);
        reader.onload = () => {
          $('#img').attr('src', reader.result as string);
        };
      }
      else{
        this._toast.add({severity:'error', detail: 'יש להעלות קובץ ששוקל עד 300KB'})
      }
    }
    
  }
  triggerWorkInput() {
    $('#workpicfile').trigger('click');
  }
  RemoveImage() {
    this.hasImg = false;
    $("#img").attr('src', " ");
  }
  //#endregion
  submitForm() {
    if (this.form.valid) {
      let order: OrderServiceWithAdDetailsVM = {
        Id: this.order.Id,
        Makat: this.order.Makat,
        Price: this.form.value.Price,
        ClientId: this.order.ClientId,
        StatusOrderId: this.form.value.StatusOrderId,
        AdvertismentServiceOrder: {
          Id: this.order.AdvertismentServiceOrder.Id,
          OrderServiceId: this.order.Id,
          PicGuid: this.order.AdvertismentServiceOrder.PicGuid,
          LinkToSite: this.form.value.LinkToSite,
          adFromDate: this.form.value.adFromDate,
          adTillDate: this.form.value.adTillDate,
          Makat: this.order.Makat,
        }

      };
        const formD = new FormData();
        formD.append('model', JSON.stringify(order));
        formD.append('picture',this.pictureFile);
        return this._httpClient.post(this.apiBaseUrl + `/api/Advertisment/UpdateOrderAdvertisment`, formD,
        {
          headers: { 'Content-Type': [] }, reportProgress: true
        }).subscribe((res: number) => {
          console.log(res);
          Swal.fire('ההזמנה נקלטה במערכת').then((val)=>{
            this.closed.emit(order);
            this._funcService.closeDialog();
          });
        });
     
      // this._advertismentService.createOrderAdvertisment(order).subscribe(res => {
      //   console.log(res);
      //   Swal.fire('ההזמנה נקלטה במערכת');
      // });
    }
  }

  createOrder() {
    let order: OrderServiceWithAdDetailsVM = {
      ClientId: 4,
      Id: 0,
      Makat: 222,
      Price: 900.0,
      StatusOrderId: 2,
      AdvertismentServiceOrder: {
        Id: 0,
        Makat: 222,
        OrderServiceId: 0,
        LinkToSite:'https://mail.google.com/mail/u/0/#inbox/FMfcgzGpFzrjXZJNkGXfMsnHWSqLbJvg',
        // PicGuid: 'c010cf75-e086-420e-9de5-2dc51964dda1',
      }
    }
    this._advertismentService.createOrderAdvertisment(order).subscribe(res => {
      alert('הזמנה חדשה נקלטה במערכת')
    })
  }
  close(){
    this._wrapperFuncService.closeDialog();
  }
  saveOrder(event) {
    if (event && event.target.files[0]) {
      const formD = new FormData();
      let order: OrderServiceWithAdDetailsVM = {
        ClientId: this.order.ClientId,
        Id: 0,
        Makat: this.order.Makat,
        Price: 300.0,
        StatusOrderId: 1,
        AdvertismentServiceOrder: {
          Id: 0,
          Makat: this.order.Makat,
          OrderServiceId: 0,
          LinkToSite: 'https://mail.google.com/mail/u/0/#inbox/FMfcgzGpFzrjXZJNkGXfMsnHWSqLbJvg',
          PicGuid: 'c010cf75-e086-420e-9de5-2dc51964dda1',
        }
      }
      formD.append('model', JSON.stringify(order));
      formD.append('picture', event.target.files[0]);
      return this._httpClient.post(this.apiBaseUrl + `/api/Advertisment/UpdateOrderAdvertisment`, formD,
        {
          headers: { 'Content-Type': [] }, reportProgress: true
        }).subscribe((res: number) => {
          console.log(res);
          alert('ההזמנה נשמרה!!!!!')
        });
    }
  }
 
}
