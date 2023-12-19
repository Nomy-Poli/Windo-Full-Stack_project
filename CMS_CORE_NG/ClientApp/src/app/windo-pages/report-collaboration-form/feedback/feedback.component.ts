import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { PaidTransactionVM } from 'src/app/services/Buisness.service';
import { WrapperCollaborationsService } from 'src/app/services/wrapper-collaborations.service';

@Component({
  selector: 'app-feedback',
  templateUrl: './feedback.component.html',
  styleUrls: ['./feedback.component.css']
})
export class FeedbackComponent implements OnInit {

  constructor(private _fb: FormBuilder, private _collaborationService: WrapperCollaborationsService) { }
  @Output() submited = new EventEmitter();
  @Input() dealType = 2;
  form: FormGroup;

  iconsNames = {
    Price: false,
    Availability: false,
    Service: false,
    Professionalism: false,
    Flexable: false, //גמישות 
    //
    MoreLeisure: false,//יותר פנאי
    MoreShopping: false, // יותר קניות
    IncreasingRevenue: false, // הגדלת הכנסות
    ReducingExpenses: false,//הקטנת הוצאות
    ReducingEffort: false, // צמצום מאמץ 
  }
  Review;
  header = 'בחרי את הצל"שים שאת מעוניינת להעניק לעסק שסיפק לך שירות';
  Enterprise = true;
  Creativity = true;
  ExposureToNewAudiences = true;
  errorMessage = '';
  ngOnInit(): void {
    window.scrollTo(0, 0);
    this.form = this._fb.group({
      // Professionalism: new FormControl(false),
    });
    switch (this.dealType) {
      case 1:
        // this.form.addControl('Availability', new FormControl(false));
        // this.form.addControl('Service', new FormControl(false));
        // this.form.addControl('Price', new FormControl(false));
        this.form.addControl('Review', new FormControl('נהניתי במיוחד מ '));
        break;
      case 2:
        this.header = 'מה הרווחתן מעסקת הברטר?'
        // this.form.addControl('Availability', new FormControl(false));
        // this.form.addControl('Service', new FormControl(false));
        // this.form.addControl('FairConsiderationForTransaction', new FormControl(false));
        this.form.addControl('ConfirmedByPartner', new FormControl(false));
        break;
      case 3:
        this.header = 'מה הרווחתן משיתוף הפעולה?'
        // this.form.addControl('Enterprise', new FormControl(false));
        // this.form.addControl('Creativity', new FormControl(false));
        // this.form.addControl('ExposureToNewAudiences', new FormControl(false));
        this.form.addControl('ConfirmedByPartners', new FormControl(false));
      default:
        break;
    }
  }


  iconClicked(iconName) {
    this.iconsNames[iconName] = !this.iconsNames[iconName];
    console.log(iconName, this.iconsNames[iconName]);
  }

  keyDown(event) {
    if (event.keyCode == 8 && this.form.get('Review').value.length <= 15) {
      event.preventDefault();
    }
  }

  next() {
    if ((this.dealType == 2 && !this.form.get('ConfirmedByPartner').value) || (this.dealType == 3 && !this.form.get('ConfirmedByPartners').value)) {
      this.errorMessage = 'רק תאשרי לנו שהכול מתואם... תודה!'
    }
    else {
      if (this.dealType == 1) {
        this.iconsNames['Review'] = this.form.get('Review').value
      }
      this.submited.emit(this.iconsNames);
    }

  }

}
//"Not a valid origin for the client: http://localhost:5000 has not been registered for client ID 827381401565-s3mf6vk4vlffok2fqqafaub1np74v7ou.apps.googleusercontent.com. Please go to https://console.developers.google.com/ and register this origin for your project's client ID."