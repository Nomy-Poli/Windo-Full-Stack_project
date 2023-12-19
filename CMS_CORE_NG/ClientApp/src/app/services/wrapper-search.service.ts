import { Injectable } from '@angular/core';
import { BehaviorSubject, observable, Observable, Subscription } from 'rxjs';
import { map } from 'rxjs/operators';
import { AccountService } from './account.service';
import { AreaVm, BuisnessAreaVm, BuisnessCategoryVm, BuisnessService, BuisnessVm, BusinessForCardVM, BusinessNamesPicUserIdVM } from './Buisness.service';
import { BoardVM, NoteService, NoteVM } from './Note.service';
import { CategorySubCategoryVm, CategoryVm, SearchCategoryService, SubCategoryVm } from './SearchCategory.service';
@Injectable({
    providedIn: 'root'
})
export class WrapperSearchService {
    numberOfCurrentPage: number;
    totalnumberOfPages: number;
    numberOfCardsInOnePage: number = 8;
    totalnumberOfCards: number;
    // false-create
    // true-update
    flagCreateOrUpdate: boolean = false;
    categoryOptions: { label: number; value: string }[] = [];
    categoriesForChoose: { label: number; value: string, disabled?: boolean }[] = [];
    subCategoryOptions1: SubCategoryVm[] = [];//profile page
    subCategoryOptions2: SubCategoryVm[] = [];//profile page
    subCategoryOptions3: SubCategoryVm[] = [];//profile page
    subCategoryOptions4: SubCategoryVm[] = [];//profile page
    subCategoryOptionForBarter1: SubCategoryVm[] = [];//profile page
    subCategoryOptionForBarter2: SubCategoryVm[] = [];//profile page
    subCategoryForSearchPageOptions: SubCategoryVm[] = [];//barter list search page להצגה
    ListOfSubCategoryToFillter: SubCategoryVm[] = [];    //רשימת התת קטגוריות שנבחרו - לפילטור
    //סינון לפי אפשרויות עסקה
    ispayingBuisness = true;
    isburterBuisness = true;
    iscollaborationBuisness = true;

    // IsBarterListComm: boolean = false;
    HomePage$ = new BehaviorSubject<boolean>(true);
    // notHomePage$:new BehaviorSubject<boolean>(false);
    onListComponnent$ = new BehaviorSubject<boolean>(false)
    //תמונות בשביל הפרסומת
    images: any[] = [
        { img: '../../assets/3.jpg', index: 1, name: 'one', visible: false, isb: true, iss: false, isp: false },
        { img: '../../assets/1.jpg', index: 2, name: 'two', visible: false, isb: false, iss: true, isp: true },
        { img: '../../assets/4.jpg', index: 3, name: 'three', visible: false, isb: true, iss: false, isp: false },
        { img: '../../assets/2.jpg', index: 4, name: 'four', visible: false, isb: false, iss: true, isp: true },
        { img: '../../assets/5.jpg', index: 5, name: 'five', visible: false, isb: true, iss: false, isp: false },
        { img: '../../assets/3.jpg', index: 6, name: 'six', visible: false, isb: false, iss: true, isp: false },
        { img: '../../assets/1.jpg', index: 7, name: 'seven', visible: false, isb: true, iss: false, isp: true },
        { img: '../../assets/4.jpg', index: 8, name: 'ate', visible: false, isb: false, iss: true, isp: false },
        { img: '../../assets/2.jpg', index: 9, name: 'nine', visible: false, isb: true, iss: false, isp: true },
        { img: '../../assets/5.jpg', index: 10, name: 'ten', visible: false, isb: false, iss: true, isp: false },
        { img: '../../assets/3.jpg', index: 11, name: 'eleven', visible: false, isb: true, iss: false, isp: true },
        { img: '../../assets/1.jpg', index: 12, name: 'twelv', visible: false, isb: false, iss: true, isp: false }
    ];
    public _imagesListSubject: BehaviorSubject<Array<any>> =
        new BehaviorSubject(this.images.slice(0, 5));
    imagesList$ = this._imagesListSubject.asObservable();

    LoginStatus$ = new BehaviorSubject<boolean>(null);
    newMessages$ = new BehaviorSubject<number>(0);
    Username$: Observable<string>;

    responsiveOptions: any[] = [
        {
            breakpoint: '1024px',
            numVisible: 5
        },
        {
            breakpoint: '768px',
            numVisible: 3
        },
        {
            breakpoint: '560px',
            numVisible: 1
        }
    ];
    //the id of the user that loged in - used for the profile component
    // public _logInUserIdSubject: BehaviorSubject<string> = new BehaviorSubject(null);
    // logInUserId$ = this._logInUserIdSubject.asObservable();

