import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit, Optional } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from 'src/app/services/account.service';
import { BreadcrumbService } from 'src/app/services/breadcrumb.service';
import { GuideModel } from 'src/app/services/Buisness.service';
import { BusinessInCaseStudyVM, CaseStudyCustomerResponsesVM, CaseStudyPictureVM, CaseStudyVM, CollaborationsService, FromTable } from 'src/app/services/Collaboration.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';
import Swal from 'sweetalert2';

import { ValidateCountWordMax10, ValidateCountWordMax200, ValidateCountWordMax50, ValidateCountWordMax70 } from './count-words.validator';
import {MessageService as ToastService} from 'primeng/api';
import { ScoringService } from 'src/app/services/Scoring.service';

@Component({
  selector: 'app-case-study-form',
  templateUrl: './case-study-form.component.html',
  styleUrls: ['./case-study-form.component.scss']
})
export class CaseStudyFormComponent implements OnInit {
  id: string = this._activeRouter.snapshot.paramMap.get('id');
  constructor(private _collaborationService: CollaborationsService,
    private _httpClient: HttpClient,
    private _activeRouter: ActivatedRoute,
    public breadcrumbService: BreadcrumbService,
    public _wrapperSearchService: WrapperSearchService,
    public _acct: AccountService,
    public _scoringService : ScoringService,
    public _toast: ToastService,
    @Optional() @Inject('API_BASE_URL') apiBaseUrl?: string,
  ) {
    this._acct.globalStateChanged.subscribe((state) => {
      this._wrapperSearchService.LoginStatus$.next(state.loggedInStatus);
    });
    this._wrapperSearchService.Username$ = this._acct.currentUserName;
    this._wrapperSearchService.HomePage$.next(false);
    breadcrumbService.setItem([
      { label: 'דף הבית', routerLink: ['/']/*, icon: 'pi pi-home'*/ },
      { label: 'קייס סטאדי', routerLink: ['/case-study-form' + this.id] }
    ]);
    this.apiBaseUrl = apiBaseUrl ? apiBaseUrl : "";

  }
  public apiBaseUrl: string;
  MAX_SIZE: number = 307200;
endPartner:boolean= true;
  pictureSrc = "../../../../../assets/CaseStudy/";
  backgroundPictureSrc = "";
  //C:\TFS\busoftBase\CMS_CORE_NG\ClientApp\src\assets\icons
  numOfPartners =0 ;
  activePartner = 0;
  activePicture = 0;
  activeResp = 0;
  caseStudy: CaseStudyVM;
  fromTableStrings = {
    1: 'עסקה בתשלום',
    2: 'ברטר',
    3: 'מיזם משותף',
  }
  fromTableNumbers = {
    PaidTransaction: 1,
    BarterDeal: 2,
    JointProject: 3
  }
  pictures = [];
  hasImage = [false, false, false, false, false, false];
  picturesFiles = [null, null, null, null, null, null];
  mainPictureFile = null;
  editable = false;
  caseStudyForm: FormGroup;
  BusinessesInCaseStudy: FormArray;
  errors = {};
  mainPictureSrc = '';
  mainPictureData: CaseStudyPictureVM;
  ngOnInit(): void {
    window.scrollTo(0, 0);
    const fromTable = this._activeRouter.snapshot.paramMap.get('fromTable');
    if (!fromTable) {
      //הצגת קייס סטדי קיים לפי ID
      this.getCaseStudy(this.id);
    }
    else {

      this._collaborationService.getCaseStudyByCollaborationId({ Id: parseInt(this.id), FromTable: this.fromTableNumbers[fromTable] as FromTable }).subscribe(res => {
        this.createCaseStudyForm(res);
      });
    }
    //  זימון הפונקציה שוסיפה ניקוד על פעולה
    this._acct.currentBusiness.subscribe(res => {
      if(res!=null)
      {
        this._scoringService.getScoreToBusiness(11,res.id).subscribe(res=>{
          console.log("res",res); 
        });
      }
    })
  }

