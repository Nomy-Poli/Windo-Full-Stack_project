import { Component, Input, OnInit } from '@angular/core';
import { AdvertismentService, BannerVM } from 'src/app/services/advertisment.service';
@Component({
  selector: 'app-advertisment-area',
  templateUrl: './advertisment-area.component.html',
  styleUrls: ['./advertisment-area.component.scss']
})
export class AdvertismentAreaComponent implements OnInit {
  constructor(private _advertismentService: AdvertismentService) { }
  @Input() makat;
  bannerObject: BannerVM;
  srcImg = '';
  ngOnInit(): void {
    this._advertismentService.getBannerWithPic(this.makat).subscribe(res => {
      this.bannerObject = res;
      if (this.bannerObject.AdvertismentServiceOrder) {
        this.srcImg = '../../../../../assets/advertisments/' + this.makat + '/' + this.bannerObject.AdvertismentServiceOrder.PicGuid+'.jpg';
      }
      else
        this.srcImg = '../../../../../assets/advertisments/' + this.makat + '/default/' + this.bannerObject.DefaultPicGuid+'.jpg';
    });
  }
}