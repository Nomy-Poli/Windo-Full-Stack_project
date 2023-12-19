import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from 'src/app/services/account.service';
import { BreadcrumbService } from 'src/app/services/breadcrumb.service';
import { BuisnessService } from 'src/app/services/Buisness.service';
import { EmailTab, MessageService, MessageVM } from 'src/app/services/Message.service';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.scss']
})

export class MessagesComponent implements OnInit, OnDestroy {

 
  constructor(public breadcrumbService: BreadcrumbService,
    private _messageService: MessageService,
    public _acct: AccountService,
    public _activatedRoute: ActivatedRoute,
    public _funcService: WrapperFuncService,
    private _wrapperSearchService: WrapperSearchService,
    private _businessService: BuisnessService,
    private _router: Router
  ) {

    breadcrumbService.setItem([
      { label: 'דף הבית', routerLink: ['/'] },
      { label: 'אזור הודעות', routerLink: ['/messages'] }
    ]);
  }

   ngOnDestroy(): void {
    this._funcService.closeDialog();
  }

  //activeTabMessages:number =0;
  //activeTabOutgoingMessages:number =1;
  activeTab : number = EmailTab.inbox;  //this.activeTabMessages;
  messageList: MessageVM[];
  outgoingMessageList:MessageVM[];
  currentMessage;
  notReadCount = 0;
  isLoading = true;
  messageListToView = null;

  onTabChange(event: any){
    console.log(`=========== onTabChange ${event.index} `);
    console.log(`messageListToView - before ${this.messageListToView}`);
      if (event.index==EmailTab.outbox){
        this.messageListToView = this.outgoingMessageList;
        if (this.outgoingMessageList && this.outgoingMessageList.length>0){
          this.openMessage(this.outgoingMessageList[0]);}
          else{
            this.currentMessage=null;
          }
        }
      else{
        this.messageListToView = this.messageList;
        if (this.openMessage && this.openMessage.length>0){
        this.openMessage(this.messageList[0]);}
        else{
          this.currentMessage=null;
        }
      }
      console.log(`messageListToView - after ${this.messageListToView}`);
  }

  ngOnInit(): void {
   
    window.scroll(0, 0);
    this._acct.globalStateChanged.subscribe((state) => {
      this._wrapperSearchService.LoginStatus$.next(state.loggedInStatus);
    });
    this._wrapperSearchService.Username$ = this._acct.currentUserName;
    // this._funcService.closeDialog();
    this._wrapperSearchService.HomePage$.next(false);
    this._wrapperSearchService.getCurrentBusiness().subscribe(res => {
      if (res){
      // this.messagesTabClicked();
       this.getMessages();
       this.getOutgoingMessages();
      }
        
      else {
        this.isLoading = false;
        this._funcService.openRegitserNoBusinessPopup().subscribe(obs => {
          this._router.navigate(['/'])
          console.log(obs);

        });
      }
//     if(this.selectedTabIndex == this.activeTabMessages)
      if(this.activeTab == EmailTab.inbox)
      {
        this.messageListToView = this.messageList;
      }
      else{
        this.messageListToView = this.outgoingMessageList;
      }
      console.log(`messageListToView ${this.messageListToView}`);
    })

  }

