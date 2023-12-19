import { Component, Input, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';

@Component({
  selector: 'app-contact-details',
  templateUrl: './contact-details.component.html',
  styleUrls: ['./contact-details.component.css']
})
export class ContactDetailsComponent implements OnInit {

  constructor(private _funcService: WrapperFuncService,
    public _wrapperSearchService: WrapperSearchService,
    public _acct: AccountService) { }

  @Input() details
  ngOnInit(): void {
  }

  close(){
    this._funcService.closeDialog();
  }
  sendMessage(){
    this.close();
    this._funcService.openNewMassage(this.details.Business.id);
  }
}
