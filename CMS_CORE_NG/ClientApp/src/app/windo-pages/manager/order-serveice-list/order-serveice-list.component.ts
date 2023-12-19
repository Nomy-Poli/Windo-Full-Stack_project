import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AccountService } from 'src/app/services/account.service';
import { AdvertismentService, OrderServiceVM, OrderServiceWithAdDetailsVM } from 'src/app/services/advertisment.service';
import { BreadcrumbService } from 'src/app/services/breadcrumb.service';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-order-serveice-list',
  templateUrl: './order-serveice-list.component.html',
  styleUrls: ['./order-serveice-list.component.scss']
})
export class OrderServeiceListComponent implements OnInit {

  constructor(public _wrapperSearchService: WrapperSearchService,
    private _acct: AccountService,
    public breadcrumbService: BreadcrumbService,
    private _advertismentService: AdvertismentService,
    private _funcService:WrapperFuncService,
    private _activatedRoute: ActivatedRoute) {
    // breadcrumbService.setItem([
    //   { label: 'דף הבית', routerLink: ['/']/*, icon: 'pi pi-home'*/ },
    //   { label: 'אזור מנהל', routerLink: ['/manager'] },
    //   { label: 'רשימת הזמנות', routerLink: ['.'] }
    // ]);
    this._acct.globalStateChanged.subscribe((state) => {
      this._wrapperSearchService.LoginStatus$.next(state.loggedInStatus);
    });
    this._wrapperSearchService.Username$ = this._acct.currentUserName;
    this.clientId = _activatedRoute.snapshot.paramMap.get('clientId');

   }

   ordersList:OrderServiceVM[] = [];
   ordersListAfterFilter:OrderServiceVM[] = [];
   clientId;

  ngOnInit(): void {
    this.getOrders();
  }



  getOrders() {
    this._advertismentService.getOrders(null,this.clientId).subscribe(res=>{
      this.ordersList = res;
      this.ordersListAfterFilter = res;
    });
  }

  filterOrdersByClient(clientId){
    this.ordersListAfterFilter = this.ordersList.filter(o=>o.ClientId == clientId);
  }
  editOrder(order){
    this._funcService.openOrderForm(null,order.Id,null).subscribe((res: OrderServiceWithAdDetailsVM)=>{
      order = res;
    });
  }
  deleteOrder(order){
    Swal.fire({
      title: 'האם את בטוחה שאת רוצה למחוק את ההזמנה??',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'כן, למחוק',
      cancelButtonText: 'לא, סליחה טעות'
    }).then((result) => {
      if (result.value) {
        this.ordersList = this.ordersList.filter(x=>x.Id!= order.Id);
        this.ordersListAfterFilter = this.ordersListAfterFilter.filter(x=>x.Id!= order.Id);
       this._advertismentService.deleteOrder(order.Id).subscribe(res=>{
         console.log(res);
         this.ordersList = this.ordersList.filter(c=>c.Id!=order.Id);
       });
      } 
    });
  }
}

