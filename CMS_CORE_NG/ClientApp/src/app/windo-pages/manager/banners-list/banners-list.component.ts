import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { AdvertismentService } from 'src/app/services/advertisment.service';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-banners-list',
  templateUrl: './banners-list.component.html',
  styleUrls: ['./banners-list.component.css']
})
export class BannersListComponent implements OnInit {

  constructor(public _wrapperSearchService: WrapperSearchService,
    private _acct: AccountService,
    public _funService: WrapperFuncService,
    private _advertismentService: AdvertismentService) { }


    bannersList = [];


  ngOnInit(): void {
    this.getBanners();
  }
  getBanners() {
    this._advertismentService.getBanners().subscribe(res=>{
      this.bannersList = res;
    })
  }
  addBanner(){
    this._funService.openBannerForm(null).subscribe(res=>{
      console.log(res);
      this.bannersList.push(res);
    });
  }

  editBanner(banner){
    this._funService.openBannerForm(banner.Id).subscribe(res=>{
      console.log(res);
      banner = res;
    });
  }
  deleteBanner(banner){
    Swal.fire({
      title: 'האם את בטוחה שאת רוצה למחוק את ההזמנה??',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'כן, למחוק',
      cancelButtonText: 'לא, סליחה טעות'
    }).then((result) => {
      if (result.value) {
        this.bannersList = this.bannersList.filter(x=>x.Id!= banner.Id);
        console.log("banner list:",this.bannersList);
        
       this._advertismentService.deleteBaner(banner.Id).subscribe(res=>{
         console.log(res);
         this.bannersList = this.bannersList.filter(c=>c.Id!=banner.Id);
       });
      } 
    });
  }
}
