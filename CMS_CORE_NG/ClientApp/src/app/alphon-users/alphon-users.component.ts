import { Component, OnInit } from '@angular/core';
import { ApplicationUser, MistamshimService } from '../services/mistamshim';
import { Router } from '@angular/router';
import { BreadcrumbService } from '../services/breadcrumb.service';
import { WrapperSearchService } from '../services/wrapper-search.service';
@Component({
  selector: 'app-alphon-users',
  templateUrl: './alphon-users.component.html',
  styleUrls: ['./alphon-users.component.scss']
})
export class AlphonUsersComponent implements OnInit {

  constructor(
    public _mistamshim: MistamshimService
    ,private _router: Router
    , public breadcrumbService: BreadcrumbService
    , public _wrapperSearchService: WrapperSearchService
  ) {
      breadcrumbService.setItem([
        { label: 'דף הבית', routerLink: ['/']/*, icon: 'pi pi-home'*/ },
        { label: 'אלפון', routerLink: ['/alphon-users'] }
      ]);
   }
   numberOfCurrentPage = 1;
   numberOfCardsInOnePage = 8;
   totalnumberOfPages;
  listOfUsers: Array<ApplicationUser> = [];
  ngOnInit(): void {
    this._wrapperSearchService.HomePage$.next(false);
    this._mistamshim.getUsers().subscribe((res) => {
      this.listOfUsers = res;
      if(res)
      this.setPagingNumber(res);
      console.log("sucsees getUser",res); 
    })
  }
  setPagingNumber(list) {
    this.numberOfCurrentPage = 1;
    let tempNumPages = Math.floor(list.length / this.numberOfCardsInOnePage);
    this.totalnumberOfPages = tempNumPages + 1;
    console.log("cdcd", this.totalnumberOfPages);
  }
  onPageChange(event) {
    if (this.numberOfCurrentPage <= this.totalnumberOfPages) {
      let tempStart = event.page * this.numberOfCardsInOnePage;

      let tempEnd;
      tempEnd = tempStart + this.numberOfCardsInOnePage;
      this.numberOfCurrentPage = event.page + 1;
    }
    else {

    }
  }
  sendEmail(email){
    location.href = "mailto:"+ email;
  }
}

