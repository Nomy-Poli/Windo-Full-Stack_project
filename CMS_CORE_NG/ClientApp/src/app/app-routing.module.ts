import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
//import { HomeComponent } from './home/home.component';
import { HomeComponent } from './windo-pages/homePage/home.component';
import { LoginComponent } from "./login/login.component";
import { RegisterComponent } from "./register/register.component";
import { ForgotPasswordComponent } from "./forgot-password/forgot-password.component";
import { ValidateCodeComponent } from "./validate-code/validate-code.component";
import { SendCodeComponent } from "./send-code/send-code.component";
import { AboutUsComponent } from "./about-us/about-us.component";
import { TermsComponent } from "./terms/terms.component";
import { ContactUsComponent } from "./contact-us/contact-us.component";
//import { RegistrationComponent } from './windo-pages/registration/registration.component';
import { BarterListComponent } from './windo-pages/barter-list/barter-list.component';
import { ChatsComponent } from './windo-pages/chats/chats.component';
//import { HeaderComponent } from './nav-menu/header/header.component';
//import { FooterComponent } from './windo-pages/footer/footer.component';
import { SearchDomainComponent } from './windo-pages/search-domain/search-domain.component';
import { FavoritesComponent } from './windo-pages/favorites/favorites.component';
import { ProfileComponent } from './windo-pages/profile/profile.component';
import { ViewCardsComponent } from './windo-pages/barter-list/view-cards/view-cards.component';
import { ViewListComponent } from './windo-pages/barter-list/view-list/view-list.component';
//import { LoginComponent } from './angular-pages/login/login.component';
import { ConfirmEmailComponent } from './confirm-email/confirm-email.component';
import { BusinessViewComponent } from './windo-pages/business-view/business-view.component';
import { ReportCollaborationFormComponent } from './windo-pages/report-collaboration-form/report-collaboration-form.component';
import { CaseStudyFormComponent } from './windo-pages/case-stusy/case-study-form/case-study-form.component';
import { CaseStudyListComponent } from './windo-pages/case-stusy/case-study-list/case-study-list.component';
import { CollaborationListComponent } from './windo-pages/case-stusy/collaboration-list/collaboration-list.component';
import { IndependentComponent } from './windo-pages/content-pages/independent/independent.component';
import { HiringComponent } from './windo-pages/content-pages/hiring/hiring.component';
import { VisionComponent } from './windo-pages/content-pages/vision/vision.component';
import { AccessibilityArrangementsComponent } from './windo-pages/accessibility-arrangements/accessibility-arrangements.component';
import { CaseStudyCardesComponent } from './windo-pages/case-stusy/case-study-cardes/case-study-cardes.component';
import { MessagesComponent } from './windo-pages/messages/messages.component';
import { NewMessagComponent } from './windo-pages/messages/new-messag/new-messag.component';
import { MessageCardComponent } from './windo-pages/messages/message-card/message-card.component';
import { AuthGuardService } from './services/auth-guard.service';
import { BoardComponent } from './windo-pages/notes/board/board.component';
import { NoteComponent } from './windo-pages/notes/board/note/note.component';
import { FQAComponent } from './windo-pages/fqa/fqa.component';
import { BanersCatalogComponent } from './windo-pages/advertisments/baners-catalog/baners-catalog.component';
import { AdOrderFormComponent } from './windo-pages/manager/ad-order-form/ad-order-form.component';
import { ManagerAreaComponent } from './windo-pages/manager/manager-area/manager-area.component';
import { OrderServiceRequestsListComponent } from './windo-pages/manager/order-service-requests-list/order-service-requests-list.component';
import { ClientsListComponent } from './windo-pages/manager/clients-list/clients-list.component';
import { OrderServeiceListComponent } from './windo-pages/manager/order-serveice-list/order-serveice-list.component';
import { ServicesAreaComponent } from './windo-pages/manager/services-area/services-area.component';
import { ScoreManagementAreaComponent } from './windo-pages/manager/score-management-area/score-management-area.component';
import { NetworkingManagmentAreaComponent } from './windo-pages/manager/networking-managment-area/networking-managment-area.component';
import { NetworkingGroupBuisnessListComponent } from './windo-pages/manager/networking-group-buisness-list/networking-group-buisness-list.component';
import { RequestOrderFormComponent } from './windo-pages/advertisments/request-order-form/request-order-form.component';
import { BusinessTableComponent } from './windo-pages/manager/manager-area/business-table/business-table.component';
import { AlphonUsersComponent } from './alphon-users/alphon-users.component';
import { HubComponent } from './windo-pages/content-pages/hub/hub.component';