    //list of buisneses, initialized in the init of the component and never changed
    public globalBuisnessList: BuisnessVm[];
    //list of buisneses cointains name email logo only, initialized in the init of the component and never changed
    public shrunkenBuisnessList: BusinessNamesPicUserIdVM[];

    //list of buisneses after filter
    public _afterFilterBuisnessListSubject: BehaviorSubject<Array<BuisnessVm>> = new BehaviorSubject(null);
    afterFilterBuisnessList$ = this._afterFilterBuisnessListSubject.asObservable();
    
    public parameterSearch;
    //list of buisneses after filter, but contains only the buisnesses of the current page
    public _buisnessListPerPageSubject: BehaviorSubject<Array<BuisnessVm>> = new BehaviorSubject(null);
    buisnessListPerPage$ = this._buisnessListPerPageSubject.asObservable();

    //list of categoreis for the aside menu
    public _listOfCategoriesSubject: BehaviorSubject<Array<CategoryVm>> = new BehaviorSubject(null);
    listOfCategories$ = this._listOfCategoriesSubject.asObservable();

    public _listOfSubCategoriesSubject: BehaviorSubject<Array<SubCategoryVm>> = new BehaviorSubject(null);
    listOfSubCategories$ = this._listOfCategoriesSubject.asObservable();

    ///list of Suggestion for the serach autocomplete input
    public _listOfSuggestionSubject: BehaviorSubject<Array<any>> = new BehaviorSubject(null);
    listOfSuggestion$ = this._listOfSuggestionSubject.asObservable();

    //list of latest buisneses for home page-never changed
    public globalLatestBuisnessListForHomePage: BusinessForCardVM[];

    //list of latest buisneses for home page
    public _latestBuisnessListSubject: BehaviorSubject<Array<BusinessForCardVM>> = new BehaviorSubject(null);
    latestBuisnessList$ = this._latestBuisnessListSubject.asObservable();
    lengthLatestBuisnessList: number;

    // public _listOfSuggestionSubject: BehaviorSubject<Array<{ type: string, value: string }>> = new BehaviorSubject(null);
    // public listOfSelectedOptions: { type: string, value: string }[] = [];

    //list of latest notes
    public _latestNotesListSubject: BehaviorSubject<Array<BoardVM>> = new BehaviorSubject(null);
    public _notesListSubject: BehaviorSubject<Array<BoardVM>> = new BehaviorSubject(null);

    public boardList: BoardVM[] = [];

    public listOfSelectedOptions: any[] = [];
    flagToAddBusinnes: boolean = false;
    tempSuggestionsList: Array<any> = [];
    tempCategoryVm: Array<CategoryVm> = [];
    isCollapsed: boolean[] = [];
    temp: CategoryVm = {};

    stemp: SubCategoryVm = {};

    AreaOptions: AreaVm[];
    AreaOptionsProfile: AreaVm[];

    constructor(public _searchService: SearchCategoryService, private _noteService: NoteService,
        public _buisnessService: BuisnessService, private _acct: AccountService) {
        if (localStorage.getItem("logInUserId")) {
            this.LoginStatus$.next(true);
        }
    }

    currentBusiness$: Observable<BusinessNamesPicUserIdVM>;

    getCurrentBusiness(): Observable<BusinessNamesPicUserIdVM> {
        if (this._acct.currentBusiness.value) {
            return this._acct.currentBusiness.asObservable();
        }
        else {
            if (!this.currentBusiness$) {
                this.currentBusiness$ = this._buisnessService.getCurrentBuisnessByEmail(this._acct.Email.value)
                    .pipe(map((result) => {
                        this._acct.currentBusiness.next(result);
                        this.currentBusiness$ = null;
                        return result;
                    }));
            }
            return this.currentBusiness$;
        }

    }
    getLatestNotes(): Observable<BoardVM[]> {
        if (this._latestNotesListSubject.value) {
            return this._latestNotesListSubject.asObservable();
        }
        else {
            this._noteService.getLatestNotes().subscribe(res => {
                this._latestNotesListSubject.next(res);
            });
        }
    }

