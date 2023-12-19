import { AfterContentChecked, ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AccountService } from 'src/app/services/account.service';
import { BreadcrumbService } from 'src/app/services/breadcrumb.service';
import { BuisnessService, BuisnessVm } from 'src/app/services/Buisness.service';
import { CaseStudyForCardsVM, CaseStudyVM, CollaborationsService } from 'src/app/services/Collaboration.service';
import { SearchCategoryService, SubCategoryVm } from 'src/app/services/SearchCategory.service';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';
import { BusinessSearchParameters, WrapperSearchService } from 'src/app/services/wrapper-search.service';

@Component({
    selector: 'app-barter-list',
    templateUrl: './barter-list.component.html',
    styleUrls: ['./barter-list.component.scss']
})
export class BarterListComponent implements OnInit, AfterContentChecked, OnDestroy {
    public list: Array<BuisnessVm>;
    businnesAfterFillter: BuisnessVm;
    prevIndex: number = null;
    count: number = 1;
    ListAfterSearch: Array<BuisnessVm> = [];

    CategoryOptions: { label: number; value: string }[] = [];
    //רשימת הקטגוריות שנבחרו
    isCollapsedList: boolean = true;
    parameterForm: FormGroup
    isCountryCollapsedOpen;
    selectedCategory;
    isDisplayForm = false;
    constructor(
        public _buisnessService: BuisnessService,
        public _wrapperSearchService: WrapperSearchService,
        private acct: AccountService,
        public breadcrumbService: BreadcrumbService,
        private cdref: ChangeDetectorRef,
        private _funcService: WrapperFuncService,
        private _activatedRoute: ActivatedRoute
        // public CollaborationsSer:CollaborationsService
    ) {
        breadcrumbService.setItem([
            { label: 'דף הבית', routerLink: ['/']/*, icon: 'pi pi-home'*/ },
            { label: 'ברטרים', routerLink: ['/barter-list'] }
        ]);
    }
    ngOnDestroy(): void {
        this._wrapperSearchService.numberOfCurrentPage = 1;
        this._wrapperSearchService._afterFilterBuisnessListSubject.unsubscribe();
        this._funcService.closeDialog();
    }
    ngAfterContentChecked() {
        this.cdref.detectChanges();
    }
    ngOnInit() {
        this._wrapperSearchService.numberOfCurrentPage = 1;
        window.scrollTo(0, 0);//on reload - go to the top of the page
        this.acct.globalStateChanged.subscribe((state) => {
            this._wrapperSearchService.LoginStatus$.next(state.loggedInStatus);
        });
        this._wrapperSearchService.Username$ = this.acct.currentUserName;
        this._wrapperSearchService.subscribeBusinesList();
        this.search();
        //fill out the aside menu
        this._wrapperSearchService.isCollapsed = new Array<false>();
        this._wrapperSearchService.HomePage$.next(false);

    }
    toggleSearch(event) {
        this.isDisplayForm = !this.isDisplayForm;
        event.stopPropagation();
    }
    closeSearch(){
        this.isDisplayForm = false;
    }
    preventToggle(event){
        event.stopPropagation();
    }
    search() {
        this._wrapperSearchService.numberOfCurrentPage = 1;
        this._buisnessService.getListOfBuisnesses().subscribe((result) => {
            //add all the list to one that includs the 4 lists
            this._wrapperSearchService._afterFilterBuisnessListSubject.next(result);
            result.forEach((element) => {
                element.listOfAll4buisnessCategory = [...element.buisnessCategory1, ...element.buisnessCategory2, ...element.buisnessCategory3, ...element.buisnessCategory4,]
                element.listOfAll4buisnessCategory = [...new Set(element.listOfAll4buisnessCategory)]
            });
            //הרשימה הגלובלית של העסקים שלא משתנה אף פעם
            this._wrapperSearchService.globalBuisnessList = result;
            this._wrapperSearchService._afterFilterBuisnessListSubject.next(result);
            if (this._wrapperSearchService.parameterSearch) {
                let params = this._wrapperSearchService.parameterSearch;
                if (params['searchText']) {
                    this.parameterForm.get('searchText').setValue(params['searchText']);
                }
                if (params['categoryId']) {
                    this.parameterForm.get('categoryId').setValue(params['categoryId']);
                }
                if (params['subCategoryIds']) {
                    this.parameterForm.get('subCategoryIds').setValue(params['subCategoryIds']);
                }
                if (params['areas']) {
                    this.parameterForm.get('areas').setValue(params['areas']);
                }
                this.searchByAll();
                this._wrapperSearchService.parameterSearch = null;
            }
        });
        this._wrapperSearchService.getAreaOptions();
        //-מספר העמוד הנוכחי-באתחול הדף אנחנו נמצאים בעמוד הראשון
        this._wrapperSearchService.numberOfCurrentPage = 1;
        this.parameterForm = new FormGroup({
            searchText: new FormControl(''),
            buisnessName: new FormControl(''),
            businessEmailAddress: new FormControl(''),
            ispayingBuisness: new FormControl(true),
            isburterBuisness: new FormControl(true),
            iscollaborationBuisness: new FormControl(true),
            categoryId: new FormControl(),
            subCategoryIds: new FormControl(),
            isInAllCountry: new FormControl(),
            areas: new FormControl([])
        });
    }
    //איזה תת קטגוריה לפתוח
    // openColaps(index: number, category, e) {
    //     if ((e.target == e.currentTarget && e != null) || e == true) {
    //         this._wrapperSearchService.isCollapsed[index] = this._wrapperSearchService.isCollapsed[index] == true ? false : true;
    //         //דואג לססגירת הקולאפס הפתוח = ז"א שיהיה רק קלאפס אחד פתוח
    //         // if (this.prevIndex != null) if (this.count == 1) this._wrapperSearchService.isCollapsed[this.prevIndex] = false;
    //         if (this.prevIndex == index) this.count++;
    //         else this.count = 1;
    //         //הישן יהיה לחדש הנוכחי
    //         this.prevIndex = index;
    //         //שליחה לפונק החיפוש לפי קטגוריה
    //         //ז"א בחירת כל התתי קטגוריות לחיפוש בעת לחיצה על קטגוריה - בצד
    //         // if (this._wrapperSearchService.isCollapsed[index] == true) {
    //         //     this._wrapperSearchService.getSubCategoriesOptionsForSearchPage(category.Id);
    //         // }
    //     }
    // }
    prevPage() {
        //checking if we have back pages left
        if (this._wrapperSearchService.numberOfCurrentPage > 1) {
            this._wrapperSearchService.afterFilterBuisnessList$.subscribe((res) => {
                //where from to slice
                let tempStart;
                tempStart =
                    (this._wrapperSearchService.numberOfCurrentPage - 1 == 1 ? 0 : this._wrapperSearchService.numberOfCurrentPage - 1) *
                    this._wrapperSearchService.numberOfCardsInOnePage -
                    this._wrapperSearchService.numberOfCardsInOnePage;
                tempStart = tempStart < 0 ? 0 : tempStart;
                //Up to where to cut
                let tempEnd;
                tempEnd =
                    this._wrapperSearchService.numberOfCurrentPage * this._wrapperSearchService.numberOfCardsInOnePage -
                    this._wrapperSearchService.numberOfCardsInOnePage;
                // this._wrapperSearchService._buisnessListPerPageSubject.next(res.slice(tempStart, tempEnd));
                this._wrapperSearchService.numberOfCurrentPage--;
            });
        }
        //if we arrived to the last page we dont do anything
        else {
        }
    }
    nextPage() {
        //checking if we have more pages left
        if (this._wrapperSearchService.numberOfCurrentPage < this._wrapperSearchService.totalnumberOfPages) {
            this._wrapperSearchService.afterFilterBuisnessList$.subscribe((res) => {
                //where from to slice
                let tempStart;
                tempStart = this._wrapperSearchService.numberOfCurrentPage * this._wrapperSearchService.numberOfCardsInOnePage;
                //Up to where to cut
                let tempEnd;
                tempEnd =
                    this._wrapperSearchService.numberOfCurrentPage * this._wrapperSearchService.numberOfCardsInOnePage +
                    this._wrapperSearchService.numberOfCardsInOnePage;
                // this._wrapperSearchService._buisnessListPerPageSubject.next(res.slice(tempStart, tempEnd));
                this._wrapperSearchService.numberOfCurrentPage++;
            });
        }
        //if we arrived to the last page we dont do anything
        else {
        }
    }
    onPageChange(event: any) {
        if (this._wrapperSearchService.numberOfCurrentPage <= this._wrapperSearchService.totalnumberOfPages) {
            // this._wrapperSearchService.afterFilterBuisnessList$.subscribe((res) => {

            let tempStart;
            tempStart = event.page * this._wrapperSearchService.numberOfCardsInOnePage;

            let tempEnd;
            tempEnd = tempStart + this._wrapperSearchService.numberOfCardsInOnePage;

            // this._wrapperSearchService._buisnessListPerPageSubject.next(res.slice(tempStart, tempEnd));

            this._wrapperSearchService.numberOfCurrentPage = event.page + 1;
            window.scrollTo({ top: 0, behavior: 'smooth' });
            // });
        }
    }
    // viewLlist() {
    //     this._wrapperSearchService.onListComponnent$.next(true);
    // }
    // ViewCard() {
    //     this._wrapperSearchService.onListComponnent$.next(false);
    // }
    onSelectCategory(label) {
        this._wrapperSearchService.getSubCategoriesOptionsForSearchPage(label);
    }
    // חיפוש לפי תת קטגוריות
    OnChooseSubCategory(subCategory) {
        if (this._wrapperSearchService.ListOfSubCategoryToFillter.includes(subCategory))//כבר קיים ברשימה
        {
            var a = this._wrapperSearchService.ListOfSubCategoryToFillter.findIndex(sc => subCategory.CategorySubCategoryId == sc.CategorySubCategoryId);
            var b = this._wrapperSearchService.ListOfSubCategoryToFillter.splice(a, 1);
            // console.log(this._wrapperSearchService.listOfCategories$);
            // console.log(b)
        }
        else {
            this._wrapperSearchService.ListOfSubCategoryToFillter.push(subCategory);
        }
        this._wrapperSearchService.search();
    }
    OnChooseSubCategoryFromDD() {
        this._wrapperSearchService.search();
        // console.log(this._wrapperSearchService.ListOfSubCategoryToFillter);
        // $('#subCategory').load(document.URL + ' #subCategory');
        // parent.LIsubCategory.location.reload();
        // window.location.reload();
    }
    ElementIcluded(subCategory) {
        var res = this._wrapperSearchService.ListOfSubCategoryToFillter.find(sc => sc.CategorySubCategoryId == subCategory.CategorySubCategoryId)
        if (res != null)
            return true
        else
            return false

    }