  getCaseStudy(id) {
    this._collaborationService.getCaseStudyById(parseInt(id)).subscribe((res: CaseStudyVM) => {
      if (res != null) {
        this.caseStudy = res;
        console.log('getCaseStudyById', res);
        this.editable = false;
        this.numOfPartners = this.caseStudy.BusinessesInCaseStudy.length;
        console.log("length",this.numOfPartners);
        
      }
    });
   
  }
  edit() {
    this.createCaseStudyForm(this.caseStudy);
  }
  //#region create form
  createCaseStudyForm(res) {
    this.picturesFiles = [null, null, null, null, null, null];;
    this.mainPictureFile = null;
    this.activePartner = 0;
    this.activePicture = 0;
    this.activeResp = 0;
    this.pictures = [];
    this.hasImage = [ false, false, false, false, false, false ];
    if (res.Id == 0) {
      this.putPictureForNewCS(res);
    }
    else {
      this.putSrcInPictures(res.CaseStudyPictures);
    }

    console.log('getCaseStudyByCollaborationId', res);
    this.caseStudy = res;
    this.caseStudyForm = new FormGroup({
      BusinessTitle: new FormControl(this.caseStudy.BusinessTitle, ValidateCountWordMax10),
      MarketingTitle: new FormControl(this.caseStudy.MarketingTitle, ValidateCountWordMax10),
      Description: new FormControl(this.caseStudy.Description, ValidateCountWordMax200),
      Challenge: new FormControl(this.caseStudy.Challenge, ValidateCountWordMax200),
      PowerMultiplier: new FormControl(this.caseStudy.PowerMultiplier, ValidateCountWordMax200),
      CustomersGain: new FormControl(this.caseStudy.CustomersGain, ValidateCountWordMax200),
      CustomerResponses: new FormArray(this.createResponesesForm(this.caseStudy.CustomerResponses)),
      BusinessesInCaseStudy: new FormArray(this.createBusinessesForm(this.caseStudy.BusinessesInCaseStudy))
    });
    console.log('form', this.caseStudyForm);
    this.editable = true;
  }
  createBusinessesForm(BusinessesInCaseStudy: BusinessInCaseStudyVM[]) {
    let formgroups = []
    BusinessesInCaseStudy.forEach(business => {
      formgroups.push(new FormGroup({
        BuinessOwnerNameForCS: new FormControl(business.BuinessOwnerNameForCS, ValidateCountWordMax10),
        LineOfBusiness: new FormControl(business.LineOfBusiness, ValidateCountWordMax10),
        WordOfPartner: new FormControl(business.WordOfPartner, ValidateCountWordMax70),
        BusinessId: new FormControl(business.BusinessId),
        Id: new FormControl(business.Id)
      }));
    });

    return formgroups;
  }
  createResponesesForm(Responses: CaseStudyCustomerResponsesVM[]) {
    let formgroups = [];
    if (Responses == null || Responses.length == 0) {
      formgroups.push(this.newResponseForm());
    }
    else {
      Responses.forEach(response => {
        formgroups.push(new FormGroup({
          CustomerName: new FormControl(response.CustomerName, ValidateCountWordMax10),
          MinimalDescription: new FormControl(response.MinimalDescription, ValidateCountWordMax10),
          Response: new FormControl(response.Response, ValidateCountWordMax50),
          Id: new FormControl(response.Id),
          CaseStudyId: new FormControl(response.CaseStudyId)
        }));
      });
    }
    return formgroups;
  }

  newResponseForm() {
    return new FormGroup({
      CustomerName: new FormControl('', ValidateCountWordMax10),
      MinimalDescription: new FormControl('', ValidateCountWordMax10),
      Response: new FormControl('', ValidateCountWordMax50),
      Id: new FormControl(0),
      CaseStudyId: new FormControl(this.caseStudy.Id)
    });
  }
  //#region pitures
  putPictureForNewCS(cs) {
    switch (cs.FromTable) {
      case FromTable.PaidTransaction:
        if (cs.PaidTransaction.PictureID && cs.PaidTransaction.PictureID != '00000000-0000-0000-0000-000000000000') {
          this.mainPictureSrc = '../../../../../assets/Collaborations/PaidTransactions/' + cs.PaidTransactionID + '/' + cs.PaidTransaction.PictureID + '.jpg';
        }
        break;
      case FromTable.BarterDeal:
        if (cs.BarterDeal.ReportsBusinessPictureID && cs.BarterDeal.ReportsBusinessPictureID != '00000000-0000-0000-0000-000000000000')
          this.mainPictureSrc = '../../../../../assets/Collaborations/BarterDeal/' + cs.BarterDealID + '/' + cs.BarterDeal.ReportsBusinessId + '/' + cs.BarterDeal.ReportsBusinessPictureID + '.jpg';
        if (cs.BarterDeal.PartnerBusinessPictureID)
          $('#img0').attr('src', '../../../../../assets/Collaborations/BarterDeal/' + cs.BarterDealID + '/' + cs.BarterDeal.PartnerBusinessId + '/' + cs.BarterDeal.PartnerBusinessPictureID + '.jpg');
        break;
      case FromTable.JointProject:
        if (cs.JointProject.PictureId && cs.JointProject.PictureId != '00000000-0000-0000-0000-000000000000')
          this.mainPictureSrc = '../../../../../assets/Collaborations/JointProject/' + cs.JointProjectID + '/' + cs.JointProject.PictureId + '.jpg';
        break;
      default:
        break;
    }
  }

