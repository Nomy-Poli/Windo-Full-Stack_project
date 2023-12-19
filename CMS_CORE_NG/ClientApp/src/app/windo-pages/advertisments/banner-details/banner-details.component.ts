import { Component, OnInit } from '@angular/core';
import { AdvertismentService } from 'src/app/services/advertisment.service';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';

@Component({
  selector: 'app-banner-details',
  templateUrl: './banner-details.component.html',
  styleUrls: ['./banner-details.component.css']
})
export class BannerDetailsComponent implements OnInit {

  constructor(private _wrapperFuncService:WrapperFuncService,
    private _advertismentService: AdvertismentService,
    private _funcService: WrapperFuncService) { }

  banner;
  
  ngOnInit(): void {
  }
  close(){
    this._funcService.closeDialog();
  }
  openRequestDialog()
  {
    this._wrapperFuncService.openRequestOrderDialog(this.banner.Makat);
  }
 

}
