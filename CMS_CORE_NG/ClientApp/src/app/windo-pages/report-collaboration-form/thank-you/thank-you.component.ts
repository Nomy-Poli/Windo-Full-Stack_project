import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-thank-you',
  templateUrl: './thank-you.component.html',
  styleUrls: ['./thank-you.component.css']
})
export class ThankYouComponent implements OnInit {

  constructor() { }
  @Output() moreCollaboration = new EventEmitter();
  @Input() dealType;

  ngOnInit(): void {

  }

  moreCollaborationClicked(){
    this.moreCollaboration.emit();
  }

  exit(){
    window.history.back();
  }
}
