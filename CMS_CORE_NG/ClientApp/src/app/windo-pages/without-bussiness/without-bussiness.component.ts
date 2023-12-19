import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/services/account.service';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';

@Component({
  selector: 'app-without-bussiness',
  templateUrl: './without-bussiness.component.html',
  styleUrls: ['./without-bussiness.component.css']
})
export class WithoutBussinessComponent implements OnInit {

  constructor(public router : Router,
              public _wrapperFuncService: WrapperFuncService,
              public acct : AccountService) { }

  @Output() closed = new EventEmitter();
  ngOnInit(): void {
  }
  goToBussinessPage(){

    this.router.navigate(["/profile"])
    this.acct.flagBus = false

  }

  close(){
    this.closed.emit();
    this._wrapperFuncService.closeDialog();
  }
}
