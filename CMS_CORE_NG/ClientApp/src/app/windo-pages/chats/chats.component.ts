import { Component, OnInit } from '@angular/core';
import { BreadcrumbService } from 'src/app/services/breadcrumb.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';

@Component({
  selector: 'app-chats',
  templateUrl: './chats.component.html',
  styleUrls: ['./chats.component.scss']
})
export class ChatsComponent implements OnInit {

  constructor(
    public breadcrumbService: BreadcrumbService,
    public _wrapperSearchService: WrapperSearchService,
  ) {
    breadcrumbService.setItem([
      { label: 'דף הבית', routerLink: ['/']/*, icon: 'pi pi-home'*/},
      { label: 'צטים', routerLink: ['/chats'] }
    ]);
  }

  ngOnInit() {
    this._wrapperSearchService.HomePage$.next(false);

  }

}
