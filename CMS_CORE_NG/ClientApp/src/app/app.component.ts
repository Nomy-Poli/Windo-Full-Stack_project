import { Component, OnInit } from '@angular/core';
import { AccountService } from './services/account.service';
import { NetworkingService } from './services/Networking.service';
import { SharedDataService } from './services/shared-data.service';
import { WrapperFuncService } from './services/wrapper-func.service';
import { WrapperSearchService } from './services/wrapper-search.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'ClientApp';

  constructor(
    public _wrapperSearchService: WrapperSearchService, private _wrapperFuncService: WrapperFuncService, private _acct:AccountService,
    public _networkingService : NetworkingService,
    public _shaerdData : SharedDataService,
  ) { }
  ngOnInit() {
    this._wrapperSearchService.getCategoriesAndSubCategoryForAsideAndSuggestions();
    this._wrapperSearchService.getcategoryOptions();
    this._acct.Email.subscribe(email => {
      if (email) {
        this._wrapperSearchService.getCurrentBusiness().subscribe(res => {
          console.log('currentBusiness',res);
          if (!res) {
            this._wrapperFuncService.openRegitserNoBusinessPopup();
          }
          else{
            this.getGroupsForUSer(res.id);
          }
        })
      }
    })
     this.getAllGroups();
  }
  getAllGroups()
  {
    this._networkingService.getAllGroups().subscribe(res=>{
       this._shaerdData.groupList$.next(res);  
    })
  }
 
  getGroupsForUSer(buisnessId : number)
  {
    this._networkingService.getAllGroupsForUser(buisnessId).subscribe(res=>{
      console.log("groupFor user", res);
       this._shaerdData.groupListForUser.next(res);  
    })
  }
  //TODO:
  // bring all groups for user 
}
