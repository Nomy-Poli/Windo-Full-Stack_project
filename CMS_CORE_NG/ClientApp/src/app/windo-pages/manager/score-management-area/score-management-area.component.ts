import { Component, OnInit, ViewChild } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { AdvertismentService } from 'src/app/services/advertisment.service';
import { BreadcrumbService } from 'src/app/services/breadcrumb.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';
import { OrderServeiceListComponent } from '../order-serveice-list/order-serveice-list.component';

@Component({
  selector: 'app-score-management-area',
  templateUrl: './score-management-area.component.html',
  styleUrls: ['./score-management-area.component.css']
})
export class ScoreManagementAreaComponent implements OnInit {

  constructor(public _wrapperSearchService: WrapperSearchService,
    private _acct: AccountService,
    public breadcrumbService: BreadcrumbService,
    private _advertismentService: AdvertismentService
  ){
    breadcrumbService.setItem([
      { label: 'דף הבית', routerLink: ['/']/*, icon: 'pi pi-home'*/ },
      { label: 'אזור מנהל', routerLink: ['/manager'] },
      { label: 'ניהול ניקוד', routerLink: ['.'] }
    ]);

    this._acct.globalStateChanged.subscribe((state) => {
      this._wrapperSearchService.LoginStatus$.next(state.loggedInStatus);
    });
    this._wrapperSearchService.Username$ = this._acct.currentUserName;
   }
   tabIndex = 0;

  ngOnInit(): void {
  }

}
