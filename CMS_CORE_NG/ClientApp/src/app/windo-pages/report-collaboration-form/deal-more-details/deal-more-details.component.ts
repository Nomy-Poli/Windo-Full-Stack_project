import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BuisnessService, BuisnessVm, BusinessInCollaborationVM, BusinessNamesPicturesVM, GuideModel } from 'src/app/services/Buisness.service';
import { WrapperCollaborationsService } from 'src/app/services/wrapper-collaborations.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';

@Component({
  selector: 'app-deal-more-details',
  templateUrl: './deal-more-details.component.html',
  styleUrls: ['./deal-more-details.component.css']
})
export class DealMoreDetailsComponent implements OnInit {

  constructor(private _fb: FormBuilder, private _buisnessService: BuisnessService,
    private _collaborationService: WrapperCollaborationsService) { }
  @Output() submited = new EventEmitter();
  @Input() dealType;
  @Input() reporterBusiness: BuisnessVm;
  @Input() partnerBusiness: BusinessNamesPicturesVM;  
  @Input() businessInCollaboration: BusinessInCollaborationVM[];

  form: FormGroup;
  formArray: FormArray;
  uploadPicture = {};
  uploadPictures = [];
  hasImg = false;
  hasImgs = [false, false];
  workPictureGuide: GuideModel[] = [];



  ngOnInit(): void {
    switch (this.dealType) {
      case 1:
        this.form = this._fb.group({
          Picture: new FormControl(),
          ScopTransactionNIS: new FormControl(null, Validators.min(1)),
        });
        break;
      case 2:
        this.form = this._fb.group({
          QuoteReportsBusiness: new FormControl(),
          QuotePartnerBusiness: new FormControl(),
          JointExplanation: new FormControl()
        });
        break;
      case 3:
        this.form = this._fb.group({});
        this.businessInCollaboration.map(x=> {
          this.form.addControl('PartInCollaboration' + x.id,new FormControl('',[Validators.maxLength(100)]))
        });
        break;
      default:
        break;
    }

  }
  filesDropped(event, index) {  
    this.hasImg = true;
    let reader = new FileReader();
    if (index != '') {
      this.hasImgs[index - 1] = true;
      this.uploadPictures[index - 1] = event[0].file;
    }
   else {
        this.uploadPicture = event[0].file;
      }
      reader.readAsDataURL(event[0].file);
      reader.onload = () => {
        $('#img' + index).attr('src', reader.result as string);
      };
} 
  onWorkFileChanged(event, index) {
    if (event.target.files && event.target.files[0]) {
      this.hasImg = true;
      let reader = new FileReader();
      if (index != '') {
        this.hasImgs[index - 1] = true;
        this.uploadPictures[index - 1] = event.target.files[0];
      }
      else {
        this.uploadPicture = event.target.files[0];
      }
      reader.readAsDataURL(event.target.files[0]);
      reader.onload = () => {
        $('#img' + index).attr('src', reader.result as string);
      };
    }
    
  }
  triggerWorkInput(index) {
    $('#workpicfile' + index).trigger('click');
  }
//   //// -----------------------------dealType collaboration--------------------------------------------
//   triggerPic(business){
//     $('#uploadPic' + business.id).trigger('click');
    
//   }
//   uploadedFile(event,business){
//     business.hasImg = true;
//     if (event.target.files && event.target.files[0]) {
//       let reader = new FileReader();
    
//       business['pictureFile'] = event.target.files[0];
//       reader.readAsDataURL(event.target.files[0]);
//       reader.onload = () => {
//         $('#img' + business.id).attr('src', reader.result as string);
//       };
//     }
//   }
//   removeBusinessImg(business){
//     business.hasImg = false;
//     $("#img" + business.id).attr('src', " ");
//   }
// ////----------------------------------------------------------------------------------------------
  RemoveImage(index) {
    if (index != '') {
      this.hasImgs[index - 1] = false;
    }
    this.hasImg = false;
    $("#img" + index).attr('src', " ");
  }

  next() {
    if (this.form.valid) {
      // const formData = new FormData();
      // formData.append('PaiTransactionPicture', this.uploadPicture)
      switch (this.dealType) {
        case 1:
          this._collaborationService.paidTrnsactionPicture = this.uploadPicture;
          this.submited.emit(this.form.get('ScopTransactionNIS').value);

          break;
        case 2:
          this._collaborationService.barterDealPictures = this.uploadPictures;
          this.submited.emit(this.form.value);
          break;
        case 3:
          this._collaborationService.JoinProjectPicture = this.uploadPicture;
          this.businessInCollaboration.map(x=>{
            x.PartInCollaboration = this.form.get('PartInCollaboration' + x.id).value;
            if(this.reporterBusiness && this.reporterBusiness.id == x.id){
              x.IfReport = true;
            }
          })
          this.submited.emit(this.form.value);
          break;
        default:
          break;
      }
    }
  }


}
