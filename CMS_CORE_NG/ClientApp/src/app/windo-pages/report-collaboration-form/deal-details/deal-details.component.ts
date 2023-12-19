import { Component, EventEmitter, Input, OnInit, Output, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from 'src/app/services/account.service';
import { BarterDealVM, BuisnessService, BuisnessVm, BusinessNamesPicturesVM, GuideModel, IBuisnessService, JointProjectVM, PaidTransactionVM } from 'src/app/services/Buisness.service';
import { WrapperCollaborationsService } from 'src/app/services/wrapper-collaborations.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';
import { ValidateArrLenght2 } from './arr-lenght.validator';
@Component({
  selector: 'app-deal-details',
  templateUrl: './deal-details.component.html',
  styleUrls: ['./deal-details.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class DealDetailsComponent implements OnInit {

  constructor(private _fb: FormBuilder,
    public _collaborationService: WrapperCollaborationsService,
    private _buisnessService: BuisnessService,
    public _wrapperSearchService: WrapperSearchService,
  ) { }

  @Output() submited = new EventEmitter();
  @Input() dealType;
  @Input() reporterBusiness: BuisnessVm;
  @Input() CollaborationType: string;
  businessList: BusinessNamesPicturesVM[];
  businessListAfterSearch: BusinessNamesPicturesVM[] = [];
  SupplierBusiness: BusinessNamesPicturesVM;
  SubCategories = [];
  SubCategoriesAfterSearch = [];
  SubCategoriesAfterSearch1 = [];
  SubCategoriesAfterSearch2 = [];
  partnerBusiness: BusinessNamesPicturesVM;
  showError = false;
  errorMessage = '';
  dForm: FormGroup;


  ngOnInit(): void {
    switch (this.dealType) {
      case 1:
        this.dForm = this._fb.group({
          ConsumerBusinessId: new FormControl('', Validators.required),
          CategorySubCategoryId: new FormControl('', Validators.required),
          Description: new FormControl('', [Validators.maxLength(60)]),
        });
        break;
      case 2:
        this.dForm = this._fb.group({
          PartnerBusinessId: new FormControl('', Validators.required),//מי העסק השותף
          ReportCategorySubCategoryId: new FormControl('', Validators.required),//קטגוריה עסק מדווח
          PartnerCategorySubCategoryId: new FormControl('', Validators.required),//קטגוריה עסק פרטנר
          ReportDescriptionDeal: new FormControl(''),//תאור השירות עסק 1
          PartnerDescriptionDeal: new FormControl(''),//תאור השירות עסק 2
          BusinessDescription: new FormControl('', Validators.required),//תאור העסקה
        });
        break;
      case 3:
        this.dForm = this._fb.group({
          BusinessInCollaborationIds: new FormControl([],[ Validators.required,ValidateArrLenght2]),// העסקים השותפים
          HeaderCollaboration: new FormControl('', Validators.required),//כותרת שת"פ
          JointExplanation: new FormControl('', Validators.required),//הסבר משותף על העיסקה
        });
        this.dropdownBusinessesSettings.singleSelection = false;
        this.dropdownBusinessesSettings['itemsShowLimit']= 3;

        break;
      default:
        break;
    }
    this.dForm.valueChanges.subscribe(value => {
      if (this.showError)
        this.validForm();
    });
   

    this.getBusinesList();
    this.getCategoriesAndSubCategory();
  }

  getBusinesList() {
    this._buisnessService.getBusinessNamesPictures().subscribe(res => {
      console.log(res);
      this.businessList = res;//
      if (this.dealType < 3) {
        this.businessList = res.filter(x => x.id != this.reporterBusiness.id);
      }
    });
  }

  getCategoriesAndSubCategory() {
    this._wrapperSearchService._listOfSuggestionSubject.subscribe(res => {
      this.SubCategories = res;
    });
  }
  dropdownBusinessesSettings = {
    singleSelection: true,
    idField: 'id',
    textField: 'buisnessName',
    searchPlaceholderText:'התחילי להקליד את שם העסק',
    noDataAvailablePlaceholderText:'לא נמצאו תוצאות',
    enableCheckAll:false,
    allowSearchFilter: true,
    closeDropDownOnSelection:true
  }
  dropdownCategoriessSettings = {
    singleSelection: true,
    idField: 'id',
    textField: 'subCategoryName',
    searchPlaceholderText:'התחילי להקליד תחום או תת תחום שירות',
    noDataAvailablePlaceholderText:'לא נמצאו תוצאות',
    enableCheckAll:false,
    allowSearchFilter: true,
    closeDropDownOnSelection:true
  }
  get getBusiness() {
    return this.businessList.reduce((acc, curr) => {
      acc[curr.id] = curr;
      return acc;
    }, {});
  }
  get getCategorySubCategory() {
    return this.SubCategories.reduce((acc, curr) => {
      acc[curr.id] = curr;
      return acc;
    }, {});
  }
  searchBusiness(event) {
    if (event.query == '') {
      this.businessListAfterSearch = this.businessList;
    }
    else {
      if (this.dealType < 3) {
        this.businessListAfterSearch = this.businessList.filter(x => x.buisnessName.toLowerCase().includes(event.query.toLowerCase()));
      }
      else {
        this.businessListAfterSearch = this.businessList.filter(x => x.buisnessName.toLowerCase().includes(event.query.toLowerCase())
          && !this.dForm.get('BusinessInCollaborationIds').value.includes(x.id));
      }
    }
  }

  onSelectBusiness(event) {
    this.partnerBusiness = this.businessList.find(x=>x.id == event.id);
  }

  onRemovedBusiness(event) {
    let arr: number[] = this.dForm.controls['BusinessInCollaborationIds'].value;
    arr = arr.filter(x => x != event.id);
    this.dForm.controls['BusinessInCollaborationIds'].setValue(arr);
  }
  searchCategory(event) {
    if (event.query == '') {
      this.SubCategoriesAfterSearch = this.SubCategories;
    }
    else {
      this.SubCategoriesAfterSearch = this.SubCategories.filter(x =>
        x.categoryName.includes(event.query) || x.subCategoryName.includes(event.query));
    }
    return this.SubCategoriesAfterSearch;

  }
  //לא בשלב זה לבחירה מרובה של קטגוריות
  searchCategories1(event) {//השרותים שסופקו עסק 1
    if (event.query == '') {
      this.SubCategoriesAfterSearch1 = this.SubCategories;//.filter(x=> !this.dForm.controls['reportCategorySubCategoryId'].value.includes(x.id));
    }
    else {
      this.SubCategoriesAfterSearch = this.SubCategories.filter(x =>
        x.categoryName.includes(event.query) || x.subCategoryName.includes(event.query));
      // && !this.dForm.controls['reportCategorySubCategoryId'].value.includes(x.id));
    }
  }
  searchCategories2(event) {///השרותים שסופקו עסק 2
    if (event.query == '') {
      this.SubCategoriesAfterSearch2 = this.SubCategories//.filter(x=> !this.dForm.controls['partnerCategorySubCategoryId'].value.includes(x.id));
    }
    else {
      this.SubCategoriesAfterSearch2 = this.SubCategories.filter(x =>
        x.categoryName.includes(event.query) || x.subCategoryName.includes(event.query));
      //&& !this.dForm.controls['partnerCategorySubCategoryId'].value.includes(x.id));
    }
  }

  onSelectCategory(event) {
    this.dForm.controls['CategorySubCategoryId'].setValue(event.id);
  }
  //לבחירה מרובה של קטגוריות
  onSelectCategories1(event) {//השרותים שסופקו עסק 1
    //  let arr = this.dForm.controls['reportCategorySubCategoryId'].value;
    //  arr.push(event.id);
    this.dForm.controls['ReportCategorySubCategoryId'].setValue(event.id);
  }

  onSelectCategories2(event) {//השרותים שסופקו עסק 2
    // let arr = this.dForm.controls['partnerCategorySubCategoryId'].value;
    // arr.push(event.id);
    this.dForm.controls['PartnerCategorySubCategoryId'].setValue(event.id);
  }

  validForm() {
    if (this.dForm.valid) {
      this.showError = false;
      this.errorMessage = '';
      return true;
    }
    else {
      switch (this.dealType) {
        case 1:
          if (this.dForm.get('Description').value.length < 4) {
            this.errorMessage = 'בשדה תיאור העסקה חובה להכניס לפחות 4 אותיות';
          }
          if (this.dForm.get('Description').value.length > 60) {
            this.errorMessage = 'בשדה תיאור העסקה ניתן להכניס עד 60 אותיות';
          }
          if (this.dForm.controls['CategorySubCategoryId'].value == '') {
            this.errorMessage = 'לא סיפרת לנו איזה שירות קיבלת';
          }
          if (this.dForm.controls['ConsumerBusinessId'].value == '') {
            this.errorMessage = 'חסר לנו פרט בסיסי – ממי קיבלת את השירות?';
          }
          break;
        case 2:
          if(this.dForm.controls['BusinessDescription'].value == ''){
            this.errorMessage = 'עדיין לא סיפרת לנו מה בעצם היה שם...'
          } 
          if (this.dForm.controls['ReportCategorySubCategoryId'].value == '' || this.dForm.controls['PartnerCategorySubCategoryId'].value == '' ) {
            this.errorMessage = 'חסר מידע על השירותים שסופקו';
          }
          if (this.dForm.controls['PartnerBusinessId'].value == '') {
            this.errorMessage = 'חסר לנו פרט בסיסי – מי הייתה הפרטנרית שלך';
          }
          break;
        case 3:
          if (this.dForm.controls['JointExplanation'].value == '') {
            this.errorMessage = 'עדיין לא סיפרת לנו מה בעצם היה שם...';
          }
          if (this.dForm.controls['HeaderCollaboration'].value == '') {
            this.errorMessage = 'כדי להציג את השת"פ שלך, אנחנו זקוקים לכותרת';
          }
          if (this.dForm.controls['BusinessInCollaborationIds'].value.length <=1) {
            this.errorMessage = 'חסר לנו מידע בסיסי – מי היו השותפות שלך?';
          }
          break;
        default:
          break;
      }
      this.showError = true;
      return false;
    }
  }

  next() {
    if (this.validForm()) {
      let formValue = this.dForm.value;
      let obj = {};
      switch (this.dealType) {
        case 1:
          obj['partnerBusiness'] = this.partnerBusiness;
          obj['ConsumerBusinessId'] = formValue.ConsumerBusinessId[0].id;
          obj['CategorySubCategoryId'] = formValue.CategorySubCategoryId[0].id;
          obj['Description'] = formValue.Description;
          formValue = obj;
          break;
        case 2:
          obj['partnerBusiness'] = this.partnerBusiness;
          obj['PartnerBusinessId'] = formValue.PartnerBusinessId[0].id;
          obj['ReportCategorySubCategoryId'] = formValue.ReportCategorySubCategoryId[0].id;
          obj['PartnerCategorySubCategoryId'] = formValue.PartnerCategorySubCategoryId[0].id;
          obj['ReportDescriptionDeal'] = formValue.ReportDescriptionDeal;
          obj['PartnerDescriptionDeal'] = formValue.PartnerDescriptionDeal;
          obj['BusinessDescription'] = formValue.BusinessDescription;
          formValue = obj;
          break
          case 3:
            formValue.BusinessesInCollaboration = []
            this.businessList.forEach(elemt=>{
              if(this.dForm.value['BusinessInCollaborationIds'].filter(x=>x.id==elemt.id).length>0){
                formValue.BusinessesInCollaboration.push(elemt)
              }
        })
            break;
        default:
          break;
      }
      this.submited.emit(formValue);
    }
  }
}

