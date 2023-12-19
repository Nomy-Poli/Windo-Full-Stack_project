import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from 'src/app/services/account.service';
import { BreadcrumbService } from 'src/app/services/breadcrumb.service';
import { BuisnessService, BusinessNamesPicUserIdVM } from 'src/app/services/Buisness.service';
//import { MessageVM, MessageService, EmailTab } from 'src/app/services/Message.service';
import { MessageVM, MessageService, EmailTab } from 'src/app/services/Message.service';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-message-card',
  templateUrl: './message-card.component.html',
  styleUrls: ['./message-card.component.scss'],
})
export class MessageCardComponent implements OnInit {

  constructor(private _messageService: MessageService,
    public _acct: AccountService,
    public _funcService: WrapperFuncService,
    private _ActivatedRoute: ActivatedRoute,
    private _router: Router,
    private _buisnessService: BuisnessService,
    private _wrapSearchService: WrapperSearchService,
    public breadcrumbService: BreadcrumbService,
    private _businessService: BuisnessService) {

    this._acct.globalStateChanged.subscribe((state) => {
      this._wrapSearchService.LoginStatus$.next(state.loggedInStatus);
    });
    this._wrapSearchService.Username$ = this._acct.currentUserName;
    this._wrapSearchService.HomePage$.next(false);
   
  }

  @Input() message: MessageVM;
  @Input() selectedTab:number;
  @Output() messageIdToDelete = new EventEmitter();
  @Output() messageIdToUnread = new EventEmitter();
  inbox: number = EmailTab.inbox-1;
  outbox: number = EmailTab.outbox-1;
  isMessageStantAlone = false;
  formMessage: FormGroup;
  isReplayOpen = false;
  isDisplayReplayAll = false;
  collapsed = true;
  isLoading = false;
  businessList = [];
  formats = ['background', 'bold', 'color', 'font', 'italic', 'link', 'size', 'underline'];
  dropdownBusinessesSettings = {
    singleSelection: false,
    idField: "id",
    textField: 'buisnessName',
    searchPlaceholderText: 'התחילי להקליד את שם העסק',
    noDataAvailablePlaceholderText: 'לא נמצאו תוצאות',
    enableCheckAll: false,
    allowSearchFilter: true,
    closeDropDownOnSelection: false,
    itemsShowLimit: 3
  }

  get getBusiness() {
    return this.businessList.reduce((acc, curr) => {
      acc[curr.id] = curr;
      return acc;
    }, {});
  }
  ngOnInit(): void {
    console.log("========== selectedTab "+this.selectedTab);
    console.log("m-s",this.message);
    
    this.formMessage = new FormGroup({
      Subject: new FormControl('', Validators.maxLength(80)),
      MessageText: new FormControl(''),
      CollaborationType: new FormControl(),
      ListMessagesTo: new FormControl([])
    });
    if (this.message) {
      if (this.message.ChildrenMessages?.length>1) {
        this.scrollToMessage(this.message.ChildrenMessages[this.message.ChildrenMessages.length-1].Id);
      }
    }
    else {
      this._wrapSearchService.getCurrentBusiness().subscribe(res => {
        if (!res) {
          this._funcService.openRegitserNoBusinessPopup().subscribe(obs => this._router.navigate(['/']));
        }
        else {
          this.getMessage();
        }
      });
      this.breadcrumbService.setItem([
        { label: 'דף הבית', routerLink: ['/'] },
        { label: 'אזור הודעות', routerLink: ['/messages'] },
        { label: 'הודעה חדשה', routerLink: [this._router.url] }
      ]);
    }
  }

  getMessage() {
    this._ActivatedRoute.queryParams.subscribe(params => {
      const messageId = params['messageId'];
      const toEmail = params['toEmail'];
      // if (messageId && toEmail) {
      this.isMessageStantAlone = true;
      if (this._acct.Email.value && this._acct.Email.value == toEmail) {
        this._messageService.getMessageById(this._acct.currentBusiness.value.id, toEmail, messageId).subscribe(res => {
          this.message = res;
          if (this.message.ChildrenMessages?.length>1) {
            this.scrollToMessage(this.message.ChildrenMessages[this.message.ChildrenMessages.length-1].Id);
            res.ChildrenMessages[this.message.ChildrenMessages.length - 1]['collapsed'] = true;
          }
          else {
            this.message['collapsed'] = true;
          }
        });
      }
      if (this.message.isCurrentUserRead) {
        this.message.isCurrentUserRead = true;
      }
      if (this.message.isCurrentUserNew) {
        this.message.isCurrentUserNew = false;
        let newMessagesCount = this._wrapSearchService.newMessages$.value;
        this._wrapSearchService.newMessages$.next(newMessagesCount - 1);
      }
    });

  }
//#region scroll
  scrollToMessage(messageId?) {
    // let parentDiv = document.q("message");
    // let innerDiv = document.getElementById(messageId);
    let parentDivJQ = $("#message");
    // let innerDivJQ =;
    // innerDiv.scrollIntoView({
    //   behavior: "smooth",
    //   block: "start",
    //   inline: "nearest"
    // })
    parentDivJQ.scrollTop(parentDivJQ.height() - $("#" + messageId).height());
  }
  scrollTo(element, to, duration, onDone?) {
    var start = element.scrollTop,
      change = to - start,
      startTime = performance.now(),
      val, now, elapsed, t;

    function animateScroll() {
      now = performance.now();
      elapsed = (now - startTime) / 1000;
      t = (elapsed / duration);

      element.scrollTop = start + change * this.easeInOutQuad(t);

      if (t < 1)
        window.requestAnimationFrame(animateScroll);
      else
        onDone && onDone();
    };

    animateScroll();
  }