  putSrcInPictures(caseStudyPictures: CaseStudyPictureVM[]) {
    if (caseStudyPictures != null && caseStudyPictures.length > 0) {
      caseStudyPictures.forEach((element, index) => {
        if (element.PicGuid != '00000000-0000-0000-0000-000000000000') {
          this.hasImage[index] = true;
          setTimeout(() => {
            let src = this.pictureSrc + element.CaseStudyId + '/' + element.PicGuid + '.jpg';
            $('#img' + index).attr('src', src);
          }, 100);
        }
      });
    }
  }

  filesDropped(event, index) {  
    console.log(event[0].file.type);
    if(event[0].file.type=='image/svg+xml' || event[0].file.type=='image/png'|| event[0].file.type=='image/jpeg'){
    if (event[0].file.size < this.MAX_SIZE){
      let reader = new FileReader();
      let file = event[0].file;
      if (index === '') {
        this.mainPictureFile = file;
      }
      else {
        this.picturesFiles[index] = file;
        this.hasImage[index] = true;
        this.errors['pics'] = null;
      }
      reader.readAsDataURL(file);
      reader.onload = () => {
        if (index === '') {
          this.mainPictureSrc = reader.result as string;
        }
        else {
          $('#img' + index).attr('src', reader.result as string);
        }

      };
    }
    else {
        this._toast.add({severity:'error', detail: 'יש להעלות קובץ ששוקל עד 300KB'})
    }
    
  }
  else{
    this._toast.add({severity:'error', detail: 'יש לעלות רק קבצים מסוג JEPG או PNG '})
  }
 }
 
  onWorkFileChanged(event, index) {
    if (event.target.files && event.target.files[0]) {
      if (event.target.files[0].size < this.MAX_SIZE) {
        let reader = new FileReader();
        let file = event.target.files[0]
        if (index === '') {
          this.mainPictureFile = file;
        }
        else {
          this.picturesFiles[index] = file;
          this.hasImage[index] = true;
          this.errors['pics'] = null;
        }
        reader.readAsDataURL(file);
        reader.onload = () => {
          if (index === '') {
            this.mainPictureSrc = reader.result as string;
          }
          else {
            $('#img' + index).attr('src', reader.result as string);
          }

        };
      }
      else {
          this._toast.add({severity:'error', detail: 'יש להעלות קובץ ששוקל עד 300KB'})
      }
    }

  }
  triggerWorkInput(index) {
    $('#workpicfile' + index).trigger('click');
  }
  removePic(index: number) {
    $('#img' + index).attr('src', '');
    this.hasImage[index] = false;
    if (this.caseStudy.Id > 0 && this.caseStudy.CaseStudyPictures[index]) {
      this.caseStudy.CaseStudyPictures.splice(index, 1);
    }
  }
  //#endregion
  //#endregion
  //#region prev next
  prevPartner() {
    if (this.activePartner > 0) {
      this.activePartner--;
    }
  }
  nextPartner() {
    if (this.activePartner < (this.caseStudy.BusinessesInCaseStudy.length - 1)) {
    
      this.activePartner++;
      console.log("mmm",this.activePartner);
      console.log("lllll",this.numOfPartners);
      
      
    }
  }
  prevPic() {
    if (this.activePicture > 0) {
      this.activePicture--;
    }
  }

  nextPic() {
    if (this.activePicture < (this.caseStudy.CaseStudyPictures.length - 1)) {
      this.activePicture++;
    }
  }
  //#endregion

