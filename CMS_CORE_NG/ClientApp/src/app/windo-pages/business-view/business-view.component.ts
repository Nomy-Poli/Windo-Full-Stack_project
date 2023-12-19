import { AfterContentInit, AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from 'src/app/services/account.service';
import { BreadcrumbService } from 'src/app/services/breadcrumb.service';
import { BuisnessService, BuisnessVm, GuideModel, IBuisnessService } from 'src/app/services/Buisness.service';
import { CaseStudyForCardsVM, CollaborationsService } from 'src/app/services/Collaboration.service';
import { NetworkingGroupVM, NoteSearchParameters, NoteService, NoteVM } from 'src/app/services/Note.service';
import { ScoringService } from 'src/app/services/Scoring.service';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';
import { Location } from '@angular/common';
import { NetworkingService } from 'src/app/services/Networking.service';
@Component({
    selector: 'app-business-view',
    templateUrl: './business-view.component.html',
    styleUrls: ['./business-view.component.scss']
})
export class BusinessViewComponent implements OnInit, OnDestroy {
    personalInformationForm: FormGroup;
    ProfilePath: string = '../../../assets/windo-images/image.png';
    CoverForm: FormGroup;
    router: Router;
    userId: string;
    idS: string;
    isCollapsed = {
        categories: true,
        pictures: true,
        dealOptions: true
    };

    BusinnesToDisplay: BuisnessVm;
    openMoreSubCategoties = [3, 3, 3, 3];
    myLatestNotes: NoteVM[] = [];
    myLatestCS: CaseStudyForCardsVM[] = [];
    myGroups: NetworkingGroupVM[] = [];
    // IsBuisnnesEmpty: BuisnessVm
    public isViewDetails1 = false;
    public isViewDetailsB1 = true;
    public isViewDetails2 = false;
    public isViewDetailsB2 = true;
    public ListArea: string[] = [];
    public ListPossibleInBarter1: string[] = [];
    public ListPossibleInBarter2: string[] = [];
    public ListPossibleInBarter3: string[] = [];
    public ListPossibleInBarter4: string[] = [];
    workPictureGuide: GuideModel[] = [];

    public isOpen1 = true;
    public isOpen2 = true;
    public isOpen3 = true;
    constructor(
        public _wrapperSearchService: WrapperSearchService,
        public _funcService: WrapperFuncService,
        private fb: FormBuilder,
        private router1: ActivatedRoute,
        public _buisnessService: BuisnessService,
        private acct: AccountService,
        public _wrapperFuncService: WrapperFuncService,
        public breadcrumbService: BreadcrumbService,
        private _noteService: NoteService,
        private _networkingService: NetworkingService,
        private _collaborationService: CollaborationsService,
        private _scoringService: ScoringService,
        private location: Location
    ) {
        this.idS = this.router1.snapshot.paramMap.get('userId');
        breadcrumbService.setItem([
            { label: 'דף הבית', routerLink: ['/'] /*, icon: 'pi pi-home'*/ },
            { label: 'הצגת עסק', routerLink: ['/business-view/' + this.idS] }
        ]);
    }

    ngOnInit() {
        this.GetBusinessByEmailId(this.idS);
        // הסתרת כתובות המייל של המשתמש
        this.location.replaceState("/business-view/"+this.idS );
        window.scroll(0, 0);
        this.isCollapsed = {
            categories: false,
            pictures: false,
            dealOptions: false
        };
        this.acct.globalStateChanged.subscribe((state) => {
            this._wrapperSearchService.LoginStatus$.next(state.loggedInStatus);
            this._wrapperSearchService.LoginStatus$.subscribe;
        });
        this._wrapperSearchService.Username$ = this.acct.currentUserName;
        this._wrapperSearchService.HomePage$.next(false);
        this._wrapperSearchService.getAreaOptions();
        //  זימון הפונקציה שוסיפה ניקוד על פעולה
        this.acct.currentBusiness.subscribe((res) => {
            if (res != null) {
                this._scoringService.getScoreToBusiness(9, res.id).subscribe((res) => {
                    console.log('res', res);
                });
            }
        });

        
    }
    ngOnDestroy(): void {
        this._funcService.closeDialog();
    }

    GetBusinessByEmailId(email: string) {
        this._buisnessService.getBuisnessByEmailId(email, null).subscribe((res) => {
            if (res != null) {
                this.BusinnesToDisplay = res;
                this.GetMyLatestNotes(res.id);
                this.GetMyLatestCS(res.id);
                this.GetMyGroups(res.id);
                this.BusinnesToDisplay.buisnessAreaList1.forEach((area) =>
                    this.ListArea.push(this._wrapperSearchService.AreaOptions.find((areaN) => areaN.id === area.areaId).name)
                );
            }
        });
    }

    GetMyLatestNotes(businessId) {
        this._noteService.getBoardsWithNotes({ BusinessId: businessId, getMyNote: true, Latest: 3 } as NoteSearchParameters).subscribe((res) => {
            this.myLatestNotes = res;
        });
    }
    GetMyGroups(businessId) {
        this._networkingService.getAllGroupsForUser(businessId).subscribe((res) => {
            this.myGroups = res;
            console.log('my-groups:', this.myGroups);
        });
    }
    openNote(note) {
        this._wrapperFuncService.openNote(note);
    }
    GetMyLatestCS(businessId) {
        this._collaborationService.getCSByBuissinesID(businessId).subscribe((res) => {
            this.myLatestCS = res;
        });
    }
    GetAllPossibleInBarter(index: number) {
        switch (index) {
            case 1:
                this.BusinnesToDisplay.buisnessCategory1.forEach((subCategory) => {
                    if (subCategory.isPossibleInBarter == true) {
                        this.ListPossibleInBarter1.push(subCategory.subCategoryName);
                    }
                });
                return this.ListPossibleInBarter1.length;
            case 2:
                this.BusinnesToDisplay.buisnessCategory2.forEach((subCategory) => {
                    if (subCategory.isPossibleInBarter == true) this.ListPossibleInBarter2.push(subCategory.subCategoryName);
                });
                return this.ListPossibleInBarter2.length;
            case 3:
                this.BusinnesToDisplay.buisnessCategory3.forEach((subCategory) => {
                    if (subCategory.isPossibleInBarter == true) this.ListPossibleInBarter3.push(subCategory.subCategoryName);
                });
                return this.ListPossibleInBarter3.length;
            case 4:
                this.BusinnesToDisplay.buisnessCategory4.forEach((subCategory) => {
                    if (subCategory.isPossibleInBarter == true) this.ListPossibleInBarter4.push(subCategory.subCategoryName);
                });
                return this.ListPossibleInBarter4.length;
            default:
                break;
        }
    }
    moreSubCategories(i) {
        this.openMoreSubCategoties[i] = this.BusinnesToDisplay['buisnessCategory' + (i + 1)].length;
    }
    lessSubCategories(i) {
        this.openMoreSubCategoties[i] = 3;
    }
    openDetails() {
        let details = {
            Business: {
                id: this.BusinnesToDisplay.id,
                buisnessName: this.BusinnesToDisplay.buisnessName + ' ' + this.BusinnesToDisplay.ownerName,
                logoPictureId: this.BusinnesToDisplay.logoPictureId
            },
            phoneNumber1: this.BusinnesToDisplay.phoneNumber1,
            phoneNumber2: this.BusinnesToDisplay.phoneNumber2,
            buisnessWebSiteLink: this.BusinnesToDisplay.buisnessWebSiteLink
        };
        this._wrapperFuncService.openBusinessContactDetails(details);
    }
    ToggleAndChangeTheIcon(index: number, collapse: any) {
        switch (index) {
            case 1:
                this.isOpen1 = !this.isOpen1;
                collapse.toggle();
                break;
            case 2:
                this.isOpen2 = !this.isOpen2;
                collapse.toggle();
                break;
            case 3:
                this.isOpen3 = !this.isOpen3;
                collapse.toggle();
                break;
            default:
                break;
        }
    }
    growPic(pic) {
        let src = '../../../../../assets/BusinessImages/';
        if (pic) {
            src += this.BusinnesToDisplay.id + '/Work/' + pic.picindex + '/' + pic.workPicGuide + '/' + pic.workPicGuide;
        } else {
            src += this.BusinnesToDisplay.id + '/Logo/' + this.BusinnesToDisplay.logoPictureId + '/' + this.BusinnesToDisplay.logoPictureId;
        }
        src += '.jpg';
        this._wrapperFuncService.openPictureDialog(src);
    }
    getFontSizeByLenght() {
        if (!this.BusinnesToDisplay.buisnessName) {
            return '3rem';
        } else {
            let buisnessName = this.BusinnesToDisplay.buisnessName.split(' ');
            let longWord = buisnessName.find((x) => x.length > 10);
            if (longWord) {
                if (longWord.length > 25) return '1.3rem';
                if (longWord.length > 23) return '1.4rem';
                if (longWord.length > 21) return '1.5rem';
                if (longWord.length > 20) return '1.7rem';
                if (longWord.length > 18) return '1.8rem';
                if (longWord.length > 16) return '1.9trem';
                if (longWord.length > 14) return '2.25rem';
                if (longWord.length > 12) return '2.4rem';
                if (longWord.length > 10) return '2.5rem';
            }
            return '3rem';
        }
    }
    close() {
        this._funcService.closeDialog();
    }
    // sendMessage(){
    //   this._funcService.openNewMassage(this.BusinnesToDisplay.id.toString());
    // }
    sendEmail(email) {
        const subject = 'הגעתי מאתר תמך ואשמח למידע על...';
        const body = 'תוכן למייל שלך כאן';
      
        const gmailUrl = `https://mail.google.com/mail/u/0/?view=cm&fs=1&to=${encodeURIComponent(email)}&su=${encodeURIComponent(subject)}&body=${encodeURIComponent(body)}`;
        window.open(gmailUrl, '_blank');
      }
      
      
    // sendEmail(email) {
    //     console.log('email', email);
      
    //     const subject = 'Your Subject Here'; // ניתן להגדיר כותרת למייל
    //     const body = 'Your email body here'; // ניתן להגדיר גוף למייל
      
    //     const gmailUrl = 'https://mail.google.com/mail/u/0/?view=cm&fs=1&to=${email}&su=הגעתי מאתר תמך ואשמח למידע על ...';
    //     // const mailtoUrl = `mailto:${email}?subject=${encodeURIComponent(subject)}&body=${encodeURIComponent(body)}`;
      
    //     window.location.href = gmailUrl;
    //   }
    //   public openEmailInGmail(): void {
    //     const gmailUrl = 'https://mail.google.com/mail/u/0/?view=cm&fs=1&to=rci@temech.org&su=הגעתי מאתר תמך ואשמח למידע על ...';
    //     window.open(gmailUrl, '_blank');
    // }
}