const routes: Routes = [
  { path: "home", component: HomeComponent },
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'AccessibilityArrangements', component: AccessibilityArrangementsComponent },
  { path: 'forgot-password', component: ForgotPasswordComponent },
  { path: 'validate-code', component: ValidateCodeComponent },
  { path: 'myaccount', loadChildren: () => import(`./user/user.module`).then(m => m.UserModule) },
  { path: 'send-code', component: SendCodeComponent },
  { path: 'about-us', component: AboutUsComponent },
  { path: 'terms', component: TermsComponent },
  { path: 'contact-us', component: ContactUsComponent },
  { path: 'alphon-users', component: AlphonUsersComponent },
  { path: 'business-view/:userId', component: BusinessViewComponent },
  { path: 'case-study-form/:id', component: CaseStudyFormComponent },
  { path: 'create-cs/:id/:fromTable', component: CaseStudyFormComponent, canActivate: [AuthGuardService] },
  { path: 'case-study-list', component: CaseStudyListComponent },
  { path: 'case-study-list/:id', component: CaseStudyListComponent },
  { path: 'collaboration-list', component: CollaborationListComponent, canActivate: [AuthGuardService] },
  { path: 'notes', component: BoardComponent  },
  { path: 'note/:id', component: NoteComponent },
  { path: 'collaboration-report', component: ReportCollaborationFormComponent, canActivate: [AuthGuardService] },
  { path: 'independent', component: IndependentComponent },
  { path: 'hiring', component: HiringComponent },
  { path: 'hub', component: HubComponent },
  { path: 'vision', component: VisionComponent },
  { path: 'FQA', component: FQAComponent },
  {
    path: 'barter-List', component: BarterListComponent,
    children: [
      { path: '', component: ViewListComponent },
      { path: 'view-cards', component: ViewCardsComponent },
      { path: 'view-list', component: ViewListComponent },
    ]
  },
  { path: 'messages', component: MessagesComponent, canActivate: [AuthGuardService] },
  { path: 'message', component: MessageCardComponent, canActivate: [AuthGuardService] },
  { path: 'app-new-messag', component: NewMessagComponent },
  { path: 'chats', component: ChatsComponent },
  { path: 'advertisments-catalog', component: BanersCatalogComponent },
  { path: 'order-form', component: AdOrderFormComponent },
  { path: 'request-order-form/:makat', component: RequestOrderFormComponent },
  // manager area
  { path: 'clients', component: ClientsListComponent, canActivate: [AuthGuardService] },
  { path: 'orders-list/:clientId', component: OrderServeiceListComponent, canActivate: [AuthGuardService] },
  { path: 'favorites', component: FavoritesComponent },
  { path: 'profile', component: ProfileComponent },
  { path: 'search-domain', component: SearchDomainComponent },
  //{ path: 'register', component: RegistrationComponent },
  //{ path: 'login', component: LoginComponent },
  { path: 'Account', component: ConfirmEmailComponent },
  {path:'manager', component:ManagerAreaComponent,canActivate: [AuthGuardService]},
  {path:'services-manager-area', component:ServicesAreaComponent,canActivate: [AuthGuardService]},
  {path:'score-management-area', component:ScoreManagementAreaComponent,canActivate: [AuthGuardService]},
  {path:'networking-managment-area', component:NetworkingManagmentAreaComponent,canActivate: [AuthGuardService]},
  {path:'business-table', component:BusinessTableComponent,canActivate: [AuthGuardService]},
  {path:'networking-group-buisness-list', component:NetworkingGroupBuisnessListComponent,canActivate: [AuthGuardService]},
  {path:'order-service-requests-list', component:OrderServiceRequestsListComponent,canActivate: [AuthGuardService]},
  { path: '**', component: HomeComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  providers: [AuthGuardService],
  exports: [RouterModule]
})
export class AppRoutingModule { }
