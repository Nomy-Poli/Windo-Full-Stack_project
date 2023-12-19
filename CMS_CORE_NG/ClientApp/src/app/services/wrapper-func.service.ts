import { Injectable } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DialogService } from 'primeng/dynamicdialog';
import { ForgotPasswordComponent } from '../forgot-password/forgot-password.component';
import { LoginComponent } from '../login/login.component';
import { RegisterComponent } from '../register/register.component';
import { TermsComponent } from '../terms/terms.component';
import { BannerDetailsComponent } from '../windo-pages/advertisments/banner-details/banner-details.component';
import { RequestOrderFormComponent } from '../windo-pages/advertisments/request-order-form/request-order-form.component';
import { BigPictureComponent } from '../windo-pages/business-view/big-picture/big-picture.component';
import { ContactDetailsComponent } from '../windo-pages/contact-details/contact-details.component';
import { AdOrderFormComponent } from '../windo-pages/manager/ad-order-form/ad-order-form.component';
import { BannerFormComponent } from '../windo-pages/manager/banner-form/banner-form.component';
import { BuisnessScoringDetailComponent } from '../windo-pages/manager/buisness-scoring-detail/buisness-scoring-detail.component';
import { ClientFormComponent } from '../windo-pages/manager/client-form/client-form.component';
import { ServicesFormComponent } from '../windo-pages/manager/services-form/services-form.component';
import { SystemManuelOperitionFormComponent } from '../windo-pages/manager/system-manuel-operition-form/system-manuel-operition-form.component';
import { SystemOperitionFormComponent } from '../windo-pages/manager/system-operition-form/system-operition-form.component';
import { WindoSiteServicesFormComponent } from '../windo-pages/manager/windo-site-services-form/windo-site-services-form.component';
import { NewMessagComponent } from '../windo-pages/messages/new-messag/new-messag.component';
import { NoteComponent } from '../windo-pages/notes/board/note/note.component';
import { CreateNoteComponent } from '../windo-pages/notes/create-note/create-note.component';
// import { CreateNoteComponent } from '../windo-pages/notes/create-note/create-note.component';
import { PartnerConfirmedComponent } from '../windo-pages/report-collaboration-form/partner-confirmed/partner-confirmed.component';
import { SendEmailAgainComponent } from '../windo-pages/send-email-again/send-email-again.component';
import { WithoutBussinessComponent } from '../windo-pages/without-bussiness/without-bussiness.component';
import { BannerVM, CatalogServiceVM,ServiceTypeVM } from './advertisment.service';
import { NoteVM } from './Note.service';
import { BusinessForScoringVM, ScroingOperationVM } from './Scoring.service';
import { NetworkingGroupBusinessVM, NetworkingGroupVM } from './Networking.service';
import { NetworkingGroupFormComponent } from '../windo-pages/manager/networking-group-form/networking-group-form.component';
import { NetworkingDetailsGroupFormComponent } from '../windo-pages/manager/networking-details-group-form/networking-details-group-form.component';

@Injectable({
    providedIn: 'root'
})
export class WrapperFuncService {
    megamenue: any = false;

    constructor(private modalService: NgbModal, public dialogService: DialogService) {
        this.megamenue = localStorage.getItem('OpenMegaMenue');
    }
    //פתיחת קומפוננט' הלוגאין כפופאפ
    openLoginDialog() {
        this.closeDialog();
        this.modalService.open(LoginComponent, { centered:true, size:'md' });
    }

    openRequestOrderDialog(id:number)
    {
      this.closeDialog();
      const modalRef= this.modalService.open(RequestOrderFormComponent,{ centered: true, size: 'lg' });
      modalRef.componentInstance.makat=id;
    }
    openBannerDialog(banner:BannerVM)
    {
      this.closeDialog();
      const modalRef= this.modalService.open(BannerDetailsComponent,{ centered: true, size: 'lg' });
      modalRef.componentInstance.banner = banner;
    }
    openOrderForm(makat?, orderId?, requestId?){
      console.log(makat);
      const modalRef= this.modalService.open(AdOrderFormComponent,{ centered: true, size: 'lg' });
      modalRef.componentInstance.makat=makat; 
      modalRef.componentInstance.orderId=orderId; 
      modalRef.componentInstance.requestId=requestId; 
      return modalRef.componentInstance.closed;
    }
    openServiceForm(service?:CatalogServiceVM){
      console.log("service",service);
      const modalRef= this.modalService.open(ServicesFormComponent,{ centered: true, size: 'lg' });
      modalRef.componentInstance.service=service;
      return modalRef.componentInstance.closed;
    }
    openSiteServicesForm(siteServices?:ServiceTypeVM){
      console.log("service",siteServices);
      const modalRef= this.modalService.open (WindoSiteServicesFormComponent,{ centered: true, size: 'lg' });
      modalRef.componentInstance.service=siteServices;
      return modalRef.componentInstance.closed;
    }

