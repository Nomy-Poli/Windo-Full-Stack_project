import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from 'src/app/services/account.service';
import { AdvertismentService, ClientVM } from 'src/app/services/advertisment.service';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';

@Component({
  selector: 'app-client-form',
  templateUrl: './client-form.component.html',
  styleUrls: ['./client-form.component.scss']
})
export class ClientFormComponent implements OnInit {

  constructor(public _wrapperSearchService: WrapperSearchService,
    public _wrapperFuncService:WrapperFuncService,
    private _acct: AccountService,
    private _funcService :WrapperFuncService,
    private _advertismentService: AdvertismentService) { }

  form: FormGroup;
  clientId: number;
  requestId: number;
  @Output() closed = new EventEmitter();
  ngOnInit(): void {
    this.form = new FormGroup({
      ContactName: new FormControl('',[Validators.required,Validators.maxLength(50)]),
      BusinessName: new FormControl('',[Validators.required,Validators.maxLength(50)]),
      Description: new FormControl('',[Validators.required,Validators.maxLength(50)]),
      Email: new FormControl('',[Validators.pattern('[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$'),Validators.required]),
      Phone: new FormControl('',[Validators.required,Validators.pattern('^[0-9]{2,3}[-. ]?[0-9]{7}$')]),
    });
    if(this.clientId){
      this._advertismentService.getClient(this.clientId).subscribe(res=>{
        this.patchValue(res);
      })
    }
    else if(this.requestId){
      this._advertismentService.newClientByRequest(this.requestId).subscribe(res=>{
        this.patchValue(res);
      })
    }
  }

  patchValue(client: ClientVM){
    this.form.patchValue({
      ContactName: client.ContactName,
      BusinessName: client.BusinessName,
      Description: client.Description,
      Phone: client.Phone,
      Email: client.Email
    });
  }
  submitForm(){
    if(this.form.valid){
      let client: ClientVM = this.form.value;
      if(this.clientId){
        client.Id = this.clientId;
        this._advertismentService.updateClient(client).subscribe(res=>{
          console.log(res);
          this.closed.emit(client);
          this._wrapperFuncService.closeDialog();

        });
      }
      else{
        client.Id = 0;
        this._advertismentService.createClient(client).subscribe(res=>{
          console.log(res);
          this.closed.emit(client);
          this._wrapperFuncService.closeDialog();
        });
      }
    }
  }
  close(){
    this._funcService.closeDialog();
  }
}