  getMessages() {
    console.log("============= getMessages נכנס לפונקציה ");
    this._messageService.getMessagesByBusinessId(this._acct.currentBusiness.value.id, this._acct.Email.value).subscribe(res => {
      this.messageList = res;
      this.isLoading = false;
      this._activatedRoute.queryParams.subscribe(params => {
        const messageId = params['messageId'];
        const toEmail = params['toEmail'];
        if(this.activeTab == EmailTab.inbox){
        if (messageId && toEmail) {
          let mess = this.messageList.find(x => x.Id == messageId);
          this.openMessage(mess);
        }
        else if (res && res.length) {
          this.openMessage(res[0]);
        }
        console.log("res:"+res);
        this.notReadCount = res.filter(m => m.isCurrentUserRead == false).length;}
        console.log("res:"+res);
      });

    },err=>{
      this.isLoading = false;
      Swal.fire({
        title: ' ארעה תקלה',
        icon: 'error',
        showClass: {
          popup: 'animate__animated animate__fadeInDown'
        },
        hideClass: {
          popup: 'animate__animated animate__fadeOutUp'
        }}).then(value=>this._router.navigate(['/']));
    });
  }
  getOutgoingMessages() {
    console.log("======== getOutgoingMessages נכנס לפונקציה ");
    
    this._messageService.getOutgoingMessagesByBusinessId(this._acct.currentBusiness.value.id, this._acct.Email.value).subscribe(res => {
      this.outgoingMessageList = res;
      this.isLoading = false;
      this._activatedRoute.queryParams.subscribe(params => {
        const messageId = params['messageId'];
        const toEmail = params['toEmail'];
        if(this.activeTab==EmailTab.outbox){
        if (messageId && toEmail) {
          let mess = this.outgoingMessageList.find(x => x.Id == messageId);
          this.openMessage(mess);
        }
        else if (res && res.length) {
          this.openMessage(res[0]);
        }
        console.log("res:"+res);
        this.notReadCount = res.filter(m => m.isCurrentUserRead == false).length;
      }
      console.log("res:"+res);
      });

    },err=>{
      this.isLoading = false;
      Swal.fire({
        title: ' ארעה תקלה',
        icon: 'error',
        showClass: {
          popup: 'animate__animated animate__fadeInDown'
        },
        hideClass: {
          popup: 'animate__animated animate__fadeOutUp'
        }}).then(value=>this._router.navigate(['/']));
    });
  }

  // messagesTabClicked(){
  //   console.log("messagesTabClicked() " );
  //   this.activeTab = this.activeTabMessages;
  //    if(this.messageList && this.messageList.length>0){
  //     this.openMessage(this.messageList[0]);
  //    }
  // }

  // outgoingMessagesTabClicked(){
  //   console.log("---outgoingMessagesTabClicked() " +this.outgoingMessageList);
  //   this.activeTab=this.activeTabOutgoingMessages;
    
  //   if(this.outgoingMessageList && this.outgoingMessageList.length>0){
  //     this.openMessage(this.outgoingMessageList[0]);
  //   }
  // }

  openMessage(message: MessageVM) {
    console.log(`!!  openMessage - ${message.MessageText} ${message.Subject}`);
    this.currentMessage = null;
    if (message.ChildrenMessages?.length) {
      if (!message.isCurrentUserRead) {
        message["collapsed"] = true;
      }
      for (let index = 0; index < message.ChildrenMessages.length; index++) {
        const mess = message.ChildrenMessages[index];
        if (!mess.isCurrentUserRead) {
          mess["collapsed"] = true;
        }
        else {
          mess["collapsed"] = false;
        }
      }
      message.ChildrenMessages[message.ChildrenMessages.length - 1]["collapsed"] = true;
    }
    else {
      message["collapsed"] = true;
    }
    if (!message.isCurrentUserRead) {
      this.notReadCount -= 1;
      this._messageService.setMessageAsRead(message.Id, this._acct.currentBusiness.value.id, this._acct.Email.value, true).subscribe(res => {
        console.log("res " +res);
      });
    }
    message.isCurrentUserRead = true;
    message.isCurrentUserNew = false;
    this.currentMessage = message;
    console.log(`! this.currentMessage ${this.currentMessage.Subject}`);
    let newMessagesCount = this.messageList.filter(x => x.isCurrentUserNew).length;
    this._wrapperSearchService.newMessages$.next(newMessagesCount);
  }
  messageUnRead(message: MessageVM) {
    this._messageService.setMessageAsRead(message.Id, this._acct.currentBusiness.value.id, this._acct.Email.value, message.isCurrentUserRead).subscribe(res => {
      console.log(res);
      this.notReadCount = this.messageList.filter(m => !m.isCurrentUserRead).length;
    });
  }