    getLatestBuisnesses() {
        this._buisnessService.getListOfLatestUpdatedBuisnesses().subscribe(res => {
            if (res) {
                let i = 1;
                //todo ליעל!!!!
                res.forEach(element => {
                    element.index = i;
                    i++;
                    element.listOfAll4buisnessCategory = [...element.buisnessCategory1, ...element.buisnessCategory2, ...element.buisnessCategory3, ...element.buisnessCategory4];
                });
                this.globalLatestBuisnessListForHomePage = res;
                this.lengthLatestBuisnessList = res.length;
                this._latestBuisnessListSubject.next(res.slice(0, 4));
            }
        })
    }
    subscribeBusinesList() {
        this._afterFilterBuisnessListSubject = new BehaviorSubject(null);
        this._afterFilterBuisnessListSubject.subscribe(result => {
            if (result) {
                this.numberOfCurrentPage = 1;
                //מספר הכרטיסים הגלובלי
                this.totalnumberOfCards = result.length;
                // מספר העמודים = מספר העסקים הכללי חלקי מספר העסקים שבכל עמוד
                //to get the integer number and not the float
                let tempNumPages = Math.floor(result.length / this.numberOfCardsInOnePage);
                this.totalnumberOfPages =
                    tempNumPages < result.length / this.numberOfCardsInOnePage ? tempNumPages + 1 : tempNumPages;
                //the number of the current page
                // this._wrapperSearchService.numberOfCurrentPage = result.length == 0 ? 0 : 1;
            }
        });
    }
    //to Fill out the list of the aside manue and the suggestions list in barter list component
    getCategoriesAndSubCategoryForAsideAndSuggestions() {
        this.temp.subCategoryList = [];
        this._searchService.getListOfCategorySubCategoryVm().subscribe((result) => {
            if (result) {
                // let temp;           
                let tempIds = [];
                let tempFlag = false;
                result.forEach(Celement => {
                    // ~~~~~הכנסת כל התתי קטגוריות לרשימה אחת~~~~~~~
                    //insert the all sub category:
                    var subCat: SubCategoryVm = {
                        CategorySubCategoryId: Celement.id,
                        Id: Celement.subCategoryId,
                        name: Celement.subCategoryName,
                        // includesToFillter: false
                    }
                    this.subCategoryForSearchPageOptions.push(subCat);//רשימת כל התתי קטגוריות
                    // ~~~~~~~~~~~~~~~~~~~~~~~
                    this.tempSuggestionsList.push(Celement);
                    this.temp = {};
                    this.temp.subCategoryList = [];
                    tempIds.forEach(e => {
                        if (Celement.categoryId == e)
                            tempFlag = true;
                    });
                    if (tempFlag == false) {
                        tempIds.push(Celement.categoryId);
                        this.temp.Id = Celement.categoryId;
                        this.temp.name = Celement.categoryName;
                        // let stemp;
                        result.forEach(SCelement => {
                            if (SCelement.categoryId == Celement.categoryId) {
                                this.stemp = {};
                                this.stemp.CategorySubCategoryId = SCelement.id;
                                this.stemp.Id = SCelement.subCategoryId;
                                this.stemp.name = SCelement.subCategoryName;
                                this.temp.subCategoryList.push(this.stemp);
                            }
                        });
                        this.tempCategoryVm.push(this.temp);
                    }
                    tempFlag = false;
                });
            }
            this._listOfCategoriesSubject.next(this.tempCategoryVm);//רשימת הקטגוריות
            this._listOfSubCategoriesSubject.next(this.subCategoryForSearchPageOptions);
            this._listOfSuggestionSubject.next(this.tempSuggestionsList);
        });
    }
    getcategoryOptions() {
        this.listOfCategories$.subscribe((result) => {
            if (result) {
                result.forEach((element) => {
                    this.categoryOptions.push({ label: element.Id, value: element.name });
                    this.categoriesForChoose.push({ label: element.Id, value: element.name });
                });
            }
        });
    }
    // ===========================================קטגוריות
    //filling out the list of the options for the dropdown in the profile component
    getSubCategoriesOptions1(id: number) {
        this.listOfCategories$.subscribe((result) => {
            if (result) {
                result.forEach((element) => {
                    if (id == element.Id)
                        this.subCategoryOptions1 = element.subCategoryList;
                });
            }
        });

    }
    // ~~~~~~2
    getSubCategoriesOptions2(id: number) {
        this.listOfCategories$.subscribe((result) => {
            if (result) {
                result.forEach((element) => {
                    if (id == element.Id)
                        this.subCategoryOptions2 = element.subCategoryList;
                });
            }
        });
    }
    // ~~~~~~3
    getSubCategoriesOptions3(id: number) {
        this.listOfCategories$.subscribe((result) => {
            if (result) {
                result.forEach((element) => {
                    if (id == element.Id)
                        this.subCategoryOptions3 = element.subCategoryList;
                });
            }
        });
    }
    // ~~~~~~4'
    getSubCategoriesOptions4(id: number) {
        this.listOfCategories$.subscribe((result) => {
            if (result) {
                result.forEach((element) => {
                    if (id == element.Id)
                        this.subCategoryOptions4 = element.subCategoryList;
                });
            }
        });
    }
    // ~~~~~~barter1
    getSubCategoriesOptionsForBarter1(id: number) {
        this.listOfCategories$.subscribe((result) => {
            if (result) {
                result.forEach((element) => {
                    if (id == element.Id)
                        this.subCategoryOptionForBarter1 = element.subCategoryList;
                });
            }
        });
    }
    // ~~~~~~barter2
    getSubCategoriesOptionsForBarter2(id: number) {
        this.listOfCategories$.subscribe((result) => {
            if (result) {
                result.forEach((element) => {
                    if (id == element.Id)
                        this.subCategoryOptionForBarter2 = element.subCategoryList;
                });
            }
        });
    }
    // ~~~~~~~~~~~~~~רשימת הרטגוריות לפי בחירה בדף החיפוש~~~~~~~~~~~~
    getSubCategoriesOptionsForSearchPage(id: number) {
        // var listWithOutDuplicates = this.ListOfSubCategoryToFillter;//אם לא רוצים לאפס את הרשימה לפני החיפוס לפי קטגוריה אלא רק להוסיף לרשימה את הקט
        var listWithOutDuplicates = [];//אם אנחנו רוצים לאפס את רשימת החיפוש לפני חיפוש לפי קטגוריה
        // this.subCategoryForSearchPageOptions = [];
        this.listOfCategories$.subscribe((result) => {
            if (result) {
                result.forEach((element) => {
                    if (id == element.Id) {
                        listWithOutDuplicates = element.subCategoryList;//הרשימה לחיפוש
                        this.subCategoryForSearchPageOptions = element.subCategoryList;//הרשימה של התתי קטגוריות
                    };
                });
            }
        });
        this.ListOfSubCategoryToFillter = [...new Set(listWithOutDuplicates)];
        //this.search();
    }
    getAreaOptions() {
        this.AreaOptionsProfile = [];
        this._buisnessService.getAreasList().subscribe(res => {
            if (res) {
                this.AreaOptions = res;
                res.forEach(area => {
                    //קוד 1 == כל הארץ
                    if (area.id != 1)
                        this.AreaOptionsProfile.push(area);
                });
            }
        })
    }
    search() {
        //clean the last search by initilize it in the global list of buisnesses
        this._afterFilterBuisnessListSubject.next(this.globalBuisnessList);

        var listAfterSearch: Array<BuisnessVm> = [];
        var GoodBusinnes;
        if (this.ListOfSubCategoryToFillter.length > 0) {
            //checking if there's more then 0 selected options
            //בחיפוש לפי תת קטגוריה נשלח לפונק' הזאת - שם נחפש לפי קומביניישן איידי
            //בחיפוש לפי קטגוריה נשלח לפונק' אחרת - שם נחפש לפי קטגוריה
            //בשתי החיפושים נכניס את התוצאות למערך:  this._afterFilterBuisnessListSubject
            this.afterFilterBuisnessList$.subscribe((res) => {
                res.forEach(b => {
                    this.ListOfSubCategoryToFillter.forEach(sb => {
                        // sb.includesToFillter = true;
                        GoodBusinnes = b.listOfAll4buisnessCategory.filter(sc => sc.combinationtId == sb.CategorySubCategoryId);
                        if (GoodBusinnes.length > 0
                            &&
                            (((this.ispayingBuisness && b.ispayingBuisness) || !this.ispayingBuisness)
                                || ((this.isburterBuisness && b.isburterBuisness) || !this.isburterBuisness)
                                || ((this.iscollaborationBuisness && b.iscollaborationBuisness) || !this.iscollaborationBuisness))) {
                            this.flagToAddBusinnes = true;
                        }

                    });
                    if (this.flagToAddBusinnes == true) {
                        listAfterSearch.push(b);
                        this.flagToAddBusinnes = false;
                    }
                })
                // })    
            });
            // res = res.filter((b) => this.listOfSelectedOptions.includes(b.name || b.name));
            //א"א להכניס הכל יחד
            var businnes = [...new Set(listAfterSearch)]
            this._afterFilterBuisnessListSubject.next(businnes);
            // this._buisnessListPerPageSubject.next(businnes);
            //מספר הכרטיסים הגלובלי
            this.totalnumberOfCards = businnes.length;
            // מספר העמודים = מספר העסקים הכללי חלקי מספר העסקים שבכל עמוד
            //to get the integer number and not the float
            let tempNumPages = Math.floor(businnes.length / this.numberOfCardsInOnePage);
            this.totalnumberOfPages =
                tempNumPages < businnes.length / this.numberOfCardsInOnePage ? tempNumPages + 1 : tempNumPages;

            //-מספר העמוד הנוכחי-באתחול הדף אנחנו נמצאים בעמוד הראשון
            this.numberOfCurrentPage = 1;

        }

    }
    searchByAllParameters(parametersObj: BusinessSearchParameters) {
        // console.log('ownerName',this.globalBuisnessList.filter(x=>x.ownerName));
        
        let listAfterSearch = [];
        this.globalBuisnessList.forEach(business => {
            if (!parametersObj.searchText || 
                (parametersObj.searchText && 
                (business.buisnessName.toLowerCase().includes(parametersObj.searchText.toLowerCase()))
                || (business.ownerName && business.ownerName.toLowerCase().includes(parametersObj.searchText.toLowerCase())))) {
                if ((parametersObj.ispayingBuisness && business.ispayingBuisness)
                    || (parametersObj.isburterBuisness && business.isburterBuisness)
                    || (parametersObj.iscollaborationBuisness && business.iscollaborationBuisness)) {
                    let flag = false;
                    if (business.buisnessAreaList1.find(x => x.areaId == 1)) {
                        flag = true;
                    }
                    else if (!parametersObj.isInAllCountry) {
                        flag = false;
                        if (parametersObj.areas.length) {
                            flag = true;
                            parametersObj.areas.forEach(area => {
                                if (!business.buisnessAreaList1.find(x => x.areaId == area.id)) {
                                    flag = false;
                                }
                            });
                        }
                        else {
                            flag = true
                        }
                    }

                    if (flag) {
                        //סינון לפי קטגוריות
                        flag = false;
                        if (parametersObj.categoryId) {

                            let cat = business.listOfAll4buisnessCategory.filter(c => c.categoryId == parametersObj.categoryId.label)
                            if (cat.length) {
                                parametersObj.subCategoryIds.forEach(elem => {
                                    if (cat.find(c => c.combinationtId == elem.CategorySubCategoryId)) {
                                        //listAfterSearch.push(business);
                                        flag = true;
                                    }
                                })
                            }
                        }
                        else {
                            if (!parametersObj.subCategoryIds.length)
                                flag = true;
                            else {
                                parametersObj.subCategoryIds.forEach(elem => {
                                    let catExist = business.listOfAll4buisnessCategory.filter(c => c.subCategoryId == elem.Id)
                                    if (catExist && catExist.length) {
                                        //listAfterSearch.push(business);
                                        flag = true;
                                    }
                                });
                            }
                        }
                    }
                    if (flag) {
                        listAfterSearch.push(business);
                    }
                }
            }
        });
        var businnes = [...new Set(listAfterSearch)]
        this._afterFilterBuisnessListSubject.next(businnes);
        // this._buisnessListPerPageSubject.next(businnes);
        //מספר הכרטיסים הגלובלי
        this.totalnumberOfCards = businnes.length;
        // מספר העמודים = מספר העסקים הכללי חלקי מספר העסקים שבכל עמוד
        //to get the integer number and not the float
        let tempNumPages = Math.floor(businnes.length / this.numberOfCardsInOnePage);
        this.totalnumberOfPages =
            tempNumPages < businnes.length / this.numberOfCardsInOnePage ? tempNumPages + 1 : tempNumPages;

        //-מספר העמוד הנוכחי-באתחול הדף אנחנו נמצאים בעמוד הראשון
        this.numberOfCurrentPage = 1;
    }

}
export class BusinessSearchParameters {
    searchText?: string | undefined;
    buisnessName?: string | undefined;
    businessEmailAddress?: string | undefined;
    ispayingBuisness?: boolean | undefined;
    isburterBuisness?: boolean | undefined;
    iscollaborationBuisness?: boolean | undefined;
    categoryId?: { label: number, value: string } | undefined;
    subCategoryIds?: SubCategoryVm[] | undefined;
    isInAllCountry?: boolean| undefined;
    areas?: AreaVm[] | undefined;

}