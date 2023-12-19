import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { BuisnessService, CollaborationTypeVM } from 'src/app/services/Buisness.service';
import { WrapperCollaborationsService } from 'src/app/services/wrapper-collaborations.service';

@Component({
  selector: 'app-report-deal-types',
  templateUrl: './report-deal-types.component.html',
  styleUrls: ['./report-deal-types.component.css']
})
export class ReportDealTypesComponent implements OnInit {

  constructor(private _businessService: BuisnessService, private _collaborationService: WrapperCollaborationsService) { }
  @Output() submited = new EventEmitter();
  @Output() closed = new EventEmitter();

  @Input() dealType;
  optionIndex: number;
  showError = false;
  errorMessage = 'לא בחרת עדיין סוג שת"פ'
  showCollaborationOptions = false;
  isCloseClick =false;
  CollaborationOptions: CollaborationTypeVM[] = [];

  selectedOption;

  ngOnInit(): void {
  }

  optionClicked(opt) {
    this.optionIndex = opt;
    if (opt == 3) {
      if (!this.CollaborationOptions.length)
        this.getCollaborationOptions();
      else{
        this.showCollaborationOptions = true;
      }
    }
    else {
      this.showCollaborationOptions = false;
    }
  }

  getCollaborationOptions() {
    // this.CollaborationOptions = [{Id:1,Description:'מיזם משותף'},
    // {Id:0,Description:'מוצר משותף'},
    // {Id:0,Description:'שיווק הדדי'},
    // {Id:0,Description:'השקעה ושותפות'}];
    // this.CollaborationOptions.forEach(obj=>{
    //   this._businessService.createCollaborationType(obj).subscribe(res=>{
    //     console.log(res,obj);
    //   });
    // })

    this._businessService.getCollaborationTypes().subscribe(res => {
      this.CollaborationOptions = res;
      this.showCollaborationOptions = true;
    })
  }

  collaborationOptionChoosed(collaborationType) {
    if (collaborationType.description != null) {
      collaborationType.Description = collaborationType.description;
    }
    this._collaborationService.collaborationType = collaborationType;
    this.showError = false;
  }


  next() {
    if (this.optionIndex && (this.optionIndex != 3 || this._collaborationService.collaborationType)) {
      this.submited.emit(this.optionIndex);
    }
    else {
      if(this.optionIndex == 3){
        this.errorMessage = 'לא בחרת עדיין סוג שיתוף פעולה';
      }
      this.showError = true;
    }
  }

}
