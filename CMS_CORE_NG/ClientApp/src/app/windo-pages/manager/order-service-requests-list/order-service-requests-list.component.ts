import { listLazyRoutes } from '@angular/compiler/src/aot/lazy_routes';
import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { AdvertismentService, RequestOrderServiceVM } from 'src/app/services/advertisment.service';
import { BreadcrumbService } from 'src/app/services/breadcrumb.service';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';

@Component({
    selector: 'app-order-service-requests-list',
    templateUrl: './order-service-requests-list.component.html',
    styleUrls: ['./order-service-requests-list.component.scss']
})
export class OrderServiceRequestsListComponent implements OnInit {
    requestsList: RequestOrderServiceVM[] = [];
    loading: boolean = true;
    statuses = { 1: 'בהמתנה', 2: 'בטיפול', 3: 'נדחתה' };
    get statusesValueLabel(): Array<{ value: any; label: any }> {
        let list = [];
        for (const key in this.statuses) {
            list.push({ value: key, label: this.statuses[key] });
        }
        return list;
    }

    constructor(
        public _wrapperSearchService: WrapperSearchService,
        private _acct: AccountService,
        private _wrapperFuncService: WrapperFuncService,
        public breadcrumbService: BreadcrumbService,
        private _advertismentService: AdvertismentService
    ) {
        // breadcrumbService.setItem([
        //   { label: 'דף הבית', routerLink: ['/']/*, icon: 'pi pi-home'*/ },
        //   { label: 'אזור מנהל', routerLink: ['/manager'] },
        //   { label: 'רשימת בקשות ליצירת קשר', routerLink: ['.'] }
        // ]);

        this._acct.globalStateChanged.subscribe((state) => {
            this._wrapperSearchService.LoginStatus$.next(state.loggedInStatus);
        });
        this._wrapperSearchService.Username$ = this._acct.currentUserName;
    }

    ngOnInit(): void {
        this.getRequsetOrderService();
        this.loading = false;
    }

    getRequsetOrderService() {
        this._advertismentService.getRequsetOrderService().subscribe((res) => {
            this.requestsList = res;
        });
    }
    changeStatus(request: RequestOrderServiceVM, status) {
        if (request.RequestStatus != status.value) {
            this._advertismentService.updateRequsetOrderServiceStatus(request.RequestStatus, status.value).subscribe((res) => {
                request.RequestStatus = status.value;
                request['showStatuses'] = null;
            });
        } else {
            request['showStatuses'] = null;
        }
    }
    openNewOrderForm(request: RequestOrderServiceVM) {
        if (request.RequestStatus == 1) {
            console.log('id', request);
            this._wrapperFuncService.openOrderForm(null, null, request.Id).subscribe((res) => {
                if (request.RequestStatus != 2)
                    this._advertismentService.updateRequsetOrderServiceStatus(request.Id, 2).subscribe((res) => {
                        request.RequestStatus = 2;
                    });
            });
        }
    }

    openNewClientForm(request) {
      if (request.RequestStatus == 1) {
        this._wrapperFuncService.openClientForm(null, request.Id);
      }
    }
}
