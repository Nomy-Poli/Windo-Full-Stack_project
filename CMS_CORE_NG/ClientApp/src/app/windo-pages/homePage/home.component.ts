import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { fromEvent, Observable, Subscription } from 'rxjs';
import { AccountService } from 'src/app/services/account.service';
import { BuisnessService } from 'src/app/services/Buisness.service';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  slides: any = [[]];
  flagPrev: boolean = true;
  flagNext: boolean = true;
  userName: Observable<any>;
  pos: any;
  parameterForm:FormGroup;
  isDisplayForm =false;
  constructor(public _wrapperSearchService: WrapperSearchService
    , private acct: AccountService
    , private router: Router
    , private modalService: NgbModal
    , public _wrapperFuncService: WrapperFuncService
    , public _buisnessService: BuisnessService) {
  }
  ngOnInit() {
    window.scroll(0,0);
    //this._wrapperFuncService.closeDialog();
    this._wrapperSearchService.Username$ = this.acct.currentUserName;
    if (this.acct.FirstName.value != null) {
      this.userName = this.acct.FirstName;
    }
    else {
      let name;
      this._wrapperSearchService.Username$.subscribe(n => name = n);
      this.userName = name.split('@')[0].split('[name.length]');
      
    }
    this.acct.globalStateChanged.subscribe((state) => {
      this._wrapperSearchService.LoginStatus$.next(state.loggedInStatus);
    });
    this._wrapperSearchService.HomePage$.next(true);
    this._wrapperSearchService.getLatestBuisnesses();
    this.next();
    this.prev();
    this.parameterForm = new FormGroup({
      searchText: new FormControl(''),
      categoryId: new FormControl(),
      subCategoryIds: new FormControl(),
      areas: new FormControl([])
  });
  this._wrapperSearchService.getAreaOptions();
  }


  @ViewChild('dragScrollItems') ele: ElementRef;
  mousemoveElement: Subscription = new Subscription();
  mouseupElement: Subscription = new Subscription();

  ngAfterViewInit() {
    if(this.ele)
      this.ele.nativeElement.scrollLeft = 0
  }

 

  next() {
    let temp;//temp array 
    this._wrapperSearchService.latestBuisnessList$.subscribe(res => {
      //i get the index of the first slide that now shown on the screen
      if (res != null) {
        let i = res[0].index;
        //the length of the source array
        let end = this._wrapperSearchService.lengthLatestBuisnessList;
        //slice the array only if there are more the 5 left
        //its always possible to 
        if (end - 4 >= i) {
          temp = this._wrapperSearchService.globalLatestBuisnessListForHomePage.slice(i, i + 4)
          //כל עוד עדין לא הגעת לסוף, ויש לך עוד מה להציג - תאפשר ללחוץ על החץ
          this.flagNext = true;
        }
        else {
          //כי אין לו אפשרות יותר ללחוץ על 'הבא' כי אין לו עוד false נהפך ל
          this.flagNext = false;

        }
        //נבדוק אם אפשר לאפשר את הלחצן של הקודם, - אם יש לאן ללכת אחרוה נשחרר את החץ אחורה
        if (i > 1) {
          //אם האינדקס יותר גדול מ1 שזה אומר שיש עוד אז תאפשר את הלחצן
          //todo: לבדוק האם הבדיקה צריכה להיות על 5 - ז"א אם יש רק עסק אחד מה יקרה?
          this.flagPrev = true;
        }
        else {
          //אם אין לך - אל תאפשר ללחוץ
          this.flagPrev = false;
        }
      }
    })//push it into the subject that listening in the html
    if (temp != null) {
      this._wrapperSearchService._latestBuisnessListSubject.next(temp);
    }
    else
      this.flagNext = false;
  }

  prev() {
    let temp;
    this._wrapperSearchService.latestBuisnessList$.subscribe(res => {
      if (res != null) {
        let i = res[0].index;
        let end = this._wrapperSearchService.lengthLatestBuisnessList;
        if (i > 1) {
          temp = this._wrapperSearchService.globalLatestBuisnessListForHomePage.slice(i - 2, i + 2);
          this.flagPrev = true;
        }
        else {
          this.flagPrev = false;
        }
        if (end - 4 >= i) {
          //כל עוד עדין לא הגעת לסוף, ויש לך עוד מה להציג - תאפשר ללחוץ על החץ
          this.flagNext = true;
        }
        else {
          //כי אין לו אפשרות יותר ללחוץ על 'הבא' כי אין לו עוד false נהפך ל
          this.flagNext = false;

        }
      }
    })
    if (temp != null)
      this._wrapperSearchService._latestBuisnessListSubject.next(temp);
    else
      this.flagPrev = false;
  }

  OpenBusiness() {
    if ((this._wrapperSearchService.LoginStatus$.value == false)) {
      this._wrapperFuncService.openLoginDialog();
    }
    else
      this.router.navigate(['/profile']);
  }
  routeToBusiness(event:PointerEvent,business){
    if(event.target['id'] !="heart-icon" ){
      this.router.navigate(['/business-view/',business.userId]);
    }
    
  }
 openVedio(video) {
    // this.embedLink = '../../../../../assets/BusinessImages/video.mp4';
    this.modalService.open(video, { centered: true, size: 'md' });
    //    $('#video')
    //   .removeClass('d-none');
    // className.style.display = "block";
    // jQuery('#video').modal('show');
  }

  //#region search business
  toggleSearch(event) {
    this.isDisplayForm = !this.isDisplayForm;
    event.stopPropagation();
  }
  closeSearch(event) {
    // if(event.target.id != "input-search")
      this.isDisplayForm = false;
  }
  onSelectCategory(label){
    this._wrapperSearchService.getSubCategoriesOptionsForSearchPage(label);
  }
  passParametersToSearch(){
    let formValue = this.parameterForm.value;
    this._wrapperSearchService.parameterSearch = formValue;
    this.router.navigate(['/barter-List']);
}
resetFillterList() {
    this.parameterForm.patchValue({
        searchText: '',
        buisnessName: '',
        businessEmailAddress: '',
        ispayingBuisness:  true,
        isburterBuisness:  true,
        iscollaborationBuisness:  true,
        categoryId: null,
        subCategoryIds: [],
        isInAllCountry: false,
        areas:[]
    })
    
    // this._wrapperSearchService.totalnumberOfPages=this._wrapperSearchService.totalnumberOfCards/this._wrapperSearchService.numberOfCardsInOnePage;
}


  //#region Lateral scroll
  mouseDownHandler(e) {
    this.pos = {
      // The current scroll
      left: this.ele.nativeElement.scrollLeft,
      x: e.clientX,
    };
    console.log(this.pos, "pos")
    this.ele.nativeElement.style.cursor = 'grabbing';
    this.ele.nativeElement.style.userSelect = 'none';
    // this.ele.nativeElement.addEventListener('mousemove', this.mouseMoveHandler.bind(this));
    // this.ele.nativeElement.addEventListener('mouseup', this.mouseUpHandler.bind(this));
    this.mousemoveElement = fromEvent(this.ele.nativeElement, 'mousemove').subscribe((e) => {
      this.mouseMoveHandler(e);
    })
    this.mouseupElement = fromEvent(this.ele.nativeElement, 'mouseup').subscribe(() => {
      this.mouseUpHandler();
    })
  };

  mouseMoveHandler(e) {
    // How far the mouse has been moved
    const dx = e.clientX - this.pos.x;
    console.log(e.clientX, dx, this.ele.nativeElement.width);
    this.ele.nativeElement.scrollLeft = this.pos.left - dx;
  };


  mouseUpHandler() {
    this.mousemoveElement.unsubscribe();
    this.mouseupElement.unsubscribe();
    this.ele.nativeElement.removeEventListener('mousemove', this.mouseMoveHandler.bind(this));
    this.ele.nativeElement.removeEventListener('mouseup', this.mouseUpHandler.bind(this));
    this.ele.nativeElement.style.cursor = 'grab';
    this.ele.nativeElement.style.removeProperty('user-select');
  };

//#endregion

scrolldown() {
  var scrollDiv = document.getElementById("section1").offsetTop;
  window.scrollTo({ top: scrollDiv, behavior: 'smooth' });
}

}
