import { Component, OnInit, ViewChild } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { AccountService } from 'src/app/services/account.service';
import { AdvertismentService } from 'src/app/services/advertisment.service';
import { BreadcrumbService } from 'src/app/services/breadcrumb.service';
import { NetworkingGroupVM, NetworkingService } from 'src/app/services/Networking.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';
import { NetworkingGroupBuisnessListComponent } from '../networking-group-buisness-list/networking-group-buisness-list.component';

@Component({
  selector: 'app-networking-managment-area',
  templateUrl: './networking-managment-area.component.html',
  styleUrls: ['./networking-managment-area.component.css']
})
export class NetworkingManagmentAreaComponent implements OnInit {
  constructor(
    public _wrapperSearchService: WrapperSearchService,
    private _acct: AccountService,
    public breadcrumbService: BreadcrumbService,
    private _advertismentService: AdvertismentService,
    public _networkingService : NetworkingService,

  ) { 
    breadcrumbService.setItem([
      { label: 'דף הבית', routerLink: ['/']/*, icon: 'pi pi-home'*/ },
      { label: 'אזור מנהל', routerLink: ['/manager'] },
      { label: 'ניהול קבוצות', routerLink: ['.'] }
    ]);

  }
  @ViewChild('buisnessComp') buisnessComp: NetworkingGroupBuisnessListComponent;
  
  tabIndex =0;
  IndexTabChange = 0;
  groupBuisnessActive = false;
  ngOnInit(): void {
    console.log("tabindex",this.IndexTabChange);
    
    
  }

  buisnessByGroups(groupId : number){
    this.tabIndex = 1;
    this.buisnessComp.getGroupWithAllBuisness(groupId)

  }
}
