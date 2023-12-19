import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { AdvertismentService, ClientVM } from 'src/app/services/advertisment.service';
import { BreadcrumbService } from 'src/app/services/breadcrumb.service';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-clients-list',
  templateUrl: './clients-list.component.html',
  styleUrls: ['./clients-list.component.scss']
})
export class ClientsListComponent implements OnInit {

  constructor(public _wrapperSearchService: WrapperSearchService,
    private _acct: AccountService,
    public breadcrumbService: BreadcrumbService,
    public _funService: WrapperFuncService,
    private _advertismentService: AdvertismentService) {
    // breadcrumbService.setItem([
    //   { label: 'דף הבית', routerLink: ['/']/*, icon: 'pi pi-home'*/ },
    //   { label: 'אזור מנהל', routerLink: ['/manager'] },
    //   { label: 'רשימת לקוחות', routerLink: ['.'] }
    // ]);
    this._acct.globalStateChanged.subscribe((state) => {
      this._wrapperSearchService.LoginStatus$.next(state.loggedInStatus);
    });
    this._wrapperSearchService.Username$ = this._acct.currentUserName;
   }

   clientsList:ClientVM[] = [];
   @Output() filterOrders = new EventEmitter();
  ngOnInit(): void {
    this.getClients();
    
  }
  getClients() {
    this._advertismentService.getClients().subscribe(res=>{
      this.clientsList = res;
    });
  }

  editClient(client: ClientVM){
    this._funService.openClientForm(client.Id).subscribe(res=>{
      client.BusinessName = res.BusinessName;
      client.ContactName = res.BusinessName;
      client.Description = res.Description;
      client.Email = res.Email;
      client.Phone = res.Phone;
    });
  }
  deleteClient(client){
    Swal.fire({
      title: 'האם את בטוחה שאת רוצה למחוק את הלקוח??',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'כן, למחוק',
      cancelButtonText: 'לא, סליחה טעות'
    }).then((result) => {
      if (result.value) {
       this._advertismentService.deleteClient(client.Id).subscribe(res=>{
         console.log(res);
         this.clientsList = this.clientsList.filter(c=>c.Id!=client.Id);
       })
      } 
      });
  }
  filterOrdersByClient(client){
    this.filterOrders.emit(client.Id);
  }
}


