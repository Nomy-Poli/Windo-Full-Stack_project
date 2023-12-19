import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { BreadcrumbService } from 'src/app/services/breadcrumb.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';

@Component({
  selector: 'app-vision',
  templateUrl: './vision.component.html',
  styleUrls: ['./vision.component.scss']
})
export class VisionComponent implements OnInit {

  constructor(public breadcrumbService: BreadcrumbService,
    private _acct:AccountService,
    private _wrapSearchService: WrapperSearchService
 
    ) {
        breadcrumbService.setItem([
            { label: 'דף הבית', routerLink: ['/']/*, icon: 'pi pi-home'*/},
            { label: 'החזון', routerLink: ['/vision'] }
        ]);
        _acct.globalStateChanged.subscribe((state) => {
          _wrapSearchService.LoginStatus$.next(state.loggedInStatus);
        });
        _wrapSearchService.Username$ = this._acct.currentUserName;
        _wrapSearchService.HomePage$.next(false);

    }  

  ngOnInit(): void {
    window.scroll(0,0)
  }

}
