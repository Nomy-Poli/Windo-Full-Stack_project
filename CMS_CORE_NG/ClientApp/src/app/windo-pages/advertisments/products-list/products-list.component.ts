import { Component, OnInit } from '@angular/core';
import { AdvertismentService, CatalogServiceVM } from 'src/app/services/advertisment.service';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';
import { AccountService } from 'src/app/services/account.service';

import Swal from 'sweetalert2';

// type NewType = CatalogServiceVM[];

@Component({
  selector: 'app-products-list',
  templateUrl: './products-list.component.html',
  styleUrls: ['./products-list.component.scss']
})
export class ProductsListComponent implements OnInit {
  catalogServices: CatalogServiceVM[]=[];
  catalogServicesAfterFilter:CatalogServiceVM[] = [];

  constructor( 
    private _advertismentService: AdvertismentService,
    private _funcService:WrapperFuncService,
    private _acct: AccountService,
    ) { 
  }
  
  ngOnInit(): void {
  this.getCatalogServices();
  }
  getCatalogServices(){
    this._advertismentService.getCatalogServices().subscribe((res)=>{
      console.log(res);
      
      this.catalogServices=res;
    })
  }
  editService(service){
    this._funcService.openServiceForm(service).subscribe((res: CatalogServiceVM)=>{
      service = res;
    });
  }
  deleteService(services){
    Swal.fire({
      title: 'האם את בטוחה שאת רוצה למחוק את ההזמנה??',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'כן, למחוק',
      cancelButtonText: 'לא, סליחה טעות'
    }).then((result) => {
      if (result.value) {
        this.catalogServices = this.catalogServices.filter(x=>x.Id!= services.Id);
        this.catalogServicesAfterFilter = this.catalogServicesAfterFilter.filter(x=>x.Id!= services.Id);
        console.log("catalogServices:",this.catalogServices);
        
       this._advertismentService.deleteService(services.Id).subscribe(res=>{
         console.log(res);
         this.catalogServices = this.catalogServices.filter(c=>c.Id!=services.Id);
       });
      } 
    });
  }
  addService(){
    this._funcService.openServiceForm().subscribe(res=>{
      console.log(res);
      this.catalogServices.push(res);
    });
  }
}
