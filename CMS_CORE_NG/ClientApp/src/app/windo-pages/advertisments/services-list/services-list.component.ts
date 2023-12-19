import { Component, OnInit } from '@angular/core';
import { AdvertismentService, CatalogServiceVM, ServiceType, ServiceTypeVM } from 'src/app/services/advertisment.service';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';
import { AccountService } from 'src/app/services/account.service';

import Swal from 'sweetalert2';

// type NewType = CatalogServiceVM[];

@Component({
  selector: 'app-services-list',
  templateUrl: './services-list.component.html',
  styleUrls: ['./services-list.component.scss']
})
export class ServicesListComponent implements OnInit {
  WindoSiteServices: ServiceTypeVM[]=[];

  constructor( 
    private _advertismentService: AdvertismentService,
    private _funcService:WrapperFuncService,
    private _acct: AccountService,
    ) { 
  }
  
  ngOnInit(): void {
  this.getWindoSiteServices();
  console.log("hhhhhhhh");
  
  }
  getWindoSiteServices(){
    this._advertismentService.getWindoSiteServices().subscribe((res)=>{
      console.log("catalog",res);
      this.WindoSiteServices=res;
    })
  }
  editService(SiteServices){
    this._funcService.openSiteServicesForm(SiteServices).subscribe((res: ServiceTypeVM)=>{
      SiteServices = res;
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
        this.WindoSiteServices = this.WindoSiteServices.filter(x=>x.Id!= services.Id);
        // this.catalogServicesAfterFilter = this.catalogServicesAfterFilter.filter(x=>x.Id!= services.Id);
        console.log("WindoSiteServices:",this.WindoSiteServices);
        
       this._advertismentService.deleteWindoSiteService(services.Id).subscribe(res=>{
         console.log(res);
         this.WindoSiteServices = this.WindoSiteServices.filter(c=>c.Id!=services.Id);
       });
      } 
    });
  }
  addService(){
    this._funcService.openSiteServicesForm().subscribe(res=>{
      console.log(res);
      this.WindoSiteServices.push(res);
    });
  }
}