  //#region responses
  get Responses(): FormArray {
    return this.caseStudyForm.get('CustomerResponses') as FormArray;
  }
  addResponse() {
    this.Responses.push(this.newResponseForm());
  }

  removeResponse(i: number) {
    this.Responses.removeAt(i);
  }

  checkResponeses() {
    let arrValue = this.Responses.getRawValue()
    console.log('this.Responses.getRawValue()',);

  }
  //#endregion


  onSubmitModel() {
    console.log(this.caseStudyForm.value);
    if (this.caseStudyForm.valid) {
      this.checkResponeses();
      this.createCS();
    }
  }
  createCS() {

    let formValue: CaseStudyVM = this.caseStudyForm.value;
    formValue.CustomerResponses = formValue.CustomerResponses.filter(x => x.Response);
    formValue.Id = this.caseStudy.Id;
    formValue.FromTable = this.caseStudy.FromTable;
    formValue.PaidTransactionID = this.caseStudy.PaidTransactionID;
    formValue.BarterDealID = this.caseStudy.BarterDealID;
    formValue.JointProjectID = this.caseStudy.JointProjectID;
    formValue.PicGuid = this.caseStudy.PicGuid? this.caseStudy.PicGuid: '00000000-0000-0000-0000-000000000000';
    if (this.caseStudy.Id > 0) {
      formValue.CaseStudyPictures = this.caseStudy.CaseStudyPictures;
    }
    const formData = new FormData();
    formData.append('model', JSON.stringify(formValue));
    if (this.picturesFiles && this.picturesFiles.length) {
      this.picturesFiles.forEach(pic => {
        formData.append('files', pic);
      });
    }
    if (this.mainPictureFile) {
      formData.append('mainPicture', this.mainPictureFile);
    }

    if (this.caseStudy.Id == 0) {

      return this._httpClient.post(this.apiBaseUrl + `/api/Collaborations/createCaseStudyWithPictures`, formData,
        {
          headers: { 'Content-Type': [] }, reportProgress: true
        }).subscribe((res: number) => {
          console.log(res);
          this.caseStudy.Id = res;
          this.activePartner = 0;
          this.activePicture = 0;
          this.mainPictureFile = null;
          this.mainPictureSrc = '';
          Swal.fire({
            icon: 'success',
            title: 'יש לנו קייס!',
            html:
              'כרטיס הקייס סטדי נשמר במאגר. עכשיו כל בעלת עסק יכולה ללמוד מסיפור ההצלחה, להתרשם מהשירותים שאת מציעה ולדעת כל מה שבחרת לספר.',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonText: 'יופי, תודה!',
            confirmButtonText: 'אני רוצה לראות את כרטיס הקייס סטדי'
          }).then((result) => {
            if (result.isConfirmed) {
              this.getCaseStudy(res);
            }
          })
        }, err => {
          Swal.fire({
            icon: 'error',
            title: 'הייתה בעיה ביצירת הקייס סטדי',
            html:
              'אנא בדוק שנית שכל הפרטים נכונים',
            showClass: {
              popup:
                'אנא בדוק שוב'
            }
          });
        });
    }
    else if (this.caseStudy.Id > 0) {

      return this._httpClient.post(this.apiBaseUrl + `/api/Collaborations/updateCaseStudy`, formData,
        {
          headers: { 'Content-Type': [] }, reportProgress: true
        }).subscribe(res => {
          console.log('updateCaseStudy', res);
          Swal.fire({
            icon: 'success',
            title: 'כרטיס הקייס סטדי עודכן!',
            html:
              'מי שתצפה בו תקבל את כל המידע שבחרת להציג',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonText: 'יופי, תודה!',
            confirmButtonText: 'אני רוצה לראות את הכרטיס המעודכן',


          }).then((result) => {
            if (result.isConfirmed) {
              this.picturesFiles = null;
              this.mainPictureFile = null;
              this.getCaseStudy(this.caseStudy.Id);
            }
          });

        }, err => {
          Swal.fire({
            icon: 'error',
            title: 'הייתה בעיה בעדכון הקייס סטדי',
            html:
              'אנא בדוק שנית שכל הפרטים נכונים',
            showClass: {
              popup:
                'אנא בדוק שוב'
            }
          })
        });
    }
  }

}
