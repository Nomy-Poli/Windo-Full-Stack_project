import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { BuisnessService } from 'src/app/services/Buisness.service';
import { CaseStudyVM, CollaborationsService } from 'src/app/services/Collaboration.service';
import { WrapperCollaborationsService } from 'src/app/services/wrapper-collaborations.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';

@Component({
  selector: 'app-case-study-home',
  templateUrl: './case-study-home.component.html',
  styleUrls: ['./case-study-home.component.scss']
})
export class CaseStudyHomeComponent implements OnInit {
// public ListCs:CaseStudyVM[]=[];
// public ListFiveCs:CaseStudyVM[]=[];
  constructor(
    public _wrapperSearchService: WrapperSearchService,
    public _acct: AccountService,
    public _buisnessService: BuisnessService,
   public _wrapperCollaborationsService:WrapperCollaborationsService,
   public CollaborationsService:CollaborationsService,
    ) {}

  ngOnInit(): void {
     this.GetLastCS();
  }


GetLastCS(){
  this.CollaborationsService.getLastCS().subscribe(
    res=>{
      if(res!=undefined)
      {
        this._wrapperCollaborationsService.listLastPicCS=res
      }
    }
  )
}
}
