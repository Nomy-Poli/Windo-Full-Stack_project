import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { BreadcrumbService } from 'src/app/services/breadcrumb.service';
import { BuisnessService, PaidTransactionVM } from 'src/app/services/Buisness.service';
import { BarterDealVM, CaseStudyVM, CollaborationsService, ICollaborationsService, JointProjectVM } from 'src/app/services/Collaboration.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';

@Component({
  selector: 'app-collaboration-list',
  templateUrl: './collaboration-list.component.html',
  styleUrls: ['./collaboration-list.component.css']
})
export class CollaborationListComponent implements OnInit {

  public ListPaidTransactionVM: PaidTransactionVM[] = [];
  public ListBarterDealVM: BarterDealVM[] = [];
  public ListJointProjectVM: JointProjectVM[] = [];

  // public table1=false;
  // public table2=false;
  // public table3=false;
  public isActive: number
  constructor(public breadcrumbService: BreadcrumbService,
    public collaborationSrevic: CollaborationsService,
    public _buisnessService: BuisnessService,
    private _collaborationService: CollaborationsService,
    public _wrapperSearchService: WrapperSearchService,
    public acct: AccountService,
  ) {
    breadcrumbService.setItem([
      { label: 'דף הבית', routerLink: ['/'] },
      { label: 'אזור מנהל', routerLink: ['/manager'] },
      { label: 'ניהול cs', routerLink: ['.'] }

    ]);
  }

  ngOnInit(): void {
    this.acct.globalStateChanged.subscribe((state) => {
      this._wrapperSearchService.LoginStatus$.next(state.loggedInStatus);
    });
    this._wrapperSearchService.Username$ = this.acct.currentUserName;
    this._wrapperSearchService.HomePage$.next(false);
    this.GetAllPaidTransactions();
    this.GetAllJointProjects();
    this.GetAllBarterDeals();

  }
  GetAllPaidTransactions() {
    this.collaborationSrevic.getAllPaidTransactions().subscribe(res => {
      if (res != undefined) {
        this.ListPaidTransactionVM = res
        console.log(this.ListPaidTransactionVM)
      }
    })
  }

  GetAllBarterDeals() {
    this.collaborationSrevic.getAllBarterDeals().subscribe(res => {
      if (res != undefined) {
        this.ListBarterDealVM = res
        console.log(this.ListBarterDealVM)
      }
    })
  }

  GetAllJointProjects() {
    this.collaborationSrevic.getAllJointProjects().subscribe(res => {
      if (res != undefined) {
        this.ListJointProjectVM = res
        console.log(this.ListJointProjectVM)
      }
    })
  }

  deleteCollaboration(fromTable, id) {
    this._collaborationService.deleteCollaborationByIDAndTable(fromTable, id).subscribe(res => {
      switch (fromTable) {
        case 1:
          this.ListPaidTransactionVM = this.ListPaidTransactionVM.filter(x => x.Id != id);
          break;
        case 2:
          this.ListBarterDealVM = this.ListBarterDealVM.filter(x => x.Id != id);
          break;
        default:
        case 1:
          this.ListJointProjectVM = this.ListJointProjectVM.filter(x => x.Id != id);
          break;
      }
    })
  }

}