  easeInOutQuad(t): number { return t < .5 ? 2 * t * t : -1 + (4 - 2 * t) * t };
//#endregion
  

  getBusinesList(toAll?: boolean) {
    if (!this._wrapSearchService.shrunkenBuisnessList) {
      this._buisnessService.getBusinessNamesPictUser().subscribe(res => {
        console.log(res);
        this._wrapSearchService.shrunkenBuisnessList = res;
        this.businessList = res;
        this.replay(toAll);
      });
    }
    else {
      this.businessList = this._wrapSearchService.shrunkenBuisnessList;
      this.replay(toAll);
    }
  }

  replay(toAll?: boolean) {
    if (!this.businessList.length) {
      this.getBusinesList(toAll);
    }
    else {
      let listMessageTo = [];
      let lastMessage = this.message.ChildrenMessages?.length ? this.message.ChildrenMessages[this.message.ChildrenMessages.length - 1] : this.message;
      listMessageTo.push(lastMessage.BusinessFrom);
      if (toAll) {
        lastMessage.ListMessagesTo.map(bus => { bus.BusinessIdTo != this._acct.currentBusiness.value.id ? listMessageTo.push(bus.BuisnessTo) : "" });
      }
      this.formMessage.get('ListMessagesTo').setValue(listMessageTo);
      this.isReplayOpen = true;
    }
  }
  cancelReplay() {
    this.formMessage.get('ListMessagesTo').setValue([]);
    this.isReplayOpen = false;
  }
  deleteMessage(message: MessageVM,event?) {
    // if (!messId) {
    //   var a = this.message.ListMessagesTo.find(x => x.BusinessIdTo == this._acct.currentBusiness.value.id);
    //   if (a) {
    //     this._messageService.setMessageAsDeleted(a.Id, true, this._acct.Email.value).subscribe(data => {
    //       if (this.isMessageStantAlone) {
    //         this._router.navigate(['/messages']);
    //       }
    //       else {
    //         this.messageIdToDelete.emit(this.message.Id);
    //       }
    //     });
    //   }
    // }
    // else {
     // var findMes = message.ChildrenMessages.find(mesId => mesId.Id == messId)
     event?.stopPropagation();
      var a = message.ListMessagesTo.find(x => x.BusinessIdTo == this._acct.currentBusiness.value.id);
      if (a) {
        this._messageService.setMessageAsDeleted(a.Id, true, this._acct.Email.value).subscribe(res => {
          if(message.Id == this.message.Id){
            if (this.isMessageStantAlone) {
              this._router.navigate(['/messages']);
            }
            else {
              this.messageIdToDelete.emit(this.message.Id);
            }
          }
          else{
            this.message.ChildrenMessages = this.message.ChildrenMessages.filter(x => x.Id != message.Id)
          }
        });
      }
    // }
  }

  markAsUnRead() {
    this._messageService.setMessageAsRead(this.message.Id, this._acct.currentBusiness.value.id, this._acct.Email.value, false).subscribe(res => {
      if (this.isMessageStantAlone) {
        this._router.navigate(['/messages']);
      }
      else {
        this.messageIdToUnread.emit(this.message.Id);
      }
    });
  }

  send() {
    if (this.formMessage.valid) {
      console.log(this.formMessage.value);
      let newMessage: MessageVM = {
        Id: "00000000-0000-0000-0000-000000000000",
        Subject: this.message.Subject,
        BusinessIdFrom: this._acct.currentBusiness.value.id,
        ParentMessagesId: this.message.Id,
        MessageText: this.formMessage.get('MessageText').value,
        CreatedAt: new Date(),
        ListMessagesTo: [],
        EmailFrom: this._acct.Email.value,
      }
      let ListMessagesTo: { id: number, buisnessName: string }[] = this.formMessage.get('ListMessagesTo').value;
      if (ListMessagesTo && ListMessagesTo.length) {
        ListMessagesTo.map(mto => {
          newMessage.ListMessagesTo.push({ Id: 0, BusinessIdTo: mto.id, IsNew: true , BusinessIdFrom: newMessage.BusinessIdFrom});
        });
      }
      this.isLoading = true;
      this._messageService.createMessage(newMessage).subscribe((res) => {
        console.log(res);
        res["collapsed"] = true;
        this.message.ChildrenMessages.push(res);
        this.isLoading = false;
        this.cancelReplay();
      }, error => {
        this.isLoading = false;
        Swal.fire({
          title: 'משהו השתבש לא הצלחנו לשלוח את ההודעה',
          icon: 'error',
          showClass: {
            popup: 'animate__animated animate__fadeInDown'
          },
          hideClass: {
            popup: 'animate__animated animate__fadeOutUp'
          }
        });
      });
    }
  }
}
