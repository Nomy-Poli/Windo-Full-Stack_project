import { Component, OnInit } from '@angular/core';
import { BreadcrumbService } from 'src/app/services/breadcrumb.service';

@Component({
  selector: 'app-manager-area',
  templateUrl: './manager-area.component.html',
  styleUrls: ['./manager-area.component.scss']
})
export class ManagerAreaComponent implements OnInit {

  constructor(public breadcrumbService: BreadcrumbService,) {
    breadcrumbService.setItem([
      { label: 'דף הבית', routerLink: ['/'] },
      { label: 'אזור מנהל', routerLink: ['/manager'] }
    ]);
   }

  ngOnInit(): void {
  }

}
