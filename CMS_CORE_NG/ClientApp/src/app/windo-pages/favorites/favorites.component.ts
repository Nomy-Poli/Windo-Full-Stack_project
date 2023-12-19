import { Component, OnInit } from '@angular/core';
import { BreadcrumbService } from 'src/app/services/breadcrumb.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';

@Component({
  selector: 'app-favorites',
  templateUrl: './favorites.component.html',
  styleUrls: ['./favorites.component.scss']
})
export class FavoritesComponent implements OnInit {

  constructor(
    public breadcrumbService: BreadcrumbService,
    public _wrapperSearchService: WrapperSearchService,
  ) {
    breadcrumbService.setItem([
      { label: 'דף הבית', routerLink: ['/']/*, icon: 'pi pi-home'*/},
      { label: 'מועדפים', routerLink: ['/favorites'] }
    ]);
  }

  ngOnInit() {
    this._wrapperSearchService.HomePage$.next(false);

  }

}
