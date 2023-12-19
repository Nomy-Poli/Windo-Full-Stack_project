import { Component, OnInit } from '@angular/core';
import { BreadcrumbService } from 'src/app/services/breadcrumb.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';

@Component({
  selector: 'app-search-domain',
  templateUrl: './search-domain.component.html',
  styleUrls: ['./search-domain.component.scss']
})
export class SearchDomainComponent implements OnInit {

  constructor(public breadcrumbService: BreadcrumbService,
    public _wrapperSearchService: WrapperSearchService,
  ) {
    breadcrumbService.setItem([
      { label: 'דף הבית', routerLink: ['/']/*, icon: 'pi pi-home'*/},
      { label: ' חיפוש תחום ', routerLink: ['/search-domain'] }
    ]);
  }

  ngOnInit() {
    this._wrapperSearchService.HomePage$.next(false);

  }

}
