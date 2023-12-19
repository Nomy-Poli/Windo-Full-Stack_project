import { BrowserModule } from '@angular/platform-browser';
import { NgSelectModule } from '@ng-select/ng-select';
import { CUSTOM_ELEMENTS_SCHEMA, ErrorHandler, NgModule } from '@angular/core';
import { SocialAuthServiceConfig, SocialAuthService, GoogleLoginProvider } from 'angularx-social-login';
//import { GoogleLoginProvider } from 'angularx-social-login';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AboutUsComponent } from './about-us/about-us.component';
import { ContactUsComponent } from './contact-us/contact-us.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
// import { HomeComponent } from './home/home.component';
import { HomeComponent } from './windo-pages/homePage/home.component';

import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { SendCodeComponent } from './send-code/send-code.component';
import { TermsComponent } from './terms/terms.component';
import { ValidateCodeComponent } from './validate-code/validate-code.component';
import { UserComponent } from './user/user.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { CookieService } from 'ngx-cookie-service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthGuardService } from './services/auth-guard.service';
import { ConfirmEmailComponent } from './confirm-email/confirm-email.component';
import { AccountService } from './services/confirmEmailAccount.service';
import { ContactService } from './services/contact.service';
import { AuthenticationService } from './services/authentication.service';
import { ErrorHandlerService } from './services/errorHandler.service';
//import { RegistrationComponent } from './windo-pages/registration/registration.component';
import { BarterListComponent } from './windo-pages/barter-list/barter-list.component';
import { ChatsComponent } from './windo-pages/chats/chats.component';
import { HeaderComponent } from './windo-pages/header/header.component';
import { FooterComponent } from './windo-pages/footer/footer.component';
import { SearchDomainComponent } from './windo-pages/search-domain/search-domain.component';
import { FavoritesComponent } from './windo-pages/favorites/favorites.component';
import { ProfileComponent } from './windo-pages/profile/profile.component';
import { ViewCardsComponent } from './windo-pages/barter-list/view-cards/view-cards.component';
import { ViewListComponent } from './windo-pages/barter-list/view-list/view-list.component';
//import { LoginComponent } from './angular-pages/login/login.component';
import { BuisnessService } from './services/Buisness.service';

import { CollaborationsService } from './services/Collaboration.service';
import { AdvertismentService } from './services/advertisment.service';
import { ScoringService } from './services/Scoring.service';
import { WrapperSearchService } from './services/wrapper-search.service';
import { WrapperFuncService } from './services/wrapper-func.service';
import { SearchCategoryService } from './services/SearchCategory.service';
import { BreadcrumbService } from './services/breadcrumb.service';
import { GetFirstWord } from './windo-pages/GetFirstWord';

