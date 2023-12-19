import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { BreadcrumbService } from 'src/app/services/breadcrumb.service';
import { BuisnessService } from 'src/app/services/Buisness.service';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';

@Component({
  selector: 'app-view-cards',
  templateUrl: './view-cards.component.html',
  styleUrls: ['./view-cards.component.scss']
})
export class ViewCardsComponent implements OnInit {
  constructor(public buisnessService: BuisnessService,
    public _wrapperSearchService: WrapperSearchService,
    public _wrapperFuncService: WrapperFuncService,
    public breadcrumbService: BreadcrumbService,
    public _acct: AccountService) {
    breadcrumbService.setItem([
      { label: 'דף הבית', routerLink: ['/']/*, icon: 'pi pi-home'*/},
      // { label: 'ברטרים', routerLink: ['/barter-List'] },
      { label: 'כרטיסים', routerLink: ['/barter-List/view-cards'] }
    ]);
  }

  ngOnInit() {
    this._wrapperSearchService.onListComponnent$.next(false);
  }


  openNewMessage(event,businessId){
    event.stopPropagation();
    this._wrapperFuncService.openNewMassage(businessId);
  }
}
