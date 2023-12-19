import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';

@Component({
  selector: 'app-partner-confirmed',
  templateUrl: './partner-confirmed.component.html',
  styleUrls: ['./partner-confirmed.component.css']
})
export class PartnerConfirmedComponent implements OnInit {

  constructor(public _wrapperFuncService: WrapperFuncService) { }

  @Output() passRes = new EventEmitter();
  @Input() dealType;
  ngOnInit(): void {
  }

  confirmed(){
    this.passRes.emit(true);
  }
}