//prime ng
import { DropdownModule } from 'primeng/dropdown';
// import {ImageModule} from 'primeng/image';
import { AccordionModule } from 'primeng/accordion'; //accordion and accordion tab
import { GalleriaModule } from 'primeng/galleria';
import { CarouselModule } from 'primeng/carousel';
import { ButtonModule } from 'primeng/button';
import { ToastModule } from 'primeng/toast';
import { PaginatorModule } from 'primeng/paginator';
// import { MdbModule } from 'mdb-angular-ui-kit';
import { BreadcrumbModule } from 'primeng/breadcrumb';
import { ProgressBarModule } from 'primeng/progressbar';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ToggleButtonModule } from 'primeng/togglebutton';
import { TimelineModule } from 'primeng/timeline';
import { InputSwitchModule } from 'primeng/inputswitch';
import { MultiSelectModule } from 'primeng/multiselect';
import { FileUploadModule } from 'primeng/fileupload';
import { MessagesModule } from 'primeng/messages';
import { MessageModule } from 'primeng/message';
import { environment } from 'src/environments/environment';
import { BusinessViewComponent } from './windo-pages/business-view/business-view.component';
import { ReportCollaborationFormComponent } from './windo-pages/report-collaboration-form/report-collaboration-form.component';
import { ReportDealTypesComponent } from './windo-pages/report-collaboration-form/report-deal-types/report-deal-types.component';
import { DealDetailsComponent } from './windo-pages/report-collaboration-form/deal-details/deal-details.component';
import { StepsModule } from 'primeng/steps';
// import { MenuItem } from 'primeng/api';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { CommonModule } from '@angular/common';
import { DealMoreDetailsComponent } from './windo-pages/report-collaboration-form/deal-more-details/deal-more-details.component';
import { InputTextModule } from 'primeng/inputtext';
import { FeedbackComponent } from './windo-pages/report-collaboration-form/feedback/feedback.component';
import { ThankYouComponent } from './windo-pages/report-collaboration-form/thank-you/thank-you.component';
import { PartnerConfirmedComponent } from './windo-pages/report-collaboration-form/partner-confirmed/partner-confirmed.component';
import { RadioButtonModule } from 'primeng/radiobutton';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { WithoutBussinessComponent } from './windo-pages/without-bussiness/without-bussiness.component';
import { CaseStudyHomeComponent } from './windo-pages/case-stusy/case-study-home/case-study-home.component';
import { CaseStudyListComponent } from './windo-pages/case-stusy/case-study-list/case-study-list.component';
import { CaseStudyFormComponent } from './windo-pages/case-stusy/case-study-form/case-study-form.component';
import { CaseStudyCardesComponent } from './windo-pages/case-stusy/case-study-cardes/case-study-cardes.component';
import { CollaborationListComponent } from './windo-pages/case-stusy/collaboration-list/collaboration-list.component';
import { TableModule } from 'primeng/table';
import { TabViewModule } from 'primeng/tabview';
import { BigPictureComponent } from './windo-pages/business-view/big-picture/big-picture.component';
import { VisionComponent } from './windo-pages/content-pages/vision/vision.component';
import { IndependentComponent } from './windo-pages/content-pages/independent/independent.component';
import { HiringComponent } from './windo-pages/content-pages/hiring/hiring.component';
import { AccessibilityArrangementsComponent } from './windo-pages/accessibility-arrangements/accessibility-arrangements.component';
import { AccountNSwagService } from './services/AccountNSwag.service';
import { SendEmailAgainComponent } from './windo-pages/send-email-again/send-email-again.component';
import { MessagesComponent } from './windo-pages/messages/messages.component';
import { MessagesListComponent } from './windo-pages/messages/messages-list/messages-list.component';
import { MessageCardComponent } from './windo-pages/messages/message-card/message-card.component';
import { cutTextPipe } from './windo-pages/short-text.pipe';
import { EditorModule } from 'primeng/editor';
import { PanelModule } from 'primeng/panel';
import { MessageService } from './services/Message.service';
import { EmailTab } from 'src/app/services/Message.service';
import { NoteService } from './services/Note.service';
import { NewMessagComponent } from './windo-pages/messages/new-messag/new-messag.component';
import { DialogService, DynamicDialogConfig, DynamicDialogModule, DynamicDialogRef } from 'primeng/dynamicdialog';
import { WhenItWasPipe } from './windo-pages/when-it-was.pipe';
import { ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { TooltipModule } from 'primeng/tooltip';
import {RippleModule} from 'primeng/ripple';
import { AdvertisingComponent } from './windo-pages/barter-list/advertising/advertising.component';
import { HomeNotesComponent } from './windo-pages/notes/home-notes/home-notes.component';
import { CreateNoteComponent } from './windo-pages/notes/create-note/create-note.component';
import { BoardComponent } from './windo-pages/notes/board/board.component';
import { NoteComponent } from './windo-pages/notes/board/note/note.component';
import { NotesListComponent } from './windo-pages/notes/notes-list/notes-list.component';
import { CalendarModule } from 'primeng/calendar';
import { FQAComponent } from './windo-pages/fqa/fqa.component';
import {MessageService as ToastService} from 'primeng/api';
import { BusinessTemplateComponent } from './windo-pages/business-template/business-template.component';
import { ContactDetailsComponent } from './windo-pages/contact-details/contact-details.component';
import { BanersCatalogComponent } from './windo-pages/advertisments/baners-catalog/baners-catalog.component';
import { AdOrderFormComponent } from './windo-pages/manager/ad-order-form/ad-order-form.component';
import { OrderServiceRequestsListComponent } from './windo-pages/manager/order-service-requests-list/order-service-requests-list.component';
import { ManagerAreaComponent } from './windo-pages/manager/manager-area/manager-area.component';
import { OrderServeiceListComponent } from './windo-pages/manager/order-serveice-list/order-serveice-list.component';
import { ClientsListComponent } from './windo-pages/manager/clients-list/clients-list.component';
import { AdvertismentAreaComponent } from './windo-pages/advertisments/advertisment-area/advertisment-area.component';
import { RequestOrderFormComponent } from './windo-pages/advertisments/request-order-form/request-order-form.component';
import { ServicesAreaComponent } from './windo-pages/manager/services-area/services-area.component';
import { NetworkingManagmentAreaComponent } from './windo-pages/manager/networking-managment-area/networking-managment-area.component';
import { ScoreManagementAreaComponent } from './windo-pages/manager/score-management-area/score-management-area.component';
import {SliderModule} from 'primeng/slider';
import {ContextMenuModule} from 'primeng/contextmenu';
import {DialogModule} from 'primeng/dialog';
import { ClientFormComponent } from './windo-pages/manager/client-form/client-form.component';
import { SpinnerComponent } from './windo-pages/spinner/spinner.component';
import { BannerFormComponent } from './windo-pages/manager/banner-form/banner-form.component';
import { BannersListComponent } from './windo-pages/manager/banners-list/banners-list.component';
import { ServicesListComponent } from './windo-pages//advertisments/services-list/services-list.component';
import { BannerDetailsComponent } from './windo-pages/advertisments/banner-details/banner-details.component';
import { ServicesFormComponent } from './windo-pages/manager/services-form/services-form.component';
import { ImageUploadComponent } from './windo-pages/manager/image-upload/image-upload.component';
import { ImageDragDirective } from './windo-pages/manager/image-drag.directive';
import { ProductsListComponent } from './windo-pages/advertisments/products-list/products-list.component';
import { WindoSiteServicesFormComponent } from './windo-pages/manager/windo-site-services-form/windo-site-services-form.component';
import { SystemOperationsListComponent } from './windo-pages/manager/system-operations-list/system-operations-list.component';
import { BuisnessScoringListComponent } from './windo-pages/manager/buisness-scoring-list/buisness-scoring-list.component';
import { BuisnessScoringDetailComponent } from './windo-pages/manager/buisness-scoring-detail/buisness-scoring-detail.component';
import { SystemOperitionFormComponent } from './windo-pages/manager/system-operition-form/system-operition-form.component';
import { ManualScoringComponent } from './windo-pages/manager/manual-scoring/manual-scoring.component';
import { SystemManuelOperitionComponent } from './windo-pages/manager/system-manuel-operition/system-manuel-operition.component';
import { SystemManuelOperitionFormComponent } from './windo-pages/manager/system-manuel-operition-form/system-manuel-operition-form.component';
import { NetworkingGroupListComponent } from './windo-pages/manager/networking-group-list/networking-group-list.component';
import { NetworkingGroupFormComponent } from './windo-pages/manager/networking-group-form/networking-group-form.component';
import { NetworkingService } from './services/Networking.service';
import { NetworkingGroupBuisnessListComponent } from './windo-pages/manager/networking-group-buisness-list/networking-group-buisness-list.component';
import { NetworkingDetailsGroupFormComponent } from './windo-pages/manager/networking-details-group-form/networking-details-group-form.component';
import { BusinessTableComponent } from './windo-pages/manager/manager-area/business-table/business-table.component';
import { AlphonUsersComponent } from './alphon-users/alphon-users.component';
import { MistamshimService } from './services/mistamshim';
import { HubComponent } from './windo-pages/content-pages/hub/hub.component';

@NgModule({
  declarations: [
    AppComponent,
    AboutUsComponent,
    ForgotPasswordComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    SendCodeComponent,
    TermsComponent,
    ValidateCodeComponent,
    UserComponent,
    //windo components
    //HomeComponent,
    //RegistrationComponent,
    BarterListComponent,
    ChatsComponent,
    HeaderComponent,
    FooterComponent,
    SearchDomainComponent,
    FavoritesComponent,
    ProfileComponent,
    ViewCardsComponent,
    ViewListComponent,
    LoginComponent,
    ConfirmEmailComponent,
    GetFirstWord,
    BusinessViewComponent,
    ReportCollaborationFormComponent,
    ReportDealTypesComponent,
    DealDetailsComponent,
    DealMoreDetailsComponent,
    FeedbackComponent,
    ThankYouComponent,
    PartnerConfirmedComponent,
    WithoutBussinessComponent,
    CaseStudyHomeComponent,
    CaseStudyListComponent,
    CaseStudyFormComponent,
    CaseStudyCardesComponent,
    CaseStudyListComponent,
    CollaborationListComponent,
    ContactUsComponent,
    BigPictureComponent,
    VisionComponent,
    IndependentComponent,
    HiringComponent,
    AccessibilityArrangementsComponent,
    SendEmailAgainComponent,
    MessagesComponent,
    MessagesListComponent,
    MessageCardComponent,
    cutTextPipe,
    NewMessagComponent,
    WhenItWasPipe,
    AdvertisingComponent,
    HomeNotesComponent,
    CreateNoteComponent,
    BoardComponent,
    NoteComponent,
    NotesListComponent,
    FQAComponent,
    BusinessTemplateComponent,
    ContactDetailsComponent,
    BanersCatalogComponent,
    AdOrderFormComponent,
    OrderServiceRequestsListComponent,
    ManagerAreaComponent,
    OrderServeiceListComponent,
    ClientsListComponent,
    AdvertismentAreaComponent,
    RequestOrderFormComponent,
    ServicesAreaComponent,
    ScoreManagementAreaComponent,
    ClientFormComponent,
    OrderServiceRequestsListComponent,
    SpinnerComponent,
    BannerFormComponent,
    BannersListComponent,
    BannerDetailsComponent,
    ServicesListComponent,
    ServicesFormComponent,
    ImageUploadComponent,
    ImageDragDirective,
    ProductsListComponent,
    WindoSiteServicesFormComponent,
    SystemOperationsListComponent,
    BuisnessScoringListComponent,
    BuisnessScoringDetailComponent,
    SystemOperitionFormComponent,
    ManualScoringComponent,
    SystemManuelOperitionComponent,
    SystemManuelOperitionFormComponent,
    NetworkingManagmentAreaComponent,
    NetworkingGroupListComponent,
    NetworkingGroupFormComponent,
    NetworkingGroupBuisnessListComponent,
    NetworkingDetailsGroupFormComponent,
    BusinessTableComponent,
    AlphonUsersComponent,
    HubComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    CommonModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    NgSelectModule,
    FormsModule,
    DropdownModule,
    // ImageModule,
    AccordionModule,
    GalleriaModule,
    CarouselModule,
    TooltipModule,
    RippleModule,
    ToastModule,
    ButtonModule,
    BreadcrumbModule,
    ProgressBarModule,
    ToggleButtonModule,
    FileUploadModule,
    MessageModule,
    MessagesModule,
    TimelineModule,
    EditorModule,
    PanelModule,
    ToastrModule.forRoot(),
    NgMultiSelectDropDownModule.forRoot(),
    // MdbModule,
    AutoCompleteModule,
    InputSwitchModule,
    MultiSelectModule,
    StepsModule,
    InputTextModule,
    InputTextareaModule,
    RadioButtonModule,
    TableModule,
    TabViewModule,
    DynamicDialogModule,
    PaginatorModule,
    CalendarModule,
    NgbModule,
    SliderModule,
    ContextMenuModule,
    DialogModule
  ],

  providers: [
    AuthGuardService,
    CookieService,
    BuisnessService,
    MistamshimService,
    AccountService,
    AccountNSwagService,
    ContactService,
    WrapperSearchService,
    WrapperFuncService,
    SearchCategoryService,
    BreadcrumbService,
    AuthenticationService,
    SocialAuthService,
    CollaborationsService,
    AdvertismentService,
    ScoringService,
    NetworkingService,
    MessageService,
    NoteService,
    DialogService,
    ToastService,
    DynamicDialogRef,
    DynamicDialogConfig,
    { provide: "API_BASE_URL", useValue: environment.API_BASE_URL },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorHandlerService,//ErrorHandler,
      multi: true
    },
    {
      provide: 'SocialAuthServiceConfig',
      useValue: {
        autoLogin: false,
        providers: [
          {
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider(
              environment.googleClientId
            )
          },
        ],
      } as SocialAuthServiceConfig
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