    searchByAll() {
        let formValue = this.parameterForm.value;
        this._wrapperSearchService.searchByAllParameters(formValue)
    }
    resetFillterList() {

        this.parameterForm.patchValue({
            searchText: '',
            buisnessName: '',
            businessEmailAddress: '',
            ispayingBuisness: true,
            isburterBuisness: true,
            iscollaborationBuisness: true,
            categoryId: null,
            subCategoryIds: [],
            isInAllCountry: false,
            areas: []
        })
        this._wrapperSearchService.numberOfCurrentPage = 1;
        this._wrapperSearchService._afterFilterBuisnessListSubject.next(this._wrapperSearchService.globalBuisnessList);
        // this._wrapperSearchService._buisnessListPerPageSubject.next(this._wrapperSearchService.globalBuisnessList);
        this._wrapperSearchService.ListOfSubCategoryToFillter = [];
        // this._wrapperSearchService.totalnumberOfCards = this._wrapperSearchService.globalBuisnessList.length;
        this.selectedCategory = null;
        this._wrapperSearchService.subCategoryForSearchPageOptions = this._wrapperSearchService._listOfSubCategoriesSubject.value;

        // this._wrapperSearchService.totalnumberOfPages=this._wrapperSearchService.totalnumberOfCards/this._wrapperSearchService.numberOfCardsInOnePage;
    }
}
