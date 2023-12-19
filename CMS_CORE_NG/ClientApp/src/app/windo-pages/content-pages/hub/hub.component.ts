import { Component, OnInit } from '@angular/core';
import { HostListener } from '@angular/core';

@Component({
  selector: 'app-hub',
  templateUrl: './hub.component.html',
  styleUrls: ['./hub.component.css']
})
export class HubComponent implements OnInit {
  constructor() { }

  ngOnInit(): void {
  }
  // @HostListener('document:click', ['$event'])
  // public onClick(event: any): void {
  //   const mailtoLink = 'mailto:rc@temech.org?subject=הגעתי מאתר תמך ואשמח למידע על .... ';
  //   window.location.href = mailtoLink;
  //   console.log(mailtoLink);
  // }
  public sendEmail(): void {
    const mailtoLink = 'mailto:rc@temech.org?subject=הגעתי מאתר תמך ואשמח למידע על .... ';
    window.location.href = mailtoLink;
    console.log(mailtoLink);
  }
  public openEmailInGmail(): void {
    const gmailUrl = 'https://mail.google.com/mail/u/0/?view=cm&fs=1&to=rc@temech.org&su=הגעתי מאתר תמך ואשמח למידע על ...';
    window.open(gmailUrl, '_blank');
}
}
