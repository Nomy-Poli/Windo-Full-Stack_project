import { Component, HostListener, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { AccountService } from 'src/app/services/account.service';
import { BreadcrumbService } from 'src/app/services/breadcrumb.service';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';

@Component({
  selector: 'app-independent',
  templateUrl: './independent.component.html',
  styleUrls: ['./independent.component.scss']
})
export class IndependentComponent implements OnInit {

  
  constructor(public breadcrumbService: BreadcrumbService
   , public _wrapperSearchService: WrapperSearchService,
   private _acct:AccountService,
   public _funcService:WrapperFuncService,
   private _wrapSearchService: WrapperSearchService
   , public _router: Router
    ) {
        breadcrumbService.setItem([
            { label: 'דף הבית', routerLink: ['/']/*, icon: 'pi pi-home'*/},
            { label: 'הפרקטיקה', routerLink: ['/practice'] }
        ]);
        _acct.globalStateChanged.subscribe((state) => {
          _wrapSearchService.LoginStatus$.next(state.loggedInStatus);
        });
        _wrapSearchService.Username$ = this._acct.currentUserName;
        _wrapSearchService.HomePage$.next(false);
        
    }




    
  ngOnInit(): void {
    // const circleElement = document.querySelector('.filled-circle') as HTMLElement;
    // circleElement.style.backgroundColor = 'black';
    window.scroll(0,0)
  }
  // sendEmail(email){
  //   console.log("email",email);
    
  //   location.href = "mailto:"+email;
  // }

  // @HostListener('document:click', ['$event'])
  // public onClick(event: any): void {
  //   const mailtoLink = 'mailto:BH@temech.org?subject=הגעתי מאתר תמך ואשמח למידע על .... ';
  //   window.location.href = mailtoLink;
  //   console.log(mailtoLink);
  // }
  public sendEmail(): void {
    const mailtoLink = 'mailto:BH@temech.org?subject=הגעתי מאתר תמך ואשמח למידע על .... ';
    window.location.href = mailtoLink;
    console.log(mailtoLink);
  }
  public openEmailInGmail(): void {
    const gmailUrl = 'https://mail.google.com/mail/u/0/?view=cm&fs=1&to=BH@temech.org&su=הגעתי מאתר תמך ואשמח למידע על ...';
    window.open(gmailUrl, '_blank');
}

}