    openClientForm(clientId?, requestId?){
      const modalRef= this.modalService.open(ClientFormComponent,{ centered: true, size: 'lg' });
      modalRef.componentInstance.clientId = clientId; 
      modalRef.componentInstance.requestId = requestId; 
      return modalRef.componentInstance.closed;
    }
    openSystemOperitionForm(operition? : ScroingOperationVM){
      const modalRef= this.modalService.open(SystemOperitionFormComponent,{ centered: true, size: 'lg' });
      modalRef.componentInstance.operition = operition; 
      return modalRef.componentInstance.closed;
    }

    openSystemManuelOperitionForm(operition? : ScroingOperationVM){
      const modalRef= this.modalService.open(SystemManuelOperitionFormComponent,{ centered: true, size: 'lg' });
      modalRef.componentInstance.operition = operition; 
      return modalRef.componentInstance.closed;
    }
    openNetworkingGroupForm(group? :NetworkingGroupVM ){
      const modalRef= this.modalService.open(NetworkingGroupFormComponent,{ centered: true, size: 'lg' });
      modalRef.componentInstance.group = group; 
      return modalRef.componentInstance.closed;
    }
    openNetworkingDetailsGroupForm(groupBusiness?){
      const modalRef= this.modalService.open(NetworkingDetailsGroupFormComponent,{ centered: true, size: 'lg' });
      modalRef.componentInstance.groupBusiness= groupBusiness;
      return modalRef.componentInstance.closed;
    }
    openBannerForm(bannerId?){
      const modalRef= this.modalService.open(BannerFormComponent,{ centered: true, size: 'lg' });
      modalRef.componentInstance.bannerId = bannerId; 
      return modalRef.componentInstance.closed;
    }
    //open the register comm like popup
    openDialogRegister() {
        this.closeDialog();
        this.modalService.open(RegisterComponent, { centered: true, size: 'md' });
    }
    //open forgat password comm like popup
    openDialogForgotPassword() {
        this.closeDialog();
        this.modalService.open(ForgotPasswordComponent, { centered: true, size: 'sm' });
    }

  //open send email again like popup
  openDialogSendEmailAgain() {
    this.closeDialog();
    this.modalService.open(SendEmailAgainComponent, { centered: true, size: 'md' });
  }
  //open the term comm like popup
  openTermsDialog() {
    this.closeDialog();
    this.modalService.open(TermsComponent, { centered: true, size: 'md' });
  }

    openDialogconfirmPartner(dealType) {
        this.closeDialog();
        const modalRef = this.modalService.open(PartnerConfirmedComponent, { centered: true, size: 'sm' });
        modalRef.componentInstance.dealType = dealType;
        return modalRef.componentInstance.passRes;
    }

    openPictureDialog(src) {
        this.closeDialog();
        const modalRef = this.modalService.open(BigPictureComponent, { centered: true, size: 'md' });
        modalRef.componentInstance.src = src;
    }

    //close all the popups
    closeDialog() {
        this.modalService.dismissAll();
        this.dialogService.dialogComponentRefMap.forEach(dialog => {
          dialog.destroy();
        });

    }

    openRegitserNoBusinessPopup() {
        this.closeDialog();
        const modalRef = this.modalService.open(WithoutBussinessComponent, { centered: true, size: 'md' });
        return modalRef.componentInstance.closed;
    }

    openBusinessContactDetails(details) {
      this.closeDialog();
      const modalRef = this.modalService.open(ContactDetailsComponent, { centered: true, size: 'lg' });
      modalRef.componentInstance.details = details;
  }

  openNewMassage(toBusiness?: string, subject?: string) {
    const ref = this.dialogService.open(NewMessagComponent, {
      header: "שלחי הודעה חדשה",
      style: {
        position: 'absolute',
        overflow: 'auto',
        bottom: '0',
        left: '10px',
        'max-height': '85%'
      },
      width: '41%',
      data: {
        businessId: toBusiness,
        subject: subject
      } 
    });
    return ref.onClose;
  }

  OpenScoringDetail(buisnessId){
    const modalRef= this.modalService.open(BuisnessScoringDetailComponent,{ centered: true, size: 'lg'});
    modalRef.componentInstance.BuisnessId = buisnessId; 
    return modalRef.hidden;
  }
  openCreateNote(boardId?: number,isGroups?:boolean, note?: NoteVM) {
    const modalRef = this.modalService.open(CreateNoteComponent, {
      centered: true, size: 'md' 
      // header: note ? "עריכת מודעה" : "צרי מודעה חדשה",
      // style: {
      //   position: 'absolute',
      //   overflow: 'auto',
      //   top: '20%',
      //   left: '30%',
      //   "max-height": "41rem",
      // },
      // width: '40%',
      // data: { note: note, boardId: boardId }
    });
    modalRef.componentInstance.noteToUpdate = note;
    modalRef.componentInstance.boardId = boardId;
    modalRef.componentInstance.isGroups = isGroups;
    return modalRef.componentInstance.closed;
  }

  openNote(note:NoteVM){
    const modalRef = this.modalService.open(NoteComponent,{ centered:true ,size:'md'});
    modalRef.componentInstance.noteId = note.Id;
    modalRef.componentInstance.note = note;
    return modalRef.componentInstance.closed;
  }

}

