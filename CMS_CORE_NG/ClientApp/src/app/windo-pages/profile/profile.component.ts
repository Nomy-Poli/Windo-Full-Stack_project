import { AfterContentChecked, AfterViewInit, ChangeDetectorRef, Component, Directive, Inject, OnInit, Optional } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BreadcrumbService } from 'src/app/services/breadcrumb.service';
import { AreaVm, BuisnessService, BuisnessVm, GuideModel } from 'src/app/services/Buisness.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';
import { SubCategoryVm } from 'src/app/services/SearchCategory.service';
import { AccountService } from 'src/app/services/account.service';
import Swal from 'sweetalert2';
import { HttpClient } from '@angular/common/http';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';
import { Router } from '@angular/router';
import { MessageService as ToastService } from 'primeng/api';
import { ScoringService } from 'src/app/services/Scoring.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent implements OnInit, AfterContentChecked {
  MAX_SIZE: number = 307200;
  ProfilePath: string = "../../../../../assets/icons/photo-camera-interface-symbol-for-button.png?w=150";//התמונה - לא נבחרה תמונה
  public apiBaseUrl: string;
  //pictures:
  coverPictureId: string;
  logoPictureId: string;
  workPictureGuide: GuideModel[] = [];//giud pic list
  uploadedWorksFiles: any[] = [];//the list of the files
  cameFromUpdate: boolean = false;
  openDeleteCoverImg: boolean = false;
  UserEmail: string;
  wantMailByCategory : boolean=false ;
  newCategory: any;
  //אם היא בחרה UPDATE
  updateBarter: boolean = false;
  // isInAllCountryFlag: boolean = false;
  //form group
  CoverForm: FormGroup;
  personalInformationForm: FormGroup;
  resOfUpdateBusinnes: BuisnessVm;//we want to keep the model that came from update
  personalInformation: BuisnessVm; //המודל אליו נכניס את הנתונים ביצירה / בעדכון העסק
  categoryOptionsAfterChoose: { label: number; value: string }[] = [];
  /*proggresbar*/
  progressBarValue: number;
  isFormGroupFull: Array<{ isFull: boolean, name: string, index: number }>;//,private messageService: MessageService

  //משתנה להצגת הקולאפסים
  public isCollapsed = [false, false, false, false,false];
  //משתנים להצגת תחומי התעסוקה - הקטגוריות
  public isToOpenCategory2: boolean = false;
  public isToOpenCategory3: boolean = false;
  public isToOpenCategory4: boolean = false;
  public isToOpenCategory5: boolean = false;
  public isToOpenCategoryForBarter2: boolean = false;
  public isToOpenCategoryForBarter3: boolean = false;
  public isToOpenCategoryForBarter4: boolean = false;
  public isToOpenCategoryForBarter5: boolean = false;
  public isBarterCollapsedOpen = true;//האם להציג בארטר?
  public isCountryCollapsedOpen = true;//האם להציג רשימת ערים
  public isOpenProductCollapsed = true;//האם להציג עוד מוצר?
  public isOpenProductForBarterCollapsed = true;//האם להציג עוד מוצר לבארטר?
  public barterFlag = false;//האם להציג עוד קטגוריות לבחירת בארטר?
  public flag = false;//האם להציג עוד קטגוריות לבחירת בארטר?
  public flagValidKindOfBusinnes: boolean = false;
  public addCat: boolean = true;//העיצוב של הפלוס בתחומי התמחות
  public addCatForBarterStyle: boolean = true;//העיצוב של הפלוס בקטגוריות הבארטר

  //קטגוריות - לעדכון;
  Category1: { label: number; value: string } = { label: null, value: 'בחרי קטגוריה ראשית' };
  Category2: { label: number; value: string } = { label: null, value: 'בחרי קטגוריה ראשית' };
  Category3: { label: number; value: string } = { label: null, value: 'בחרי קטגוריה ראשית' };
  Category4: { label: number; value: string } = { label: null, value: 'בחרי קטגוריה ראשית' };

  CategoryForOfferBarter1: { label: number; value: string } = { label: null, value: 'בחרי קטגוריה ראשית' };
  CategoryForOfferBarter2: { label: number; value: string } = { label: null, value: 'בחרי קטגוריה ראשית' };
  CategoryForOfferBarter3: { label: number; value: string } = { label: null, value: 'בחרי קטגוריה ראשית' };
  CategoryForOfferBarter4: { label: number; value: string } = { label: null, value: 'בחרי קטגוריה ראשית' };

  //קטגוריות לבארטר
  CForBarter1: { label: number; value: string } = { label: null, value: 'בחרי קטגוריה ראשית' };
  CForBarter2: { label: number; value: string } = { label: null, value: 'בחרי קטגוריה ראשית' };

  //תתי קטגוריות 
  //לפה נכניס את תתי הקטגוריות עם קוד הקישור
  ModelSubCategory1: SubCategoryVm[] = [];
  ModelSubCategory2: SubCategoryVm[] = [];
  ModelSubCategory3: SubCategoryVm[] = [];
  ModelSubCategory4: SubCategoryVm[] = [];

  get BusinessCategoriesNotify(){
    return this.personalInformationForm.get('BusinessCategoriesNotify') as FormArray;
  }

  //קטגוריות בשביל לדעת האם היא רוצה בארטר
  ModelSubCategoryForBarter1: SubCategoryVm[] = [];
  ModelSubCategoryForBarter2: SubCategoryVm[] = [];
  ModelSubCategoryForBarter3: SubCategoryVm[] = [];
  ModelSubCategoryForBarter4: SubCategoryVm[] = [];

  //תתי קטגוריות לבארטר 
  buisnessBarterCategory1: SubCategoryVm[] = [];
  buisnessBarterCategory2: SubCategoryVm[] = [];
  //סטטוס העלאת תמונה 
  workHasImg1: boolean = false;
  workHasImg2: boolean = false;
  workHasImg3: boolean = false;
  workHasImg4: boolean = false;
  workHasImg5: boolean = false;
  workHasImg6: boolean = false;

  //רשימת התת קטגורויות למילוי תת הקטגוריות לבארטר
  SubCategoryListForBurter1: SubCategoryVm[];
  SubCategoryListForBurter2: SubCategoryVm[];
  SubCategoryListForBurter3: SubCategoryVm[];
  SubCategoryListForBurter4: SubCategoryVm[];
  OpenBarterTwo: boolean = false;
  ModelArea: AreaVm[] = [];
  //רשימת ההקטגוריות ותתי הקטגוריות - הרשימה המקושרת
  public isOpen1 = true
  public isOpen2 = true
  public isOpen3 = true
  public isOpen4 = true
  public isOpen5 = true
  public file: File;

  ifSubmitClicked = false;
  ifScoringCliked = false;
  isLoading = false;

  constructor(
    public _buisnessService: BuisnessService,
    public _wrapperSearchService: WrapperSearchService,
    private _scoringService: ScoringService,
    private __funcService :WrapperFuncService,
    private fb: FormBuilder,
    public breadcrumbService: BreadcrumbService,
    public _wrapperFuncService: WrapperFuncService,
    public acct: AccountService,
    private httpClient: HttpClient,
    private router: Router,
    private cdref: ChangeDetectorRef,
    private _toast: ToastService,
    @Optional() @Inject('API_BASE_URL') apiBaseUrl?: string
  ) {
    this.apiBaseUrl = apiBaseUrl ? apiBaseUrl : "";
    breadcrumbService.setItem([
      { label: 'דף הבית', routerLink: ['/']/*, icon: 'pi pi-home'*/ },
      { label: 'פרופיל עסק', routerLink: ['/profile'] }
    ]);

  }
 
  // ngAfterViewInit() {
  //   this.cdRef.detectChanges();
  // }
  ngAfterContentChecked() {
    this.cdref.detectChanges();
  }
  submitToolTip = "עדין לא נערכו שינויים בטופס!"
  ngOnInit() {
    window.scroll(0, 0);
    this.acct.globalStateChanged.subscribe((state) => {
      this._wrapperSearchService.LoginStatus$.next(state.loggedInStatus);
    });
    this._wrapperSearchService.Username$ = this.acct.currentUserName;
    // console.log("this.acct",this.acct.currentBusiness.value.id);
    
    this._wrapperFuncService.closeDialog();
    this._wrapperSearchService.HomePage$.next(false);
    this._wrapperSearchService.getAreaOptions();
    this.isFormGroupFull =
      [
        { isFull: false, name: "buisnessName", index: 1 },
        { isFull: false, name: "actionDiscription", index: 2 },
        { isFull: false, name: "discription", index: 3 },
        // { isFull: false, name: "product1", index: 4 },
        { isFull: false, name: "Category1", index: 4 },
        { isFull: false, name: "SubCategory1", index: 5 },
        { isFull: false, name: "buisnessOption", index: 6 },
        // { isFull: false, name: "CategoryForBarter1", index: 8 },
        // { isFull: false, name: "CategoryForBarter2", index: 9 },
        // { isFull: false, name: "BarterProduct1", index: 10 },
      ];

    this.personalInformation = { id: 0 };
    this.resOfUpdateBusinnes = { id: 0, logoPictureId: '00000000-0000-0000-0000-000000000000' }
    //checking if it's an update state
    //  if (this._wrapperSearchService.flagCreateOrUpdate == true) {
    // this.idUser = localStorage.getItem("logInUserId");
    this.UserEmail = this.acct.Email.value;
    // }
    this.progressBarValue = 0;
    //cover pic
    this.CoverForm = this.fb.group({
      coverpicfile: new FormControl(),//תמונת כובר
    })
    //כל הטופס
    console.log("acct",this.acct);
    
    this.personalInformationForm = this.fb.group({
      idBusiness: new FormControl(0),
      // firstname: new FormControl( this.acct.FirstName.value /*[Validators.required, Validators.maxLength(15), Validators.minLength(2)]*/),
      // lastname: new FormControl(this.acct.LastName.value /*[Validators.required, Validators.maxLength(15), Validators.minLength(2)]*/),
      isdisplayBusinessOwnerName: new FormControl(false),
      buisnessName: new FormControl('', [Validators.required, Validators.maxLength(30), Validators.minLength(2)]),
      email: new FormControl({ value: this.acct.Email.value, disabled: true }, /*[Validators.required, Validators.email]*/),
      buisnessEmailAddress: new FormControl('', [Validators.pattern('[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$')/*Validators.required, Validators.maxLength(20), Validators.minLength(10)*/]),
      buisnessWebSiteLink: new FormControl('', [Validators.minLength(3)] /*[Validators.required, Validators.maxLength(20), Validators.minLength(10)]*/),
      phoneNumber1: new FormControl('', [/*Validators.required,*/ Validators.pattern('^[0-9]{2,3}[-. ]?[0-9]{7}$')]),
      phoneNumber2: new FormControl('', [/* [Validators.required,*/ Validators.pattern('^[0-9]{2,3}[-. ]?[0-9]{7}$')]),//, Validators.pattern(this.phonePattern)
      address: new FormControl('',/* [Validators.required, Validators.maxLength(20), Validators.minLength(4)]*/),
      isInAllCountry: new FormControl(true),//בכל הארץ?
      actionDiscription: new FormControl('', [Validators.required, Validators.maxLength(50), Validators.minLength(5)]),
      discription: new FormControl('', [Validators.required, Validators.maxLength(120), Validators.minLength(5)]),
      ispayingBuisness: new FormControl(false),//בתשלום
      isburterBuisness: new FormControl(false),//בבארטר
      iscollaborationBuisness: new FormControl(false),//בשתפ
      isAllServiceForBurter: new FormControl(true),  //האם אני נותנת את כל השירותי ם שלי כבארטר - הקטגוריות שכבר בחרתי
      OptionalCollaborationDescription: new FormControl('', Validators.maxLength(100)),
      profpicfile: new FormControl(),//תמונת הלוגו
      workpicfile: new FormControl([]),//תמונות של תיק העבודות
      product1: new FormControl(''),
      product2: new FormControl(),//''
      BarterProduct1: new FormControl(''),
      BarterProduct2: new FormControl(''),
      area: new FormControl(this.ModelArea, /*[Validators.required]*/),
      //קטגוריות - בשביל העדכון
      Category1: new FormControl(this.Category1/*[Validators.required]*/),
      Category2: new FormControl(this.Category2/*[Validators.required]*/),
      Category3: new FormControl(this.Category3/*[Validators.required]*/),
      Category4: new FormControl(this.Category4/*[Validators.required]*/),
      //קטגוריות - לבארטר
      CategoryForOfferBarter1: new FormControl(this.CategoryForOfferBarter1),
      CategoryForOfferBarter2: new FormControl(this.CategoryForOfferBarter2),
      CategoryForOfferBarter3: new FormControl(this.CategoryForOfferBarter3),
      CategoryForOfferBarter4: new FormControl(this.CategoryForOfferBarter4),
      //בשביל התתי קטגוריות
      SubCategory1: new FormControl([Validators.required]),
      SubCategory2: new FormControl(/*[Validators.required]*/),
      SubCategory3: new FormControl(/*[Validators.required]*/),
      SubCategory4: new FormControl(/*[Validators.required]*/),
      SubCategoryForBarter1: new FormControl(/*[Validators.required]*/),
      SubCategoryForBarter2: new FormControl(/*[Validators.required]*/),
      SubCategoryForBarter3: new FormControl(/*[Validators.required]*/),
      SubCategoryForBarter4: new FormControl(/*[Validators.required]*/),
      //בארטר
      CForBarter1: new FormControl(this.CForBarter1),
      CForBarter2: new FormControl(this.CForBarter2),
      CategoryForBarter1: new FormControl(this.buisnessBarterCategory1,/* [Validators.required]*/),
      CategoryForBarter2: new FormControl(this.buisnessBarterCategory2,/* [Validators.required]*/),
      //רק כדי לדעת האם היא רוצה בארטר?
      IWantProduct: new FormControl(false),
      IWantProductForBarter: new FormControl(false),
      IWantToOpenCategoryToGetBarter: new FormControl(true),

      //performents
      WantedGetHelpNotification: new FormControl(),
      WantedGetDailyNotification: new FormControl(),
      BusinessCategoriesNotify: new FormArray([])
    });
    //בשביל עדכון - קבלת הפרטים של המשתמש לפי מייל
    this.GetBusinessByEmailId(this.UserEmail);
    this.CheckIfAtLeastOnePicExist();
    window.scrollTo(0, 0);//on reload - go to the top of the page
    //////////////

  }
  // product1
  clearText(id: string) {
    this.personalInformationForm.controls[id].setValue('');
  }
  InsertAllValueFromUpdate(result: BuisnessVm) {
    this.cameFromUpdate = true;
    this._wrapperSearchService.categoriesForChoose = this._wrapperSearchService.categoryOptions.filter(x => x.label > 0);
    //אם היא בחרה בארטר נפתח לה את הקולאפס של הבארטר
    if (result.isburterBuisness == true) {
      this.isBarterCollapsedOpen = false;
    }
    this.ModelArea = [];
    if (result.buisnessAreaList1 != null) {
      result.buisnessAreaList1.forEach(area => {
        // if (area.areaId == 1 && this.personalInformationForm.get('isInAllCountry').value == true)//אם זה כל הארץ
        if (area.areaId == 1)//אם זה כל הארץ
        {
          this.personalInformationForm.get('isInAllCountry').setValue(true);
          // this.ModelArea = [];//נאפס את המערך - כי אם יש את כל הארץ לא צריך כלום מעבר
          // return;
          // throw console.log("העסק מתאפשר בכל הארץ");
        }
        else {
          this.personalInformationForm.get('isInAllCountry').setValue(false);
          this.isCountryCollapsedOpen = false;
          this.ModelArea.push({
            id: area.areaId,
            name: (this._wrapperSearchService.AreaOptionsProfile.find((areaN => areaN.id === area.areaId))).name
          })
          this.ModelArea = [...new Set(this.ModelArea)];//מוריד כפולים
        }
      });
    }
    //1
    if (result.buisnessCategory1 != null) {
      this.ModelSubCategory1 = [];//נאתחל את המערך מכל מה שהיה לפני - (אם זה בעדכון)
      result.buisnessCategory1.forEach(subC1 => {
        if (this.Category1.label == null) {
          // this.personalInformationForm.get('Category1').setValue({ label: subC1.categoryId, value: subC1.categoryName })
          this.Category1 = {
            label: subC1.categoryId,
            value: subC1.categoryName
          }
          this._wrapperSearchService.getSubCategoriesOptions1(this.Category1.label);
          this.categoryOptionsAfterChoose[0] = { label: this.Category1.label, value: this.Category1.value };
        }
        this.ModelSubCategory1.push(
          {
            CategorySubCategoryId: subC1.combinationtId,
            Id: subC1.subCategoryId,
            name: subC1.subCategoryName
          }
        )
        if (subC1.isPossibleInBarter == true)//בדיקה בשביל הבאטר - למה שהיא נותנת - מילוי הקטגוריות והתתי קטגוריות
        {
          this.CategoryForOfferBarter1 = {
            label: subC1.categoryId,
            value: subC1.categoryName
          }
          // console.log
          this.personalInformationForm.get('CategoryForOfferBarter1').setValue(subC1.categoryName);
          this.ModelSubCategoryForBarter1.push({
            CategorySubCategoryId: subC1.combinationtId,
            Id: subC1.subCategoryId,
            name: subC1.subCategoryName,
          });
        }
      });
      this.SubCategoryListForBurter1 = this.ModelSubCategory1;
      this.ModelSubCategoryForBarter1 = [...new Set(this.ModelSubCategoryForBarter1)];//מוריד כפולים
      this.ModelSubCategory1 = [...new Set(this.ModelSubCategory1)];//מוריד כפולים
      //if notify
      let ifChoosedCategory = result.BusinessCategoriesNotify.length >0 && result.BusinessCategoriesNotify.find(x=>x.categoryId == result.buisnessCategory1[0].categoryId)? true: false
      this.BusinessCategoriesNotify.push( new FormControl(ifChoosedCategory));
    }
    //categ... 2
    if (result.buisnessCategory2.length > 0) {
      this.ModelSubCategory2 = [];//נאתחל את המערך מכל מה שהיה לפני - (אם זה בעדכון)
      result.buisnessCategory2.forEach(subC2 => {
        if (this.Category2.label == null) {
          // this.personalInformationForm.get('Category1').setValue({ label: subC1.categoryId, value: subC1.categoryName })
          this.Category2 = {
            label: subC2.categoryId,
            value: subC2.categoryName
          }
          this._wrapperSearchService.getSubCategoriesOptions2(this.Category2.label);
          this.categoryOptionsAfterChoose[1] = { label: this.Category2.label, value: this.Category2.value };
        }
        this.ModelSubCategory2.push(
          {
            CategorySubCategoryId: subC2.combinationtId,
            Id: subC2.subCategoryId,
            name: subC2.subCategoryName
          }
        );
        if (subC2.isPossibleInBarter == true)//בדיקה בשביל הבאטר - למה שהיא נותנת - מילוי הקטגוריות והתתי קטגוריות
        {
          this.isToOpenCategoryForBarter2 = true;
          this.CategoryForOfferBarter2 = {
            label: subC2.categoryId,
            value: subC2.categoryName
          }
          this.personalInformationForm.get('CategoryForOfferBarter2').setValue(subC2.categoryName);
          this.ModelSubCategoryForBarter2.push({
            CategorySubCategoryId: subC2.combinationtId,
            Id: subC2.subCategoryId,
            name: subC2.subCategoryName,
          });
        }
      });
      this.isToOpenCategory2 = true;
      this.SubCategoryListForBurter2 = this.ModelSubCategory2;//אתחול רשימת ההצעות של הקטגוריות
      this.ModelSubCategory2 = [...new Set(this.ModelSubCategory2)];//מוריד כפולים
      this.ModelSubCategoryForBarter2 = [...new Set(this.ModelSubCategoryForBarter2)];//מוריד כפולים
      //if notify
      let ifChoosedCategory = result.BusinessCategoriesNotify.length >0 && result.BusinessCategoriesNotify.find(x=>x.categoryId == result.buisnessCategory2[0].categoryId)? true: false
      this.BusinessCategoriesNotify.push( new FormControl(ifChoosedCategory));
     

    }
    // //categ... 3
    if (result.buisnessCategory3.length > 0) {
      this.ModelSubCategory3 = [];//נאתחל את המערך מכל מה שהיה לפני - (אם זה בעדכון)
      result.buisnessCategory3.forEach(subC3 => {
        if (this.Category3.label == null) {
          // this.personalInformationForm.get('Category1').setValue({ label: subC1.categoryId, value: subC1.categoryName })
          this.Category3 = {
            label: subC3.categoryId,
            value: subC3.categoryName
          }
          this._wrapperSearchService.getSubCategoriesOptions3(this.Category3.label);
          this.categoryOptionsAfterChoose[2] = { label: this.Category3.label, value: this.Category3.value };
        }
        this.ModelSubCategory3.push(
          {
            CategorySubCategoryId: subC3.combinationtId,
            Id: subC3.subCategoryId,
            name: subC3.subCategoryName
          }
        );
        if (subC3.isPossibleInBarter == true)//בדיקה בשביל הבאטר - למה שהיא נותנת - מילוי הקטגוריות והתתי קטגוריות
        {
          this.isToOpenCategoryForBarter3 = true;
          this.CategoryForOfferBarter3 = {
            label: subC3.categoryId,
            value: subC3.categoryName
          }
          this.personalInformationForm.get('CategoryForOfferBarter3').setValue(subC3.categoryName);
          this.ModelSubCategoryForBarter3.push({
            CategorySubCategoryId: subC3.combinationtId,
            Id: subC3.subCategoryId,
            name: subC3.subCategoryName,
          });
        }
      });
      this.isToOpenCategory3 = true;
      this.SubCategoryListForBurter3 = this.ModelSubCategory3;
      this.ModelSubCategory3 = [...new Set(this.ModelSubCategory3)];//מוריד כפולים
      this.ModelSubCategoryForBarter3 = [...new Set(this.ModelSubCategoryForBarter3)];//מוריד כפולים
      //if notify
      let ifChoosedCategory = result.BusinessCategoriesNotify.length >0 && result.BusinessCategoriesNotify.find(x=>x.categoryId == result.buisnessCategory3[0].categoryId)? true: false
      this.BusinessCategoriesNotify.push( new FormControl(ifChoosedCategory));

    }
    // //categ... 4
    
    if (result.buisnessCategory4.length > 0) {
      this.ModelSubCategory4 = [];//נאתחל את המערך מכל מה שהיה לפני - (אם זה בעדכון)
      result.buisnessCategory4.forEach(subC4 => {
        if (this.Category4.label == null) {
          // this.personalInformationForm.get('Category1').setValue({ label: subC1.categoryId, value: subC1.categoryName })
          this.Category4 = {
            label: subC4.categoryId,
            value: subC4.categoryName
          }
          this._wrapperSearchService.getSubCategoriesOptions4(this.Category4.label);
          this.categoryOptionsAfterChoose[3] = { label: this.Category4.label, value: this.Category4.value };
        }
        this.ModelSubCategory4.push(
          {
            CategorySubCategoryId: subC4.combinationtId,
            Id: subC4.subCategoryId,
            name: subC4.subCategoryName
          }
        );
        if (subC4.isPossibleInBarter == true)//בדיקה בשביל הבאטר - למה שהיא נותנת - מילוי הקטגוריות והתתי קטגוריות
        {
          this.isToOpenCategoryForBarter4 = true;
          this.CategoryForOfferBarter4 = {
            label: subC4.categoryId,
            value: subC4.categoryName
          }
          this.personalInformationForm.get('CategoryForOfferBarter4').setValue(subC4.categoryName);
          this.ModelSubCategoryForBarter4.push({
            CategorySubCategoryId: subC4.combinationtId,
            Id: subC4.subCategoryId,
            name: subC4.subCategoryName,
          });
        }
      });
      this.isToOpenCategory4 = true;
      this.SubCategoryListForBurter4 = this.ModelSubCategory4;
      this.ModelSubCategory4 = [...new Set(this.ModelSubCategory4)];//מוריד כפולים
      this.ModelSubCategoryForBarter4 = [...new Set(this.ModelSubCategoryForBarter4)];//מוריד כפולים
      this.addCat = false;
      //if notify
      let ifChoosedCategory = result.BusinessCategoriesNotify.length >0 && result.BusinessCategoriesNotify.find(x=>x.categoryId == result.buisnessCategory4[0].categoryId)? true: false
      this.BusinessCategoriesNotify.push( new FormControl(ifChoosedCategory));
  
    }
    console.log('this.BusinessCategoriesNotify',this.BusinessCategoriesNotify);

    if (result.buisnessBarterCategory1.length > 0) {
      this.buisnessBarterCategory1 = [];//נאתחל את המערך מכל מה שהיה לפני - (אם זה בעדכון)
      result.buisnessBarterCategory1.forEach(subBarter1 => {
        if (this.CForBarter1.label == null) {
          // this.personalInformationForm.get('Category1').setValue({ label: subC1.categoryId, value: subC1.categoryName })
          this.CForBarter1 = {
            label: subBarter1.categoryId,
            value: subBarter1.categoryName
          }
          this._wrapperSearchService.getSubCategoriesOptionsForBarter1(subBarter1.categoryId);
        }
        this.buisnessBarterCategory1.push(
          {
            CategorySubCategoryId: subBarter1.combinationtId,
            Id: subBarter1.subCategoryId,
            name: subBarter1.subCategoryName
          }
        )
      });
      this.buisnessBarterCategory1 = [...new Set(this.buisnessBarterCategory1)];//מוריד כפולים
    }
    //cate... barter 2
    if (result.buisnessBarterCategory2.length > 0) {
      this.buisnessBarterCategory2 = [];//נאתחל את המערך מכל מה שהיה לפני - (אם זה בעדכון)
      result.buisnessBarterCategory2.forEach(subBarter2 => {
        if (this.CForBarter2.label == null) {
          // this.personalInformationForm.get('Category1').setValue({ label: subC1.categoryId, value: subC1.categoryName })
          this.CForBarter2 = {
            label: subBarter2.categoryId,
            value: subBarter2.categoryName
          }
          this._wrapperSearchService.getSubCategoriesOptionsForBarter2(subBarter2.categoryId);
        }
        this.buisnessBarterCategory2.push(
          {
            CategorySubCategoryId: subBarter2.combinationtId,
            Id: subBarter2.subCategoryId,
            name: subBarter2.subCategoryName
          }
        )
      });
      this.buisnessBarterCategory2 = [...new Set(this.buisnessBarterCategory2)];//מוריד כפולים
      this.OpenBarterTwo = true;
      console.log(this._wrapperSearchService.subCategoryOptionForBarter2);
      console.log(this.buisnessBarterCategory2);
    }
    this.CoverForm.patchValue({
      coverpicfile: result.coverPicture
    })
    if (result.OptionalCollaborationDescription == null) {
      result.OptionalCollaborationDescription = "";
    }
    this.personalInformationForm.patchValue({
      
      // name: result.buisnessName,
      idBusiness: result.id,
      buisnessName: result.buisnessName,
      profpicfile: result.logoPicture,
      phonNumber: result.phoneNumber1,
      buisnessWebSiteLink: result.buisnessWebSiteLink,
      buisnessEmailAddress: result.businessEmailAddress,
      discription: result.discription ? result.discription : '',
      actionDiscription: result.actionDiscription ? result.actionDiscription : '',
      phoneNumber1: result.phoneNumber1,
      phoneNumber2: result.phoneNumber2,
      address: result.address,
      ispayingBuisness: result.ispayingBuisness,
      isburterBuisness: result.isburterBuisness,
      iscollaborationBuisness: result.iscollaborationBuisness,
      product1: result.product1 ? result.product1 : '',
      product2: result.product2 ? result.product2 : '',
      BarterProduct1: result.barterProduct1 ? result.barterProduct1 : '',
      BarterProduct2: result.barterProduct2 ? result.barterProduct2 : '',
      isAllServiceForBurter: result.isburterPossibleInAllCategory,
      OptionalCollaborationDescription: result.OptionalCollaborationDescription,
      IWantToOpenCategoryToGetBarter: result.isopenToSuggestionsForBarter,
      isdisplayBusinessOwnerName: result.isdisplayBusinessOwnerName,
      WantedGetHelpNotification: result.WantedGetHelpNotification,
      WantedGetDailyNotification: result.WantedGetDailyNotification,
      // BusinessCategoriesNotify:result.BusinessCategoriesNotify
    });
    if (result.product1 != "") {
      this.personalInformationForm.get('IWantProduct').setValue(true);
      this.flag = true;
    }
    if (result.barterProduct1 != "") {
      this.personalInformationForm.get('IWantProductForBarter').setValue(true);
      this.barterFlag = true;
    }
    this.coverPictureId = result.coverPictureId;
    this.logoPictureId = result.logoPictureId;
    //insert the work guide list into array
    if (result.workPictureGuide != null) {
      result.workPictureGuide.forEach((guide, index) => {
        this.workPictureGuide[index] = guide;
      })
    }
    //insert the cover pic
    if (result.coverPictureId != '00000000-0000-0000-0000-000000000000') {
      this.openDeleteCoverImg = true;
      var src = `../../../../../assets/BusinessImages/` + result.id + `/Cover/` + result.coverPictureId + `/` + result.coverPictureId + `.jpg?h=210`;
      $('#cover-img')
        .find('img')
        .attr('src', src);
      var a = $('#cover-img').find('img')
      console.log(a);
    }
    else {
      $('#cover-img')
        .find('img')
        .attr('src', " ");
    }
    //insert the logo pic
    if (result.logoPictureId != '00000000-0000-0000-0000-000000000000') {
      var src = `../../../../../assets/BusinessImages/` + result.id + `/Logo/` + result.logoPictureId + `/` + result.logoPictureId + `.jpg?w=105`;
      $('#profpic')
        .find('img')
        .attr('src', src);
    }
    else {
      $('#profpic')
        .find('img')
        .attr('src', " ");
      $('#profile')
        .find('img')
        .attr('src', this.ProfilePath);

    }
    this.file = null;
    //insert the pic
    if (this.workPictureGuide != null) {

      this.workPictureGuide.forEach(wp => {
        var src = `../../../../../assets/BusinessImages/` + result.id + `/Work/` + wp.picindex + `/` + wp.workPicGuide + `/` + wp.workPicGuide + `.jpg?w=240`;
        $('#' + wp.picindex).attr('src', src);
        if (wp.workPicGuide == undefined) {
          this['workHasImg' + wp.picindex] = false;
          // this.workHasImg1 = false;
          $('#' + wp.workPicGuide)
            .attr('src', " ");
        }
        else {
          this['workHasImg' + wp.picindex] = true;
        }
      })
    }
    //עדכון מצב הטופס אחרי הבאת הפרטים של המשתמש
    this.personalInformationForm.get('buisnessName').updateValueAndValidity();
    this.personalInformationForm.get('actionDiscription').updateValueAndValidity();
    this.personalInformationForm.get('discription').updateValueAndValidity();
    this.personalInformationForm.get('SubCategory1').updateValueAndValidity();
    this.personalInformationForm.get('discription').updateValueAndValidity();
    // this.personalInformationForm.updateValueAndValidity();

    //this dictionary is use to know how many inputs are full to know how much precent of the
    // //reset the progrres bar
    this.progressBarValue = 0;
    // fill the progress bar after fetch the user data
    result.buisnessName == '' ? this.isFormGroupFull[0].isFull = false : this.isFormGroupFull[0].isFull = true;
    result.actionDiscription == '' ? this.isFormGroupFull[1].isFull = false : this.isFormGroupFull[1].isFull = true;
    result.discription == '' ? this.isFormGroupFull[2].isFull = false : this.isFormGroupFull[2].isFull = true;
    // result.product1 == '' ? this.isFormGroupFull[3].isFull = false : this.isFormGroupFull[3].isFull = true;
    result.buisnessCategory1.length == 0 ? this.isFormGroupFull[3].isFull = false : this.isFormGroupFull[3].isFull = true;
    result.buisnessCategory1.length == 0 ? this.isFormGroupFull[4].isFull = false : this.isFormGroupFull[4].isFull = true;
    this.checkValidationForBusinesOptionToProssesBar() == false ? this.isFormGroupFull[5].isFull = true : this.isFormGroupFull[5].isFull = false;
    // result.buisnessAreaList1.length == 0 ? this.isFormGroupFull[6].isFull = false : this.isFormGroupFull[6].isFull = true;
    // result.buisnessBarterCategory1.length == 0 ? this.isFormGroupFull[7].isFull = false : this.isFormGroupFull[7].isFull = true;
    // result.buisnessBarterCategory2.length == 0 ? this.isFormGroupFull[8].isFull = false : this.isFormGroupFull[8].isFull = true;
    // result.barterProduct1 == '' ? this.isFormGroupFull[9].isFull = false : this.isFormGroupFull[9].isFull = true;

    this.isFormGroupFull.forEach(element => {
      if (element.isFull == false && element.name == "buisnessOption") {
        this.progressBarValue += 15;
        // else
        // this.progressBarValue += 17;
      }
      if (element.isFull == true)
        this.progressBarValue += 17;

    });
   this.BusinessCategoriesNotify.value.forEach((element) => {
    if(element)
    {
     this.wantMailByCategory = true;
     console.log("fff",this.wantMailByCategory);
     
    }
    console.log("gggg",this.wantMailByCategory);
  });
  }
  GetBusinessByEmailId(UserEmail: string) {
    this._buisnessService.getBuisnessByEmailId(UserEmail,null).subscribe(
      res => {
        if (res != null) {
          this.InsertAllValueFromUpdate(res);
          this.resOfUpdateBusinnes = res;
          console.log(res);
        }
      });
  };
  // time line
  checkValidationForTimeLine() {
    if (this.personalInformationForm.get('buisnessName').hasError('required')
      && this.personalInformationForm.get('actionDiscription').hasError('required')
      && this.personalInformationForm.get('discription').hasError('required')

    ) {
      return true;
    }
    else
      return false
  }
  checkValidationForTimeLine1() {
    if (this.personalInformationForm.get('SubCategory1').hasError('required') === true ||
      this.personalInformationForm.get('buisnessName').hasError('required') == true
      || this.personalInformationForm.get('actionDiscription').hasError('required') == true
      || this.personalInformationForm.get('discription').hasError('required') == true
    ) {

      return true;
    }
    else
      return false;
  }
  // ===============================================קטגוריות==========================================
  checkWitchCategoryToOpen() {
    if (this.isToOpenCategory2 == false) {
      this.isToOpenCategory2 = true;
      this.addCat = true;
    }
    else if (this.isToOpenCategory3 == false) {
      this.isToOpenCategory3 = true;
      this.addCat = true;
    }
    else if (this.isToOpenCategory4 == false) {
      this.isToOpenCategory4 = true;
      this.addCat = false;
    }
  }
  checkWitchCategoryToOpenInBarter() {
    if (this.isToOpenCategoryForBarter2 == false) {
      this.isToOpenCategoryForBarter2 = true;
      this.addCatForBarterStyle = true;
    }
    else if (this.isToOpenCategoryForBarter3 == false) {
      this.isToOpenCategoryForBarter3 = true;
      this.addCatForBarterStyle = true;
    }
    else if (this.isToOpenCategoryForBarter4 == false) {
      this.isToOpenCategoryForBarter4 = true;
      this.addCatForBarterStyle = false;
    }
  }
  setDisableOfChoosedCategory() {
    this._wrapperSearchService.categoriesForChoose.forEach(categoty => {
      if ((this.categoryOptionsAfterChoose[0] && categoty.label == this.categoryOptionsAfterChoose[0].label)
        || (this.categoryOptionsAfterChoose[1] && categoty.label == this.categoryOptionsAfterChoose[1].label)
        || (this.categoryOptionsAfterChoose[2] && categoty.label == this.categoryOptionsAfterChoose[2].label)
        || (this.categoryOptionsAfterChoose[3] && categoty.label == this.categoryOptionsAfterChoose[3].label)
      )
        categoty.disabled = true;
      else {
        categoty.disabled = false;
      }
    })
  }

  onChangeCategory1(newValue) {
    //take care of the progress bar 
    let temp = this.isFormGroupFull.find(v => v.name == "Category1");
    if (newValue.value == null) {
      if (temp.isFull == true) {
        this.progressBarValue -= 17
        this.isFormGroupFull[3].isFull = false;
      }
    }
    else {
      if (temp.isFull != true) {
        this.progressBarValue += 17;
        this.isFormGroupFull[3].isFull = true;
      }
    }
    this.categoryOptionsAfterChoose[0] = { label: newValue.value.label, value: newValue.value.value };
    this.setDisableOfChoosedCategory();
    this.ModelSubCategory1 = [];
    this._wrapperSearchService.getSubCategoriesOptions1(newValue.value.label);
        //הוספה ל פורומגרופ של קבלת תגובה במייל 
     if(this.BusinessCategoriesNotify.controls.length==0 )
        this.BusinessCategoriesNotify.push( new FormControl(false));
    else{
        this.wantMailByCategory==true;
    }
    this.BusinessCategoriesNotify.controls[0].setValue(false);

  }
  onChangeSubCategory1(newValue) {
    let temp = this.isFormGroupFull.find(v => v.name == "SubCategory1");
    if (newValue.value == null || newValue.value == 0) {
      if (temp.isFull == true) {
        this.progressBarValue -= 17;
        this.isFormGroupFull[4].isFull = false;
      }
    }
    else {
      if (temp.isFull != true) {
        this.progressBarValue += 17;
        this.isFormGroupFull[4].isFull = true;
      }
    }

    const a = [...new Set(this.ModelSubCategory1)];//מוריד כפולים
    // this.SubCategoryListForBurter1 = this.ModelSubCategory1;
  }
  // ~~~~~~~~~~~~~2
  onChangeCategory2(newValue) {
    this.ModelSubCategory2 = [];
    this._wrapperSearchService.getSubCategoriesOptions2(newValue.value.label);
    this.categoryOptionsAfterChoose[1] = { label: newValue.value.label, value: newValue.value.value };
    this.setDisableOfChoosedCategory();
      //הוספה ל פורומגרופ של קבלת תגובה במייל 
      if(this.BusinessCategoriesNotify.controls.length==1 )
         this.BusinessCategoriesNotify.push( new FormControl(false));
  this.BusinessCategoriesNotify.controls[1].setValue(false);

  }
  // ~~~~~~~~~~~~~~~~~~~~~3
  onChangeCategory3(newValue) {
    // this.Category3 = newValue.value.label;
    this.ModelSubCategory3 = [];
    this._wrapperSearchService.getSubCategoriesOptions3(newValue.value.label);
    this.categoryOptionsAfterChoose[2] = { label: newValue.value.label, value: newValue.value.value };
    this.setDisableOfChoosedCategory();
    // this.personalInformationForm.get("topicId").setValue(newValue.value.label)
      //הוספה ל פורומגרופ של קבלת תגובה במייל 
      if(this.BusinessCategoriesNotify.controls.length==2 )
      this.BusinessCategoriesNotify.push( new FormControl(false));
  this.BusinessCategoriesNotify.controls[2].setValue(false);
  }
  // ~~~~~~~~~~~~~~~~~~~~~~4
  onChangeCategory4(newValue) {
    // this.Category4 = newValue.value.label;
    this.ModelSubCategory4 = [];
    this._wrapperSearchService.getSubCategoriesOptions4(newValue.value.label);
    this.categoryOptionsAfterChoose[3] = { label: newValue.value.label, value: newValue.value.value };
    this.setDisableOfChoosedCategory();
      //הוספה ל פורומגרופ של קבלת תגובה במייל 
      if(this.BusinessCategoriesNotify.controls.length==3 )
      this.BusinessCategoriesNotify.push( new FormControl(false));
  this.BusinessCategoriesNotify.controls[3].setValue(false);
  }

  //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~barter
  onChangeCategoryForBarter1(newValue) {
    //take care of the progress bar 
    // let temp = this.isFormGroupFull.find(v => v.name == "CategoryForBarter1");
    // if (newValue.value == null) {
    //   if (temp.isFull == true) {
    //     this.progressBarValue -= 10;
    //     this.isFormGroupFull[7].isFull = false;
    //   }
    // }
    // else {
    //   if (temp.isFull != true) {
    //     this.progressBarValue += 10;
    //     this.isFormGroupFull[7].isFull = true;
    //   }
    // }
    this.buisnessBarterCategory1 = [];
    this.CForBarter1 = newValue.value.label;
    this._wrapperSearchService.getSubCategoriesOptionsForBarter1(newValue.value.label);
    // this.personalInformationForm.get("topicId").setValue(newValue.value.label)
  }
  onChangeCategoryForBarter2(newValue) {
    //take care of the progress bar 
    // let temp = this.isFormGroupFull.find(v => v.name == "CategoryForBarter2");
    // if (newValue.value == null) {
    //   if (temp.isFull == true) {
    //     this.progressBarValue -= 10;
    //     this.isFormGroupFull[8].isFull = false;
    //   }
    // }
    // else {
    //   if (temp.isFull != true) {
    //     this.progressBarValue += 10;
    //     this.isFormGroupFull[8].isFull = true;
    //   }
    // }
    this.buisnessBarterCategory2 = [];
    this.CForBarter2 = newValue.value.label;
    this._wrapperSearchService.getSubCategoriesOptionsForBarter2(newValue.value.label);
    // this.personalInformationForm.get("topicId").setValue(newValue.value.label)
  }
  //~~~~~~~~~~~~~~~~~~~~~~~~~~ what i let to barter  ~~~~~~~~~~~~~~~~~~~~~~
  OnCategoryOfBarterToGiveChange(NewValue, index) {
    if (NewValue.value.label === this.categoryOptionsAfterChoose[0].label) {
      // this.SubCategoryListForBurter1 = this.ModelSubCategory1;      
      this['SubCategoryListForBurter' + index] = this.ModelSubCategory1;
      // console.log(this.SubCategoryListForBurter1);
    }
    else
      if (NewValue.value.label === this.categoryOptionsAfterChoose[1].label) {
        // this.SubCategoryListForBurter2 = this.ModelSubCategory2;
        this['SubCategoryListForBurter' + index] = this.ModelSubCategory2;
      }
      else
        if (NewValue.value.label === this.categoryOptionsAfterChoose[2].label) {
          // this.SubCategoryListForBurter3 = this.ModelSubCategory3;
          this['SubCategoryListForBurter' + index] = this.ModelSubCategory3;
        }
        else
          if (NewValue.value.label === this.categoryOptionsAfterChoose[3].label) {
            // this.SubCategoryListForBurter4 = this.ModelSubCategory4;
            this['SubCategoryListForBurter' + index] = this.ModelSubCategory4;
          }
  }
  //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ delete category ~~~~~~~~~~~~~~~~~~~~~
  OndeleteCategory(index) {
    //איתחול הקטגוריות - המערך עצמו
    this['Category' + index] = { label: null, value: null };
    //איפוס הערך של הקטגוריה בטופס
    this.personalInformationForm.get('Category' + index).setValue(this['Category' + index]);
    //איפוס התת קטגוריות - אם הוא כבר בחר
    this['ModelSubCategory' + index] = [];
    // איפוס התת קטגוריות מהטופס
    this.personalInformationForm.get('SubCategory' + index).setValue(this['ModelSubCategory' + index]);
    //איפוס האופציות בראפר
    this._wrapperSearchService['subCategoryOptions' + index] = [];
    //עדכון הקולאפס של הפלוס
    this['isToOpenCategory' + index] = false;
    this.addCat = true;
    this.categoryOptionsAfterChoose[index] = { label: null, value: null };
    this.setDisableOfChoosedCategory();
    //עדכון האופציה של קבלת מייל 
    console.log("jjjj",this.BusinessCategoriesNotify);
    this.BusinessCategoriesNotify.removeAt(index-1);
  }
  //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ delete barter category ~~~~~~~~~~~~~~~~~~~~~
  OndeleteOfferBarterCategory(index) {
    //איתחול הקטגוריות - המערך עצמו    
    this['SubCategoryListForBurter' + index] = this.categoryOptionsAfterChoose;
    //איפוס הערך של הקטגוריה בטופס
    this.personalInformationForm.get('CategoryForOfferBarter' + index).setValue(null);
    //איפוס התת קטגוריות - אם הוא כבר בחר
    this['ModelSubCategoryForBarter' + index] = [];
    // איפוס התת קטגוריות מהטופס
    this.personalInformationForm.get('SubCategoryForBarter' + index).setValue(this['ModelSubCategoryForBarter' + index]);
    //עדכון הקולאפס של הפלוס
    this['isToOpenCategoryForBarter' + index] = false;
    this.addCatForBarterStyle = true;
  }
  OndeleteGetBarterCategory() {
    this.CForBarter2 = { label: null, value: 'בחרי קטגוריה ראשית' };
    //איפוס הערך של הקטגוריה בטופס
    this.personalInformationForm.get('CForBarter2').setValue(null);
    //איפוס התת קטגוריות - אם הוא כבר בחר
    this.buisnessBarterCategory2 = [];
    // איפוס התת קטגוריות מהטופס
    this.personalInformationForm.get('CategoryForBarter2').setValue(this.buisnessBarterCategory2);
    //עדכון הקולאפס של הפלוס
    this.OpenBarterTwo = false;
    //עדכון העיצוב של הפלוס
  }
  // ==========================================================================================
  // onChangeArea(newValue) {
  //   let temp = this.isFormGroupFull.find(v => v.name == "area");
  //   if (newValue.value == null) {
  //     if (temp.isFull == true) {
  //       this.progressBarValue -= 10;
  //       this.isFormGroupFull[6].isFull = false;
  //     }
  //   }
  //   else {
  //     if (temp.isFull != true) {
  //       this.progressBarValue += 10;
  //       this.isFormGroupFull[6].isFull = true;
  //     }
  //   }
  // }
  fillProgressbar(event) {
    // if (event.key != null) {
    let temp = this.isFormGroupFull.find(v => v.name == event.currentTarget.id);
    let index = this.isFormGroupFull.indexOf(temp);
    let isValid = this.personalInformationForm.get(event.currentTarget.id).valid;
    //if the input is valid
    if (isValid == true) {
      if (temp.isFull != true) {
        this.progressBarValue += 17;
        this.isFormGroupFull[index].isFull = true;
      }
    }
    //if the input is not valid
    else {
      //if temp.isFull is already full, we need to reduce 10 precent,
      //and if not we wont add anything till the input will be valid
      if (temp.isFull == true) {
        this.progressBarValue -= 17;
        this.isFormGroupFull[index].isFull = false;
      }
    }
    // }
  }
  filesDropped(event,index) {  
    console.log("hhh",event);
    let reader = new FileReader();
    this.file = event[0].file;
    this.personalInformationForm.get('profpicfile').setValue(this.file);
    reader.readAsDataURL(this.file);
    reader.onload = () => {
      //todo return
      // this.personalInformation.logoPicture = reader.result as string;
      $('#profpic')
        .find('img')
        .attr('src', reader.result as string);
      $('#profile')
        .find('img')
        .attr('src', reader.result as string);
      //this.ProfilePath = reader.result as string
    };
    this.personalInformationForm.markAsDirty();
        this.personalInformationForm.markAsTouched();
    console.log("upload-files",this.file);
} 
  onFileChanged(event) {
    if (event.target.files && event.target.files[0]) {
      if (event.target.files[0].size < this.MAX_SIZE) {
        let reader = new FileReader();
        this.file = event.target.files[0];
        this.personalInformationForm.get('profpicfile').setValue(this.file);
        reader.readAsDataURL(this.file);
        reader.onload = () => {
          //todo return
          // this.personalInformation.logoPicture = reader.result as string;
          $('#profpic')
            .find('img')
            .attr('src', reader.result as string);
          $('#profile')
            .find('img')
            .attr('src', reader.result as string);
          //this.ProfilePath = reader.result as string
        };

        this.personalInformationForm.markAsDirty();
        this.personalInformationForm.markAsTouched();
      }
      else {
        this._toast.add({ severity: 'error', detail: 'יש להעלות קובץ ששוקל עד 300KB' });
      }
    }
  }
  removeLogoImg() {
    $('#profpic')
      .find('img')
      .attr('src', " ");
    $('#profile')
      .find('img')
      .attr('src', this.ProfilePath);
    // this.resOfUpdateBusinnes.logoPictureId = '00000000-0000-0000-0000-000000000000';
    this.logoPictureId = '00000000-0000-0000-0000-000000000000';
    this.personalInformationForm.get('profpicfile').setValue(null);
    this.file = null;
    this.personalInformationForm.markAsDirty();
    this.personalInformationForm.markAsTouched();

  }
  /*avigail = good*/
  RemoveImage(index) {
    const indexPic: number = this.workPictureGuide.findIndex(g => g.picindex == index);
    this['workHasImg' + index] = false;
    $("#" + index).attr('src', " ");
    this.uploadedWorksFiles[index - 1] = null;
    if (indexPic !== -1) {
      this.workPictureGuide.splice(indexPic, 1);
    }
    this.CheckIfAtLeastOnePicExist();
    this.personalInformationForm.markAsDirty();
    this.personalInformationForm.markAsTouched();
  }
  CheckIfAtLeastOnePicExist() {
    let count = 0;
    for (let i = 1; i < 7; i++) {
      if ($("#" + i).attr('src') != null && $("#" + i).attr('src') != " ")
        return true;
      else count++;
    }
    if (count == 6)
      return false;
  }
  clickwantMailByCategory(){
    this.wantMailByCategory=!this.wantMailByCategory
  }
  onCoverFileChanged(event) {
    if (event.target.files && event.target.files[0]) {
      let reader = new FileReader();
      let file = event.target.files[0];
      reader.readAsDataURL(file);
      reader.onload = () => {
        //todo return
        // this.personalInformation.logoPicture = reader.result as string;
        $('#cover-img')
          .find('img')
          .attr('src', reader.result as string);
        // $('#coverpicfile')
        //   .find('img')
        //   .attr('src', reader.result as string);
      };
      this.CoverForm.get('coverpicfile').setValue(file);
      this.openDeleteCoverImg = true;
      this.personalInformationForm.markAsDirty();
      this.personalInformationForm.markAsTouched();
    }
  }
  triggerInput() {
    $('#profpicfile').trigger('click');
  }
  //פתיחת תקיה לבחירת קובץ - בכובר
  triggerInputCover() {
    $('#coverpicfile').trigger('click');
  }
  //הסרה של תמונת השער
  triggerInputCoverRemove() {
    // $('#coverpicfile').trigger('click');
    $('#cover-img')
      .find('img')
      // .css('display', 'none');
      .attr('src', " ")
    $('#coverpicfile')
      .find('img')
      // .css('display', 'none');
      .attr('src', " ")
    this.openDeleteCoverImg = false;
    this.personalInformationForm.markAsDirty();
    this.personalInformationForm.markAsTouched();
  }
  //work pic = open the trigger
  triggerWorkInput(index) {
    $('#workpicfile' + index).trigger('click');

  }
  //work pic upload
  onWorkFileChanged(event, index) {
    if (event.target.files && event.target.files[0]) {
      if (event.target.files[0].size < this.MAX_SIZE) {
        let reader = new FileReader();
        this['workHasImg' + index] = true;
        let file = event.target.files[0];
        reader.readAsDataURL(file);
        reader.onload = () => {
          $('#' + index).attr('src', reader.result as string);
          this.uploadedWorksFiles[index - 1] = <File>event.target.files[0];
          const indexG: number = this.workPictureGuide.findIndex(g => g.picindex == index);
          if (indexG !== -1) {
            this.workPictureGuide.splice(indexG, 1);
          }
          //הוספת האינדקס לרשימה
          this.workPictureGuide.push({
            picindex: index,
            picName: this.uploadedWorksFiles[index - 1].name,
          })
        };

        this.personalInformationForm.markAsDirty();
        this.personalInformationForm.markAsTouched();
      }
      else {
        this._toast.add({ severity: 'error', detail: 'יש להעלות קובץ ששוקל עד 300KB' })
      }
    }
  }
  getFontSizeByLenght() {
    if (!this.personalInformationForm.get('buisnessName').value) {
      return '3rem';
    }
    else {
      let buisnessName = (this.personalInformationForm.get('buisnessName').value as string).split(' ');
      let longWord = buisnessName.find(x => x.length > 10)
      if (longWord) {
        if (longWord.length > 25)
          return '1.3rem';
        if (longWord.length > 23)
          return '1.4rem';
        if (longWord.length > 21)
          return '1.5rem';
        if (longWord.length > 20)
          return '1.7rem';
        if (longWord.length > 18)
          return '1.8rem';
        if (longWord.length > 16)
          return '1.9trem';
        if (longWord.length > 14)
          return '2.25rem';
        if (longWord.length > 12)
          return '2.4rem';
        if (longWord.length > 10)
          return '2.5rem';
      }
      return '3rem';
    }
  }
  resetForm() {
    this.isToOpenCategory2 = false;
    this.isToOpenCategory3 = false;
    this.isToOpenCategory4 = false;
    this._wrapperSearchService.categoriesForChoose.forEach(x => x.disabled == false);
    if (this.cameFromUpdate == true)//if we came from update we want to insert the last data
    {
      this.categoryOptionsAfterChoose = [];
      this.InsertAllValueFromUpdate(this.resOfUpdateBusinnes);
    }
    else {//if this user create now the businnes - we want to dalete all the data
      this.personalInformationForm.get('isburterBuisness').setValue(false);//בארטר
      this.personalInformationForm.get('ispayingBuisness').setValue(false);//תשלום
      this.personalInformationForm.get('iscollaborationBuisness').setValue(false);//בשתפ
      this.personalInformationForm.get('isAllServiceForBurter').setValue(true);//האם בארטר מורשה בהכל?
      this.personalInformationForm.get('OptionalCollaborationDescription').setValue('');//האם בארטר מורשה בהכל?
      this.personalInformationForm.get('actionDiscription').setValue('');//סלוגן
      this.personalInformationForm.get('discription').setValue('');//תאור
      this.personalInformationForm.get('buisnessName').setValue('');//שם העסק
      this.personalInformationForm.get('buisnessEmailAddress').setValue('');//מייל עסק
      this.personalInformationForm.get('buisnessWebSiteLink').setValue('');//לינק
      this.personalInformationForm.get('phoneNumber1').setValue('');//פלאפון 1
      this.personalInformationForm.get('phoneNumber2').setValue('');//פלאפון 2
      this.personalInformationForm.get('address').setValue('');//כתובת
      this.personalInformationForm.get('product1').setValue('');//מוצר 1
      this.personalInformationForm.get('product2').setValue('');//מוצר 2
      this.personalInformationForm.get('BarterProduct1').setValue('');//מוצר לבארטר 1
      this.personalInformationForm.get('BarterProduct2').setValue('');//מוצר לבארטר 2
      this.personalInformationForm.get('isInAllCountry').setValue(true);//ערים
      this.personalInformationForm.get('profpicfile').setValue(null);
      this.isCountryCollapsedOpen = true;//שהדרופ דאון יהיה סגור
      this.ModelArea = []; //אתחול הערים
      this.triggerInputCoverRemove();

      $('#profpic').find('img').attr('src', " ");//תמונת פרופיל
      $('#profpic').find('img').prop('content', " ")//תמונת פרופיל
      $('#profile').find('img').attr('src', this.ProfilePath);//תמונת פרופיל בצד
      // $('#profile').find('img').attr(" ");//תמונת פרופיל בצד

      //remove from html        
      $('#1').find('img').attr('src', null)
      $('#2').find('img').attr('src', null)
      $('#3').find('img').attr('src', null)
      $('#4').find('img').attr('src', null)
      $('#5').find('img').attr('src', null)
      $('#6').find('img').attr('src', null)
      this.uploadedWorksFiles = [];//reset all the files
      this.workPictureGuide = [];//delet the guide list
      //איפוס קטגוריות
      for (let i = 1; i < 5; i++) {
        //איתחול הקטגוריות - המערך עצמו
        this['Category' + i] = { label: null, value: null };
        //איפוס הערך של הקטגוריה בטופס
        this.personalInformationForm.get('Category' + i).setValue(this['Category' + i]);
        //איפוס התת קטגוריות - אם הוא כבר בחר
        this['ModelSubCategory' + i] = [];
        //איפוס התת קטגוריות של הבארטר
        this['ModelSubCategoryForBarter' + i] = [];
        //איפוס התת קטגוריות מהטופס
        this.personalInformationForm.get('SubCategory' + i).setValue(this['ModelSubCategory' + i]);
        //איפוס התת קטגוריות של הבארטר מהטופס
        this.personalInformationForm.get('SubCategoryForBarter' + i).setValue(this['ModelSubCategory' + i]);
        //איפוס האופציות בראפר
        this._wrapperSearchService['subCategoryOptions' + i] = [];
        //סגירת הקולאפסים
        this.isCollapsed[i] = true;
        //סגירת התחום של הקטגוריות
        if (i > 1) {
          this['isToOpenCategory' + i] = false;
        }
      }
      this.buisnessBarterCategory1 = [];
      this.buisnessBarterCategory2 = [];
      this.personalInformationForm.get('CategoryForBarter1').setValue(this.buisnessBarterCategory1);//קטגוריה לבארטר 1
      this.personalInformationForm.get('CategoryForBarter2').setValue(this.buisnessBarterCategory2);//קטגוריה לבארטר 2
      this.isFormGroupFull[0].isFull = false;
      this.isFormGroupFull[1].isFull = false;
      this.isFormGroupFull[2].isFull = false;
      this.isFormGroupFull[3].isFull = false;
      this.isFormGroupFull[4].isFull = false;
      this.isFormGroupFull[5].isFull = false;
      this.progressBarValue = 0;
      this.isBarterCollapsedOpen = true;
      this.logoPictureId = '00000000-0000-0000-0000-000000000000';
      // categoriesForChoose: { label: number; value: string, disabled?: boolean }[] = [];

      //this.setDisableOfChoosedCategory();
      // this._wrapperSearchService.categoriesForChoose.forEach(res=>res.disabled=false);
      //this._wrapperSearchService.categoriesForChoose=[];
      this.categoryOptionsAfterChoose = [];
      this.file = null;
    }

  }
  addetionToSubmit() {
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~מילוי הקוגוריות במודל שישלח
    //list of subcategory 1
    this.personalInformation.buisnessCategory1 = [];
    this.personalInformation.buisnessCategory2 = [];
    this.personalInformation.buisnessCategory3 = [];
    this.personalInformation.buisnessCategory4 = [];
    this.personalInformation.buisnessBarterCategory1 = [];
    this.personalInformation.buisnessBarterCategory2 = [];
    this.personalInformation.buisnessAreaList1 = [];
    var listwithoutD = [];
    this.ModelSubCategory1.forEach(cat => {
      var res = listwithoutD.find(sc => sc.CategorySubCategoryId == cat.CategorySubCategoryId)
      if (res == null)
        listwithoutD.push(cat)
    });
    this.ModelSubCategory1 = listwithoutD;
    //2
    listwithoutD = [];
    this.ModelSubCategory2.forEach(cat => {
      var res = listwithoutD.find(sc => sc.CategorySubCategoryId == cat.CategorySubCategoryId)
      if (res == null)
        listwithoutD.push(cat)
    });
    this.ModelSubCategory2 = listwithoutD;
    //3
    listwithoutD = [];
    this.ModelSubCategory3.forEach(cat => {
      var res = listwithoutD.find(sc => sc.CategorySubCategoryId == cat.CategorySubCategoryId)
      if (res == null)
        listwithoutD.push(cat)
    });
    this.ModelSubCategory3 = listwithoutD;
    //4
    listwithoutD = [];
    this.ModelSubCategory4.forEach(cat => {
      var res = listwithoutD.find(sc => sc.CategorySubCategoryId == cat.CategorySubCategoryId)
      if (res == null)
        listwithoutD.push(cat)
    });
    this.ModelSubCategory4 = listwithoutD;
    //barter 1
    listwithoutD = [];
    this.buisnessBarterCategory1.forEach(cat => {
      var res = listwithoutD.find(sc => sc.CategorySubCategoryId == cat.CategorySubCategoryId)
      if (res == null)
        listwithoutD.push(cat)
    });
    this.buisnessBarterCategory1 = listwithoutD;
    //barter 2
    listwithoutD = [];
    this.buisnessBarterCategory2.forEach(cat => {
      var res = listwithoutD.find(sc => sc.CategorySubCategoryId == cat.CategorySubCategoryId)
      if (res == null)
        listwithoutD.push(cat)
    });
    this.buisnessBarterCategory2 = listwithoutD;
    //insert the data
    if (this.ModelSubCategory1 != null) {
      this.ModelSubCategory1.forEach(ThisCategoryList => {
        if (this.personalInformationForm.get('isAllServiceForBurter').value == true) {
          var Barter = true;
        }
        //אם הרשימה ריקה אז היא לא מאפשרת בארטר בכלל
        else if (this.ModelSubCategoryForBarter1 == null) {
          Barter = false;
        }
        else {
          Barter = false;
          this.ModelSubCategoryForBarter1.forEach(batrer => {
            if (batrer.CategorySubCategoryId == ThisCategoryList.CategorySubCategoryId) {
              Barter = true;
            }
          })
        }
        this.personalInformation.buisnessCategory1.push({
          businessId: null,
          categoryId: this.categoryOptionsAfterChoose[0].label,
          subCategoryId: ThisCategoryList.Id,
          combinationtId: ThisCategoryList.CategorySubCategoryId,

          isPossibleInBarter: Barter,
        })
      });
    }
    //list of subcategory 2
    if (this.ModelSubCategory2 != null) {
      this.ModelSubCategory2.forEach(ThisCategoryList => {
        if (this.personalInformationForm.get('isAllServiceForBurter').value == true) {
          var Barter = true;
        }
        //אם הרשימה ריקה אז היא לא מאפשרת בארטר בכלל
        else if (this.ModelSubCategoryForBarter2 == null) {
          Barter = false;
        }
        else {
          Barter = false;
          this.ModelSubCategoryForBarter2.forEach(batrer => {
            if (batrer.CategorySubCategoryId == ThisCategoryList.CategorySubCategoryId) {
              Barter = true;
            }
          })
        }
        this.personalInformation.buisnessCategory2.push({
          businessId: null,
          categoryId: this.categoryOptionsAfterChoose[1].label,
          combinationtId: ThisCategoryList.CategorySubCategoryId,
          subCategoryId: ThisCategoryList.Id,

          isPossibleInBarter: Barter
        })
      });
    }
    //list of subcategory 3
    if (this.ModelSubCategory3 != null) {
      this.ModelSubCategory3.forEach(ThisCategoryList => {
        if (this.personalInformationForm.get('isAllServiceForBurter').value == true) {
          var Barter = true;
        }
        //אם הרשימה ריקה אז היא לא מאפשרת בארטר בכלל
        else if (this.ModelSubCategoryForBarter3 == null) {
          Barter = false;
        }
        else {
          Barter = false;
          this.ModelSubCategoryForBarter3.forEach(batrer => {
            if (batrer.CategorySubCategoryId == ThisCategoryList.CategorySubCategoryId) {
              Barter = true;
            }
          })
        }
        this.personalInformation.buisnessCategory3.push({
          businessId: null,
          categoryId: this.categoryOptionsAfterChoose[2].label,
          combinationtId: ThisCategoryList.CategorySubCategoryId,
          subCategoryId: ThisCategoryList.Id,

          isPossibleInBarter: Barter
        })
      });
    }
    //list of subcategory 4
    if (this.ModelSubCategory4 != null) {
      this.ModelSubCategory4.forEach(ThisCategoryList => {
        if (this.personalInformationForm.get('isAllServiceForBurter').value == true) {
          var Barter = true;
        }
        //אם הרשימה ריקה אז היא לא מאפשרת בארטר בכלל
        else if (this.ModelSubCategoryForBarter4 == null) {
          Barter = false;
        }
        else {
          Barter = false;
          this.ModelSubCategoryForBarter4.forEach(batrer => {
            if (batrer.CategorySubCategoryId == ThisCategoryList.CategorySubCategoryId) {
              Barter = true;
            }
          })
        }
        this.personalInformation.buisnessCategory4.push({
          businessId: null,
          categoryId: this.categoryOptionsAfterChoose[3].label,
          combinationtId: ThisCategoryList.CategorySubCategoryId,
          subCategoryId: ThisCategoryList.Id,

          isPossibleInBarter: Barter
        })
      });
    }
    //~~~~~~~~~~~~~~barter:
    //!~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~barter1
    if (this.buisnessBarterCategory1 != null) {
      this.buisnessBarterCategory1.forEach(ThisCategoryList => {
        // if (this.personalInformation.buisnessBarterCategory1 == null) {
        //   this.personalInformation.buisnessBarterCategory1 = [];
        // }
        this.personalInformation.buisnessBarterCategory1.push({
          businessId: null,
          categoryId: this.CForBarter1.label,
          combinationtId: ThisCategoryList.CategorySubCategoryId,
          subCategoryId: ThisCategoryList.Id
        })
      });
    }
    //!~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~barter2
    if (this.buisnessBarterCategory2 != null) {
      this.buisnessBarterCategory2.forEach(ThisCategoryList => {
        // if (!this.personalInformation.buisnessBarterCategory2) {
        //   this.personalInformation.buisnessBarterCategory2 = [];
        // }
        this.personalInformation.buisnessBarterCategory2.push({
          businessId: null,
          categoryId: this.CForBarter2.label,
          combinationtId: ThisCategoryList.CategorySubCategoryId,
          subCategoryId: ThisCategoryList.Id
        })
      });
    }
    // מעבר על רשימת הערים שהתקבלה ולהכניס אותם למודל
    //אם היא בחרה כל הארץ ועוד דברים נכניס רק כל הארץ
    if (this.personalInformationForm.get('isInAllCountry').value == true) {
      if (!this.personalInformation.buisnessAreaList1)
        this.personalInformation.buisnessAreaList1 = [];
      this.personalInformation.buisnessAreaList1.push({
        areaId: 1
      })
    }
    //רק אם היא לא בחרה את כל הארץ
    else if (this.personalInformationForm.get('isInAllCountry').value == false) {
      this.ModelArea.forEach(Marea => {
        if (this.personalInformation.buisnessAreaList1.findIndex(id => Marea.id == id.areaId) == -1) {
          this.personalInformation.buisnessAreaList1.push({
            areaId: Marea.id
          });
        }
      })
    }
  }

  //check validation:
  checkValidation() {

    let valid: Boolean = false;
    valid = !this.checkValidationForBusinesOption();
    if (this.personalInformationForm.valid && valid == false && this.checkIfDirty()) {
      
      return false;
    }
    else {
      if (!this.checkIfDirty()) {
        this.submitToolTip = "עדין לא נערכו שינויים בטופס!"

      }
      else {
        this.submitToolTip = "אין אפשרות לעדכן יש פרטים שעדיין לא הושלמו!"

      }
      return true;
    }
  }
  //בדיקה בשביל ה  prossesbar
  // אם הוא סימן אחת מ 3 האפשרויות להוסיף לו אחוזים
  checkValidationForBusinesOptionToProssesBar() {
    if (this.personalInformationForm.get('ispayingBuisness').value == true
      || this.personalInformationForm.get('isburterBuisness').value == true
      || this.personalInformationForm.get('iscollaborationBuisness').value == true) {
      return true
    }
    return false;
  }

  onChangeBusinesOption(newvalue) {
    let a = this.checkValidationForBusinesOptionToProssesBar()
    console.log(newvalue)
    let temp = this.isFormGroupFull.find(v => v.name == "buisnessOption");
    if (newvalue.checked == false) {
      if (temp.isFull == true || a == false) {
        this.progressBarValue -= 15
        this.isFormGroupFull[5].isFull = false;
      }
    }
    else {
      if (temp.isFull != true) {
        this.progressBarValue += 15;
        this.isFormGroupFull[5].isFull = true;
      }
    }
  }
  //בדיקת ולידציה על האפשריות של העסק - בארטר ,תשלום ושתפ
  checkValidationForBusinesOption() {
    if (this.personalInformationForm.get('ispayingBuisness').value == false
      && this.personalInformationForm.get('isburterBuisness').value == false
      && this.personalInformationForm.get('iscollaborationBuisness').value == false) {
      return false
    }
    return true;
  }
  checkIfDirty() {
    if (this.personalInformationForm.dirty && this.personalInformationForm.touched) {
      return true;
    }
    return false;
  }
  //submit
  onSubmitwithFileAndModel() {
    if (this.ifSubmitClicked || this.ifScoringCliked) {
      return;
    }
    this.ifSubmitClicked = true;
    //TODO : 1 add full bus. form to the formdata obj 2 add list of imgs to request 3 set value od profile pic to get curreft img
    this.personalInformation.id = this.personalInformationForm.get('idBusiness').value;
    this.personalInformation.userId = localStorage.getItem("logInUserEmail");//userId
    this.personalInformation.buisnessName = this.personalInformationForm.get('buisnessName').value;//buisnessName 
    this.personalInformation.isdisplayBusinessOwnerName = this.personalInformationForm.get('isdisplayBusinessOwnerName').value;//האם להציג את שם בעלת העסק
    this.personalInformation.phoneNumber1 = this.personalInformationForm.get('phoneNumber1').value;//1 טלפונים
    this.personalInformation.phoneNumber2 = this.personalInformationForm.get('phoneNumber2').value;//2 טלפונים        
    this.personalInformation.address = this.personalInformationForm.get('address').value;//כתובת
    this.personalInformation.actionDiscription = this.personalInformationForm.get('actionDiscription').value;//סלוגן
    this.personalInformation.discription = this.personalInformationForm.get('discription').value;//תיאור
    let webSiteLink: string = this.personalInformationForm.get('buisnessWebSiteLink').value;//לינק לאתר

    if (webSiteLink) {
      //webSiteLink = webSiteLink.toLowerCase();
      webSiteLink = webSiteLink.replace("https://", '');
      webSiteLink = webSiteLink.replace("HTTPS://", '');
      webSiteLink = webSiteLink.replace("www.", '');
      webSiteLink = webSiteLink.replace("WWW.", '');
      webSiteLink = webSiteLink.replace("http://", '');
      this.personalInformation.buisnessWebSiteLink = "https://" + webSiteLink;
    }
    else {
      this.personalInformation.buisnessWebSiteLink = "";
    }
    this.personalInformation.businessEmailAddress = this.personalInformationForm.get('buisnessEmailAddress').value;//מייל העסק
    this.personalInformation.ispayingBuisness = this.personalInformationForm.get('ispayingBuisness').value;//האם שיטת העסק היא לפי תשלום?
    this.personalInformation.isburterBuisness = this.personalInformationForm.get('isburterBuisness').value;//האם העסק הוא בשיטת בארטר?
    this.personalInformation.iscollaborationBuisness = this.personalInformationForm.get('iscollaborationBuisness').value;//האם העסק פועל בשת"פ
    this.personalInformation.isburterPossibleInAllCategory = this.personalInformationForm.get('isAllServiceForBurter').value;//האם העסק מרשה את כל השרותים כבארטר
    this.personalInformation.OptionalCollaborationDescription = this.personalInformationForm.get('OptionalCollaborationDescription').value;//האם העסק מרשה את כל השרותים כבארטר
    this.personalInformation.isopenToSuggestionsForBarter = this.personalInformationForm.get('IWantToOpenCategoryToGetBarter').value;//פתוחה להצעות מה שאני אקבל
    this.personalInformation.product1 = this.personalInformationForm.get('product1').value;//מוצר ראשון
    this.personalInformation.product2 = this.personalInformationForm.get('product2').value;//מוצר שני
    this.personalInformation.barterProduct1 = this.personalInformationForm.get('BarterProduct1').value;//מוצר ראשון לבארטר
    this.personalInformation.barterProduct2 = this.personalInformationForm.get('BarterProduct2').value;//מוצר שני לבארטר
    this.personalInformation.lastupdatedStartDate = null;
    this.personalInformation.coverPictureId = this.coverPictureId;
    this.personalInformation.logoPictureId = this.logoPictureId;
    this.personalInformation.workPictureGuide = this.workPictureGuide;
    this.personalInformation.WantedGetHelpNotification = this.personalInformationForm.get('WantedGetHelpNotification').value;
    this.personalInformation.WantedGetDailyNotification = this.personalInformationForm.get('WantedGetDailyNotification').value;
    // TODO : esti - add form array of
    this.personalInformation.BusinessCategoriesNotify = [];
    this.BusinessCategoriesNotify.value.forEach((element,index) => {
      if(element==true){
       this.personalInformation.BusinessCategoriesNotify.push({BusinessId: this.personalInformation.id, categoryId: this['Category'+(index+1)].label, Id: 0});
      }
   });
    if(!this.personalInformation.Score){
      this.personalInformation.Score = 0;

    }
    this.personalInformation.ScoreStatus = 0;
    this.addetionToSubmit();
    const formData = new FormData();//create form data
    //insert the pictures
    formData.append('coverPicture', this.CoverForm.get('coverpicfile').value ? this.CoverForm.get('coverpicfile').value : null);
    formData.append('logoPicture', this.personalInformationForm.get('profpicfile').value ? this.personalInformationForm.get('profpicfile').value : null);
    //insert the array pic
    for (let file of this.uploadedWorksFiles) {
      formData.append('files', file);
    }

    //insert the model
    formData.append("model", JSON.stringify(this.personalInformation));
    debugger
    this.isLoading = true;
console.log("loading=" , this.isLoading);

    //send to the controllers
    return this.httpClient.post(this.apiBaseUrl + `/api/Buisness/createBuisnessWithFiles`, formData,
      {
        headers: { 'Content-Type': [] }, reportProgress:
          true
      }).subscribe((res: number) => {
        if (res != null) {
          this.personalInformationForm.markAsUntouched();
          this.submitToolTip = "עדין לא נערכו שינויים בטופס!";
          this.ifSubmitClicked = false;
          //מה קורה אם הצליח ליצור או לעדכן:
          if (res > 0 && this.personalInformation.id == 0) {
            this.personalInformationForm.get('idBusiness').setValue(res);
            this.personalInformation.id = res;
            Swal.fire({
              icon: 'success',
              title: 'עשינו עסק!',
              html:
                'כרטיס העסק שלך נשמר במאגר. עכשיו כל בעלת עסק יכולה להכיר את העסק שלך, להתרשם מהשירותים שאת מציעה ולדעת כל מה שבחרת לספר.',
              showCancelButton: true,
              confirmButtonColor: '#3085d6',
              cancelButtonText: 'יופי, תודה!',
              confirmButtonText: 'אני רוצה לראות את כרטיס העסק שלי'
            }).then((result) => {
              if (result.isConfirmed) {
                this.router.navigate(['/business-view', this.acct.Email.value]);
              }
            });
            this.acct.currentBusiness.next({
              id: this.personalInformation.id,
              logoPictureId: this.personalInformation.logoPictureId,
              userId: this.personalInformation.userId,
              buisnessName: this.personalInformation.buisnessName
            });
          }

          else if (res > 0 && this.personalInformation.id > 0) {
            this.isLoading = false;
            console.log("loading=" , this.isLoading);
        
            Swal.fire({
              icon: 'success',
              title: 'כרטיס העסק שלך עודכן!',
              html:
                'מי שתצפה בו תקבל את כל המידע שבחרת להציג',
              showCancelButton: true,
              confirmButtonColor: '#3085d6',
              cancelButtonText: 'יופי, תודה!',
              confirmButtonText: 'אני רוצה לראות את הכרטיס המעודכן',


            }).then((result) => {
              if (result.isConfirmed) {
                this.router.navigate(['/business-view', this.acct.Email.value]);
              }

            })
            this.router.navigate(['/barter-List']);
          }
          else {
            this.isLoading = false;
            console.log("loading=" , this.isLoading);
        
            Swal.fire({
              icon: 'info',
              title: 'הודעה:',
              html:
                'אנו מצטערים אך אירעה תקלה',
              showClass: {
                popup:
                  'עבר בהצלחה'
              }
            })
          }
        }
      },
        err => {
          this.isLoading = false;
          console.log("loading=" , this.isLoading);
      
          this.ifSubmitClicked = false;
          //לא הצליח
          Swal.fire({
            icon: 'error',
            title: 'הייתה בעיה ביצירת/בעדכון העסק',
            html:
              'אנא בדוק שנית שכל הפרטים נכונים',
            showClass: {
              popup:
                'אנא בדוק שוב'
            }
          })
        }
      );

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
      case 4:
        this.isOpen4 = !this.isOpen4;
        collapse.toggle();
        break;
        case 5:
          this.isOpen5 = !this.isOpen5;
          collapse.toggle();
          break;
      default: break
    }

  }
  seeScoreDetails(event :Event){
      this.__funcService.OpenScoringDetail(this.acct.currentBusiness.value.id).subscribe(res=>{
        this.ifScoringCliked = false;
      }) ;
      this.ifScoringCliked = true;
  }
}
