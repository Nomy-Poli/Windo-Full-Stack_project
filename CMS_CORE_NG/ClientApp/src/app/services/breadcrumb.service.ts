import { Injectable } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BreadcrumbService {
  public _breadcrumbsSubject: BehaviorSubject<MenuItem[]> = new BehaviorSubject([
    // { label: 'דף הבית', routerLink: ['/'], icon: 'pi pi-home' }
  ]);
  breadcrumbs$ = this._breadcrumbsSubject.asObservable();

  constructor() { }

  setItem(item: MenuItem[]) {
    this._breadcrumbsSubject.next(item);
  }
}
