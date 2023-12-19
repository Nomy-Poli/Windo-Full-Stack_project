import { Component, OnInit } from '@angular/core';
import { AccountService } from '../services/account.service';
import { BreadcrumbService } from '../services/breadcrumb.service';
import { WrapperFuncService } from '../services/wrapper-func.service';
import { WrapperSearchService } from '../services/wrapper-search.service';

@Component({
    selector: 'app-terms',
    templateUrl: './terms.component.html',
    styleUrls: ['./terms.component.css']
})
export class TermsComponent implements OnInit {
    constructor(public _wrapperFuncService: WrapperFuncService,
              public _wrapperSearchService: WrapperSearchService,
              public acct: AccountService,
              public breadcrumbService: BreadcrumbService) { 
                   breadcrumbService.setItem([
                { label: 'דף הבית', routerLink: ['/']/*, icon: 'pi pi-home'*/},
                { label: 'תנאי השימוש', routerLink: ['/terms'] }
            ]);}
    ngOnInit(): void {
        this.acct.globalStateChanged.subscribe((state) => {
          this._wrapperSearchService.LoginStatus$.next(state.loggedInStatus);
        });
    window.scroll(0,0)
    this._wrapperSearchService.Username$ = this.acct.currentUserName;
    this._wrapperSearchService.HomePage$.next(false);
    this._wrapperFuncService.closeDialog()
}

}
