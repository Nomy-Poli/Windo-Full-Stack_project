import { Component, HostListener, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { BreadcrumbService } from 'src/app/services/breadcrumb.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';

@Component({
  selector: 'app-hiring',
  templateUrl: './hiring.component.html',
  styleUrls: ['./hiring.component.scss']
})
export class HiringComponent implements OnInit {

  constructor(public breadcrumbService: BreadcrumbService,
    private _acct:AccountService,
    private _wrapSearchService: WrapperSearchService
 
    ) {
        breadcrumbService.setItem([
            { label: 'דף הבית', routerLink: ['/']/*, icon: 'pi pi-home'*/},
            { label: 'הבשורה', routerLink: ['/tiding'] }
        ]);
        _acct.globalStateChanged.subscribe((state) => {
          _wrapSearchService.LoginStatus$.next(state.loggedInStatus);
        });
        _wrapSearchService.Username$ = this._acct.currentUserName;
        _wrapSearchService.HomePage$.next(false);
    }  

  ngOnInit(): void {
    window.scroll(0,0)
  //  this.sendEmail();
  }

  // sendEmail(email){
  //   console.log("email",email);
    
  //   location.href = "mailto:"+email;
  // }
  // @HostListener('document:click', ['$event'])
  // public onClick(event: any): void {
  //   const mailtoLink = 'mailto:rci@temech.org?subject=הגעתי מאתר תמך ואשמח למידע על .... ';
  //   window.location.href = mailtoLink;
  //   console.log(mailtoLink);
  // }
  public sendEmail(): void {
const recipientEmail = 'rci@temech.org';
const subject = 'הגעתי מאתר תמך ואשמח למידע על ...';
const body = 'תוכן ההודעה שלך כאן';

const gmailLink = `https://mail.google.com/mail/?view=cm&fs=1&to=${recipientEmail}&su=${encodeURIComponent(subject)}&body=${encodeURIComponent(body)}`;
window.open(gmailLink, '_blank');

  }
}
