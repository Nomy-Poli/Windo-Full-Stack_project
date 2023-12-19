import { Component, Input, OnInit } from '@angular/core';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';

@Component({
  selector: 'app-big-picture',
  templateUrl: './big-picture.component.html',
  styleUrls: ['./big-picture.component.css']
})
export class BigPictureComponent implements OnInit {

  constructor(public _wrapperFuncService: WrapperFuncService) { }
  @Input() src;
  ngOnInit(): void {
  }

}
