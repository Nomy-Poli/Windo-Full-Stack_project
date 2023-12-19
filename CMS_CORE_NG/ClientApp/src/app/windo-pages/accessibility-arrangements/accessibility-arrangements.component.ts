import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { BreadcrumbService } from 'src/app/services/breadcrumb.service';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';

@Component({
  selector: 'app-accessibility-arrangements',
  templateUrl: './accessibility-arrangements.component.html',
  styleUrls: ['./accessibility-arrangements.component.css']
})
export class AccessibilityArrangementsComponent implements OnInit {

  constructor(public breadcrumbService: BreadcrumbService,
    public _wrapperFuncService: WrapperFuncService,
              public _wrapperSearchService: WrapperSearchService,
              public acct: AccountService,
    ) {
        breadcrumbService.setItem([
            { label: 'דף הבית', routerLink: ['/']/*, icon: 'pi pi-home'*/ },
            { label: 'נגישות', routerLink: ['/AccessibilityArrangements'] }
        ]);
    }  

  ngOnInit(): void {
    this.acct.globalStateChanged.subscribe((state) => {
      this._wrapperSearchService.LoginStatus$.next(state.loggedInStatus);
    });
    this._wrapperSearchService.Username$ = this.acct.currentUserName;
    this._wrapperSearchService.HomePage$.next(false);
  }
}