  deleteMes(messageId) {
    //TODO לסדר את הרשימה , גם היוצא
    //this.messageList = this.messageList.filter(id => id.Id != messageId);
    console.log(`deleteMes  messageId: ${messageId}`);
    console.log(`messageListToView ${this.messageListToView}`);
    this.messageListToView = this.messageListToView.filter(id => id.Id != messageId);
    this.currentMessage = null;
  }
  markAsUnRead(messageId) {
    
    //TODO let mess = this.messageList.find(id => id.Id == messageId);
    // let mess = this.messageListToView.find(id => id.Id == messageId);
    // mess.isCurrentUserRead = false;
    this.currentMessage.isCurrentUserRead =false;
    this.notReadCount = this.messageList.filter(m => !m.isCurrentUserRead).length;
    this.currentMessage = null;
  }
  relode() : void {
    console.log("enter");
    window.location.reload();
  }
  

  // getMessagesFromCash() {
  //   this.messageList = [
  //     {
  //       Id: "00000000-0000-0000-0000-000000000000",
  //       Subject: "תוכלי ליצור איתי קשר?",
  //       FromEmail: "rutykolsky98@gmail.com",
  //       CreatedAt: new Date(),
  //       BusinessFrom: { "id": 2, "buisnessName": "זר לי", "logoPictureId": "c158dcbd-4cbd-4ebe-a392-ed983d7a7ad1" },
  //       ListMessagesTo: [
  //         { Id: 1, Email: "rutykolsky98@gmail.com", IsRead: false, BuisnessTo: { id: 2, "buisnessName": "זר לי", "logoPictureId": "c158dcbd-4cbd-4ebe-a392-ed983d7a7ad1" } },
  //         { Id: 2, Email: "rut.prog@gmail.com", IsRead: false, BuisnessTo: { id: 6, buisnessName: "כפתור ופרח", logoPictureId: "a62e3edc-48b0-4120-a546-7ab9db8b1511" } },
  //         { Id: 3, Email: "rut.prog@gmail.com", IsRead: false, BuisnessTo: { id: 5, buisnessName: "אמונה בה'", logoPictureId: "b2ad671e-8f94-4ab0-83eb-294418d12207" } }
  //       ],
  //       ParentMessagesId: null,
  //       ChildrenMessages: [],
  //       MessageText: `ישיבת מיר היא ישיבה חרדית-ליטאית ותיקה הנחשבת לאחת משתי הישיבות הגדולות
  //          ביותר בעולם (יחד עם ישיבת לייקווד).<br>
  //                  הישיבה נחשבת כאחת הישיבות הראשונות שנוסדו במתכונת הישיבה החדשה, 
  //           שנייה לישיבת וולוז'ין. נוסדה בעיר מיר שבבלארוס על ידי הרב שמואל טיקטינסקי, לפני שנת ה'תקע"ה. הישיבה נמנתה עם <strong>'ישיבות המוסר</strong> שאימצו את תנועת המוסר. סגנונה הלימודי הושפע מתלמידי ראשי ישיבות שונים שהגיעו ללמוד בישיבה והביאו עמם את סגנון לימודם של רבותיהם, וצביונה המוסרי אף הוא הושפע מתנועת תלמידים זו, בעיקר בוגרי ישיבת סלובודקה שנשלחו לישיבה בידי הסבא מסלובודקה.`
  //     },
  //     {
  //       Id: "00000000-0000-0000-0000-000000000000",
  //       Subject: "תוכלי ליצור איתי קשר?",
  //       FromEmail: "rut.prog@gmail.com",
  //       CreatedAt: new Date(2022, 0, 13, 11, 13, 0),
  //       BusinessFrom: { id: 6, buisnessName: "כפתור ופרח", logoPictureId: "a62e3edc-48b0-4120-a546-7ab9db8b1511" },
  //       ListMessagesTo: [
  //         { Id: 4, Email: "rutykolsky98@gmail.com", IsRead: true, BuisnessTo: { id: 2, "buisnessName": "זר לי", "logoPictureId": "c158dcbd-4cbd-4ebe-a392-ed983d7a7ad1" } },
  //         { Id: 5, Email: "rut.prog@gmail.com", IsRead: false, BuisnessTo: { id: 5, buisnessName: "אמונה בה'", logoPictureId: "b2ad671e-8f94-4ab0-83eb-294418d12207" } }
  //       ],
  //       MessageText: "ישיבת מיר היא ישיבה חרדית-ליטאית ותיקה הנחשבת לאחת משתי הישיבות הגדולות ביותר בעולם (יחד עם ישיבת לייקווד).        הישיבה נחשבת כאחת הישיבות הראשונות שנוסדו במתכונת הישיבה החדשה, שנייה לישיבת וולוז'ין. נוסדה בעיר מיר שבבלארוס על ידי הרב שמואל טיקטינסקי, לפני שנת ה'תקעה. הישיבה נמנתה עם 'ישיבות המוסר' שאימצו את תנועת המוסר. סגנונה הלימודי הושפע מתלמידי ראשי ישיבות שונים שהגיעו ללמוד בישיבה והביאו עמם את סגנון לימודם של רבותיהם, וצביונה המוסרי אף הוא הושפע מתנועת תלמידים זו, בעיקר בוגרי ישיבת סלובודקה שנשלחו לישיבה בידי הסבא מסלובודקה.",
  //       ChildrenMessages: [
  //         {
  //           Id: "3",
  //           Subject: null,
  //           FromEmail: "rut.prog@gmail.com",
  //           CreatedAt: new Date(2022, 1, 13, 12, 41, 0),
  //           BusinessFrom: { id: 5, buisnessName: "אמונה בה'", logoPictureId: "b2ad671e-8f94-4ab0-83eb-294418d12207" },
  //           ListMessagesTo: [
  //             { Id: 1, Email: "rutykolsky98@gmail.com", IsRead: false, BuisnessTo: { id: 2, buisnessName: "זר לי", "logoPictureId": "c158dcbd-4cbd-4ebe-a392-ed983d7a7ad1" } },
  //             { Id: 2, Email: "rut.prog@gmail.com", IsRead: false, BuisnessTo: { id: 6, buisnessName: "כפתור ופרח", logoPictureId: "a62e3edc-48b0-4120-a546-7ab9db8b1511" } },
  //           ],
  //           ParentMessagesId: "2",
  //           ChildrenMessages: [],
  //           MessageText: "תתקשרי אלי בהקדם 0504112122!"
  //         },
  //         {
  //           Id: "",
  //           Subject: null,
  //           FromEmail: "rut.prog@gmail.com",
  //           CreatedAt: new Date(2022, 1, 5, 8, 50, 0),
  //           BusinessFrom: { id: 5, buisnessName: "אמונה בה'", logoPictureId: "b2ad671e-8f94-4ab0-83eb-294418d12207" },
  //           ListMessagesTo: [
  //             { Id: 1, Email: "rutykolsky98@gmail.com", IsRead: false, BuisnessTo: { id: 2, buisnessName: "זר לי", "logoPictureId": "c158dcbd-4cbd-4ebe-a392-ed983d7a7ad1" } },
  //             { Id: 2, Email: "rut.prog@gmail.com", IsRead: false, BuisnessTo: { id: 6, buisnessName: "כפתור ופרח", logoPictureId: "a62e3edc-48b0-4120-a546-7ab9db8b1511" } },
  //           ],
  //           ChildrenMessages: [],
  //           MessageText: "בעצם הסתדרתי לבד"
  //         },
  //       ],
  //     }
  //   ]
  // }
 

}


