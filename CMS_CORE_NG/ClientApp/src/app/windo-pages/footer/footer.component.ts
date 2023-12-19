import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent implements OnInit {
  
  addresMapLink = "https://www.google.com/maps/place/Jerusalem+Hub/@31.7889353,35.2029564,17z/data=!3m1!4b1!4m5!3m4!1s0x1502d6255d8d067b:0x4ddf0cf52f59aeee!8m2!3d31.7889353!4d35.2051451"
  //"https://www.google.com/maps/search/?api=1&"
  
  constructor() { }

  ngOnInit() {

  }
  mailTo() {
    $("#a").trigger('click')
  }
}
