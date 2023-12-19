import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import Swal from 'sweetalert2';
import { AccountService } from '../services/account.service';
import { BreadcrumbService } from '../services/breadcrumb.service';
import { ContactModel, ContactService } from '../services/contact.service';
import { MessageService, MessageVM, SupportMessageVM } from '../services/Message.service';
import { WrapperFuncService } from '../services/wrapper-func.service';
import { WrapperSearchService } from '../services/wrapper-search.service';
import { style } from '@angular/animations';

@Component({
  selector: 'app-contact-us',
  templateUrl: './contact-us.component.html',
  styleUrls: ['./contact-us.component.css']
})
export class ContactUsComponent implements OnInit {
  contactForm: FormGroup;
  model: ContactModel = {};
  message: string;
  isLoading: boolean = false;
  idSend:boolean=false
  ifFromHelp:boolean=false
  ifFromOrder:boolean=false
  isTabPanelActive: boolean = false;
  UserEmail:string
  public toasterService: ToastrService
  constructor(private fb: FormBuilder
    , public _contactService: ContactService
    , public _wrapperSearchService: WrapperSearchService
    , public _wrapFuncService: WrapperFuncService
    , private _messageService: MessageService
    , public acct: AccountService
    , public breadcrumbService: BreadcrumbService
    ,private _router: Router
    ) {
        breadcrumbService.setItem([
            { label: 'דף הבית', routerLink: ['/']/*, icon: 'pi pi-home'*/ },
            { label: 'יצירת קשר', routerLink: ['/contact-us'] }
        ]);
    }
    activeTab: number = 1;
  ngOnInit(): void {
    this.acct.globalStateChanged.subscribe((state) => {
      this._wrapperSearchService.LoginStatus$.next(state.loggedInStatus);
    });
    this._wrapperSearchService.Username$ = this.acct.currentUserName;
    this._wrapperSearchService.HomePage$.next(false);
    this.setBackgroundImage();
    this.UserEmail = this.acct.Email.value;
    this.contactForm = this.fb.group({
      // buisnessEmailAddress: new FormControl('', [Validators.pattern('[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$')/*Validators.required, Validators.maxLength(20), Validators.minLength(10)*/]),
      name: ['', [Validators.pattern('^[\u0590-\u05FF]+(?: [\u0590-\u05FF]+)*$'),Validators.required]],
      // name: ['',[Validators.pattern('^[\u0590-\u05FF\s]+$'),Validators.required]],  
      email: [this.UserEmail, [Validators.pattern('[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$'), Validators.required]],
      phoneNumber: ['', [Validators.pattern('^[0-9]{2,3}[-. ]?[0-9]{7}$')]],
      message: ["", Validators.required]
    });
    // this._wrapperSearchService.getCurrentBusiness().subscribe(res=>{
    //   if(!res){
    //     this._wrapFuncService.openRegitserNoBusinessPopup().subscribe(res=>{
    //       this._router.navigate(['/']);
    //     })
    //   }
    // })
  }

  onSubmit() {
    this.isLoading = true;
    //this.model.email = this.contactForm.get("email").value ? this.contactForm.get("email").value : null;
    //this.model.phoneNumber = this.contactForm.get("phoneNumber").value ? this.contactForm.get("phoneNumber").value : null;
    //this.model.name = this.contactForm.get("name").value ? this.contactForm.get("name").value : null;
    //this.model.message = this.contactForm.get("message").value ? this.contactForm.get("message").value : null;
    this.model.email = this.contactForm.get("email").value;
    this.model.phoneNumber = this.contactForm.get("phoneNumber").value;
    this.model.name = this.contactForm.get("name").value;
    this.model.message = this.contactForm.get("message").value;

    if (this.model.email && this.model.message) {
      this._contactService.contact(this.model).subscribe(res => {
        // Swal.fire(
        //   "ההודעה נשלחה!", " ", "success"
        // )
        this.isLoading = false;
        this.idSend = true
      })
    }
    else {
      // Swal.fire({
      //   icon: 'error',
      //   title: 'הייתה בעיה בשליחת ההודעה',
      //   html:
      //     'חובה להכניס מייל והודעה',
      //   showClass: {
      //     popup:
      //       'אנא בדוק שוב'
      //   }
      // })
      this.message = "חובה להכניס מייל והודעה";
      this.isLoading = false;
    }
  }

  setBackgroundImage() {
    $('body').css({
      'background-image': 'none',
      'background-repeat': 'no-repeat',
      'background-size': 'cover'
    });
  }

  sendMessage(tabIndex) {
  
    if (this.contactForm.valid) {
      let newMessage: SupportMessageVM = {
        MessageText: this.contactForm.get('message').value,
        Subject: tabIndex == 1 ? 'עזרה טכנית' : 'פניה בנושא חופשי',
        EmailFrom: this.contactForm.get('email').value,
        UserName: this.contactForm.get('name').value
      };
      this.isLoading = true;
      this._messageService.createSupportMessage(newMessage).subscribe(res => {
        this.isLoading = false;
        this.idSend = true;
      }, err => {
        this.isLoading = false;
        Swal.fire({
          icon: 'error',
          title: 'הייתה בעיה בשליחת ההודעה',
          html:
            'מצטערים יתכן ומייל זה איננו מחובר למערכת',
          showClass: {
            popup:
              'אנא בדוק שוב'
          }
        })
      });

    }

  }
  onTabViewLoad(event) {
    const firstTab = document.querySelector('.p-tabview-nav a:first-child');
    if (firstTab) {
      firstTab.classList.add('focused');
    }
  }
}
