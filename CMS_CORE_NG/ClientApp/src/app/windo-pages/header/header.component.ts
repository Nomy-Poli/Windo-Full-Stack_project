import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { SocialAuthService } from 'angularx-social-login';
import { Observable } from 'rxjs';
import { AccountService } from 'src/app/services/account.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { BuisnessService } from 'src/app/services/Buisness.service';
import { MessageService } from 'src/app/services/Message.service';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  //בדיקה האם נכנס דרך ספק חיצוני או פנימי
  isExternalAuth: boolean;
  Menu: boolean;
  megamenue: any = false;
  userName: Observable<any>;
  breadcrumbItems = [];
  newMessagesCount = 0;
  aboutDropdownOpen(){
    return $('#ItabouatListems').hasClass('show');
  }
  servicDropdownOpen(){
    return $('#servicListItems').hasClass('show');
  }
  scrollToTop(){
    
    const navbar = document.querySelector('.flex header-container') as HTMLElement;
  

  if (navbar !== null) {
    const navbarHeight = navbar.offsetHeight || 0;
    

    window.scroll({ top: window.pageYOffset + navbarHeight , left: 0, behavior: 'smooth' });
  }
  }
  constructor(private acct: AccountService, private router: Router
    , private _socialAuthService: SocialAuthService
    , private _authService: AuthenticationService
    , public _wrapperSearchService: WrapperSearchService
    , public _wrapperFuncService: WrapperFuncService
    , public _acct: AccountService
    , public _messageService: MessageService
    , public _router: Router
    , private _businessService: BuisnessService) { }
  ngOnInit() {
    
    this.Menu = false;
    // this.megamenue = new Observable<string>(observer => {
    //   setInterval(() => observer.next(localStorage.getItem('OpenMegaMenue')));
    // });
    // this.megamenue = localStorage.getItem('OpenMegaMenue');
    // this._wrapperSearchService.Username$ = this._wrapperSearchService.Username$.split('@');
    
    

    this._wrapperFuncService.closeDialog();
    if (this.acct.FirstName.value != null) {
      this.userName = this.acct.FirstName;
    }
    else {
      let name;
      this._wrapperSearchService.Username$.subscribe(n => {name = n});
      this.userName = name.split('@')[0].split('[name.length]');
    }
    this._router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.Menu = false;
        // this._wrapperFuncService.closeDialog();
      }
    });
    this._acct.currentBusiness.subscribe(business => {
      if (business) {
        this._messageService.getNewMessageCount(business.id, business.userId).subscribe(res => {
          this._wrapperSearchService.newMessages$.next(res);
        });
      }
      else{
        this._wrapperSearchService.newMessages$.next(0);
      }
    });
    this.megamenue
  }
  myFunc(){ 
    if(!this.router.url.includes('returnUrl')) 
      this._wrapperFuncService.closeDialog();
  }
  loginClick(event:Event){
    this._wrapperFuncService.openLoginDialog()
    this.stopProg(event);
  }
  registerClick(event:Event){
    this._wrapperFuncService.openDialogRegister()
    this.stopProg(event);
  }
  stopProg(event){
    event.stopPropagation();
  }
  onLogout() {
    this.acct.logout().subscribe((result) => {
      console.log('התנתקת בהצלחה');
      this._wrapperSearchService.newMessages$.next(0);
    });
    if (this.isExternalAuth)
      this._authService.signOutExternal();

    this.router.navigate(['/home']);
  }
  //   openDialog() { 
  //     this.modalService.open(RegisterComponent, { centered: true, size: 'sm' });
  // }
  // openDialogHesmburger(HemburgerMenu){
  // this.modalService.open(HemburgerMenu,{ size:'lg' } )
  // }
  closeMenu() {
    this.Menu = false;
  }
  openMenu() {
    this.Menu = true;
  }
  OpenBusiness() {
    if ((this._wrapperSearchService.LoginStatus$.value == false)) {
      this._wrapperFuncService.openLoginDialog();
    }
    else
      this.router.navigate(['/profile']);
  }
}
