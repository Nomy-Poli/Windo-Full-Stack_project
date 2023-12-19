import { Component, OnInit } from '@angular/core';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';

@Component({
  selector: 'app-advertising',
  templateUrl: './advertising.component.html',
  styleUrls: ['./advertising.component.css']
})
export class AdvertisingComponent implements OnInit {
  randomPictures = new Array<{}>();
  constructor(private _wrapperSearchService: WrapperSearchService) { }

  ngOnInit(): void {
    this.setRandomPicture();
  }

  setRandomPicture() {
    let random;
    this._wrapperSearchService._afterFilterBuisnessListSubject.subscribe((res) => {
      if (this.randomPictures.length == 6 || !res) return;
      this.randomPictures = []
      for (let i = 0; i < 6; i++) {
        random = Math.floor(Math.random() * (res?.length));
        if (res && res[random]) {
          if (res[random].logoPictureId != "00000000-0000-0000-0000-000000000000" && !this.randomPictures.find(x => x['id'] == res[random].id))
            this.randomPictures.push({ 'id': res[random].id, 'logoPictureId': res[random].logoPictureId , buisnessName: res[random].buisnessName});
          else
            i--;
        }
      }
    });
  }

}
