import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AccountService } from 'src/app/services/account.service';
import { BreadcrumbService } from 'src/app/services/breadcrumb.service';
import { BuisnessService } from 'src/app/services/Buisness.service';
import { CaseStudyForCardsVM, CollaborationsService } from 'src/app/services/Collaboration.service';
import { WrapperCollaborationsService } from 'src/app/services/wrapper-collaborations.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';
// import { BuisnessService } from 'src/app/services/';

@Component({
  selector: 'app-case-study-list',
  templateUrl: './case-study-list.component.html',
  styleUrls: ['./case-study-list.component.scss']
})
export class CaseStudyListComponent implements OnInit {
  BuisnessId:any

  @Input() listCSToDisplayToEmit:number;

  constructor(
    public _buisnessService: BuisnessService,
    public _wrapperSearchService: WrapperSearchService,
    // public _wrapperFuncService: WrapperFuncService,
    public WrapperCollaborationsSer:WrapperCollaborationsService,
    public _acct: AccountService,
    public _wrapperCollaborationsService: WrapperCollaborationsService,
    public breadcrumbService: BreadcrumbService, 
    ) {
        breadcrumbService.setItem([
            { label: 'דף הבית', routerLink: ['/']/*, icon: 'pi pi-home'*/},
            { label: 'רשימת הקייס סטאדי', routerLink: ['/case-study-list'] }
        ]);
    }

  ngOnInit(): void {
    // console.log(this.listCSToDisplayToEmit)
    
    window.scroll(0,0);
    this._acct.globalStateChanged.subscribe((state) => {
      this._wrapperSearchService.LoginStatus$.next(state.loggedInStatus);
    });
    this._wrapperSearchService.Username$ = this._acct.currentUserName;
    this._wrapperSearchService.HomePage$.next(false);
  }
  // GetCSByEmail(idBuissness:number)
  // {
  //     this.CollaborationsSer.getCSByBuissinesID(idBuissness).subscribe(res=>{
  //         if(res!=undefined)
  //         {
  //             this.ListCSOfBuis=res
  //             console.log(this.ListCSOfBuis)
  //         }  
  //     })
  // }
}
