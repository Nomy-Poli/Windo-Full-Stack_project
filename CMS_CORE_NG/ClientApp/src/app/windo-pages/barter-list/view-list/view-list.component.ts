import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { BreadcrumbService } from 'src/app/services/breadcrumb.service';
import { BuisnessService } from 'src/app/services/Buisness.service';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';
import { BusinessForScoringVM, BusinessScoringsDetailVM, ScoringService } from 'src/app/services/Scoring.service';

@Component({
  selector: 'app-view-list',
  templateUrl: './view-list.component.html',
  styleUrls: ['./view-list.component.scss']
})

export class ViewListComponent implements OnInit {
  buisnessId=this._wrapperSearchService._afterFilterBuisnessListSubject;
  color: string;
  letters: string = '0123456789ABCDEF';
  constructor(public buisnessService: BuisnessService,
    public _wrapperSearchService: WrapperSearchService,
    public _wrapperFuncService: WrapperFuncService,
    public breadcrumbService: BreadcrumbService,
    private _scoringService: ScoringService,
    public _acct: AccountService) {
    breadcrumbService.setItem([
      { label: 'דף הבית', routerLink: ['/']/*, icon: 'pi pi-home'*/},
      // { label: 'ברטרים', routerLink: ['/barter-List'] },
      { label: 'רשימת עסקים', routerLink: ['/barter-List/view-list'] }
    ]);
    
  }
  listOfScoringDetails :BusinessScoringsDetailVM[] = [];
  ngOnInit() {
    // console.log("qqqqq",this._wrapperSearchService._afterFilterBuisnessListSubject._value);
    
    this.getRandomColor();
    this._wrapperSearchService.onListComponnent$.next(true);
    this.getListOfDetails(this.buisnessId);
    
  }
  getRandomColor() {
    this.color = '#'; // <-----------
    for (var i = 0; i < 6; i++) {
      this.color += this.letters[Math.floor(Math.random() * 16)];
    }
  }


  sendEmail(email){
    console.log("email",email);
    
    location.href = "mailto:"+email;
  }

  // openNewMessage(event,businessId){
  //   event.stopImmediatePropagation();
  //   event.preventDefault ? event.preventDefault() : (event.returnValue = false);
  //   event.stopPropagation ? event.stopPropagation() : (event.cancelBubble = true);  
  //   this._wrapperFuncService.openNewMassage(businessId);
  // }
  getListOfDetails(businessId){
    this._scoringService.getBusinessScoringsDetailById(businessId).subscribe((res =>{
        this.listOfScoringDetails =res;
        console.log("i am Detailbuisness today",this.listOfScoringDetails);
      }));
  }
}
