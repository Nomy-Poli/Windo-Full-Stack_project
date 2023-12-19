import { Component, Input, OnInit } from '@angular/core';
import { BuisnessService, BusinessNamesPicUserIdVM } from 'src/app/services/Buisness.service';

@Component({
  selector: 'app-business-template',
  templateUrl: './business-template.component.html',
  styleUrls: ['./business-template.component.css']
})
export class BusinessTemplateComponent implements OnInit {

  constructor(
   private _buisnessService : BuisnessService
  ) { }

  @Input() business: BusinessNamesPicUserIdVM;
  @Input() size: string;
  ngOnInit(): void {
    console.log("buisness- t",this.business);
    
  }

}
