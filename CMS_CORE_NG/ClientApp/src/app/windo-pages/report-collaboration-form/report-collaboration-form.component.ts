import { Component, Inject, OnInit, Optional } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { BarterDealVM, BuisnessService, BuisnessVm, BusinessNamesPicturesVM, CollaborationTypeVM, JointProjectVM, PaidTransactionVM } from 'src/app/services/Buisness.service';
import { AccountService } from 'src/app/services/account.service';
import { WrapperCollaborationsService } from 'src/app/services/wrapper-collaborations.service';
import { HttpClient } from '@angular/common/http';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';
import { BreadcrumbService } from 'src/app/services/breadcrumb.service';

@Component({
    selector: 'app-report-collaboration-form',
    templateUrl: './report-collaboration-form.component.html',
    styleUrls: ['./report-collaboration-form.component.scss']
})
export class ReportCollaborationFormComponent implements OnInit {
    constructor(private _businessService: BuisnessService,
        private _acct: AccountService,
        private httpClient: HttpClient,
        public _wrapperSearchService: WrapperSearchService,
        private _collaborationService: WrapperCollaborationsService,
        public _wrapperFuncService: WrapperFuncService,
        public breadcrumbService: BreadcrumbService,
        @Optional() @Inject('API_BASE_URL') apiBaseUrl?: string,
        ) {
            breadcrumbService.setItem([
                { label: 'דף הבית', routerLink: ['/']/*, icon: 'pi pi-home'*/},
                { label: 'דיווח על שת"פ', routerLink: ['/collaboration-report'] }
            ]);
            this.apiBaseUrl = apiBaseUrl ? apiBaseUrl : "";
    }

    activeIndex = 1;
    dealType: 1 | 2 | 3;
    paidTrnsaction: PaidTransactionVM;
    reporterBusiness: BuisnessVm;
    businessInCollaboration: BusinessNamesPicturesVM[];
    partnerBusiness: BusinessNamesPicturesVM;
    barterDeal: BarterDealVM; //לשים כאן מודלים אמיתיים ולאתחל ב new
    JointProject: JointProjectVM;

    public apiBaseUrl: string;

    ngOnInit() {
        window.scrollTo(0,0);
        this._businessService.getBuisnessByEmailId(this._acct.Email.value,null).subscribe((res) => {
            if (res != null) {
                this.reporterBusiness = res;
            }
        });
    }

    choosed(event) {
        this.dealType = event;
        if (this.dealType == 1)
            this.activeIndex += 1;
        else {
            this._wrapperFuncService.openDialogconfirmPartner(this.dealType)
                .subscribe(res => { 
                    console.log(res);
                    this.activeIndex += 1 
                    this._wrapperFuncService.closeDialog();
                });

        }
    }

    dealDetailsSubmited(event) {
        switch (this.dealType) {
            case 1:
                {
                    this._collaborationService.paidTrnsactionModel = event;
                    this._collaborationService.paidTrnsactionModel.SupplierBusinessId = this.reporterBusiness.id;
                    delete event['partnerBusiness'];
                }
                break;
            case 2:
                this.partnerBusiness = event['partnerBusiness'];
                this._collaborationService.barterDealModel = event;
                this._collaborationService.barterDealModel.Id = 0;
                this._collaborationService.barterDealModel.ReportsBusinessId = this.reporterBusiness.id;
                this._collaborationService.barterDealModel.ReportDate = new Date();
                this._collaborationService.barterDealModel.ConfirmedByPartner = true;
                break;
            case 3:
                this.JointProject = event;
                this.JointProject.CollaborationTypeId = this._collaborationService.collaborationType.Id?this._collaborationService.collaborationType.Id:this._collaborationService.collaborationType['id'];
            default:
                break;
        }
        this.activeIndex += 1;
    }

    dealMoreDetailsSubmited(event) {
        switch (this.dealType) {
            case 1:
                {
                    this._collaborationService.paidTrnsactionModel.ScopTransactionNIS = event;
                }
                break;
            case 2: {
                this._collaborationService.barterDealModel.QuoteReportsBusiness = event.QuoteReportsBusiness;
                this._collaborationService.barterDealModel.QuotePartnerBusiness = event.QuotePartnerBusiness;
                this._collaborationService.barterDealModel.JointExplanation = event.JointExplanation;

            }
            case 3:
                console.log(this.JointProject);
                
            default:
                break;
        }
        this.activeIndex += 1;
    }

    feedbackSubmited(event) {
        switch (this.dealType) {
            case 1:
                this._collaborationService.paidTrnsactionModel.Professionalism = event.Professionalism;
                this._collaborationService.paidTrnsactionModel.Availability = event.Availability;
                this._collaborationService.paidTrnsactionModel.Service = event.Service;
                this._collaborationService.paidTrnsactionModel.Price = event.Price;
                this._collaborationService.paidTrnsactionModel.Review = event.Review;
                this.createPaidTransactionWithPicture();
                break;
            case 2:
                this._collaborationService.barterDealModel.MoreLeisure = event.MoreLeisure;
                this._collaborationService.barterDealModel.MoreShopping = event.MoreShopping;
                this._collaborationService.barterDealModel.IncreasingRevenue = event.IncreasingRevenue;
                this._collaborationService.barterDealModel.ReducingExpenses = event.ReducingExpenses;
                this._collaborationService.barterDealModel.ReducingEffort = event.ReducingEffort;
                this._collaborationService.barterDealModel.ConfirmedByPartner = true;
                this.createBarterDealWithPictures();
                break;
            case 3:
                this.JointProject.MoreLeisure = event.MoreLeisure;
                this.JointProject.MoreShopping = event.MoreShopping;
                this.JointProject.IncreasingRevenue = event.IncreasingRevenue;
                this.JointProject.ReducingExpenses = event.ReducingExpenses;
                this.JointProject.ReducingEffort = event.ReducingEffort;
                this.JointProject.ConfirmedByPartners = true;
                this.createJoinProjectWithPictures();
                break;
            default:
                break;
        }
        this.activeIndex += 1;

    }

    moreCollaboration() {
        this.activeIndex = 1;
    }

    savePaidTransaction(value) {
        this._businessService.createPaidTransaction(value).subscribe((res) => {
            this.paidTrnsaction.Id = res;
            console.log(res);
        });
    }

    createPaidTransactionWithPicture() {
        const formData = new FormData();
        formData.append('PaidTransactionPicture', this._collaborationService.paidTrnsactionPicture);
        formData.append('model', JSON.stringify(this._collaborationService.paidTrnsactionModel));
        return this.httpClient.post(this.apiBaseUrl + `/api/Buisness/createPaidTransactionWithPicture`, formData).subscribe(res => {
            console.log(res);

        });
    }

    createBarterDealWithPictures() {
        const formData = new FormData();
        formData.append('reportPicture', this._collaborationService.barterDealPictures[0]);
        formData.append('partnerPicture', this._collaborationService.barterDealPictures[1]);
        formData.append('model', JSON.stringify(this._collaborationService.barterDealModel));
        return this.httpClient.post(this.apiBaseUrl + `/api/Buisness/CreateBarterDealWithPictures`, formData).subscribe(res => {
            console.log(res);
        });
    }

    createJoinProjectWithPictures() {
        const formData = new FormData();
        formData.append('Picture', this._collaborationService.JoinProjectPicture);
        formData.append('model', JSON.stringify(this.JointProject));
        return this.httpClient.post(this.apiBaseUrl + `/api/Buisness/createJoinProjectWithPictures`, formData).subscribe(res => {
            console.log(res);
        });
    }

}
