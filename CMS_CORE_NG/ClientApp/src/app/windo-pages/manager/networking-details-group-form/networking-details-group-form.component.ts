import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BuisnessService, BusinessForCardVM } from 'src/app/services/Buisness.service';
import { NetworkingGroupBusinessVM, NetworkingService } from 'src/app/services/Networking.service';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';
import { SharedDataService } from 'src/app/services/shared-data.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';
import { IDropdownSettings } from 'ng-multiselect-dropdown';

import Swal from 'sweetalert2';

@Component({
  selector: 'app-networking-details-group-form',
  templateUrl: './networking-details-group-form.component.html',
  styleUrls: ['./networking-details-group-form.component.css']
})
export class NetworkingDetailsGroupFormComponent implements OnInit {

  constructor(
    public _funcService: WrapperFuncService,
    public _networkingService : NetworkingService,
    public _buisnessService : BuisnessService,
    public _shaerdData : SharedDataService
  ) {}
newGroupBuisness : NetworkingGroupBusinessVM = {} as  NetworkingGroupBusinessVM;
groupBusiness;//אם זה עידכון נכנס כל האובייקט לגרופביזנס ואם זה הוספה נכנס אליו רק ID של הקבוצה
buisnessList : BusinessForCardVM[] = [] ;
businessArry;

@Output() closed = new EventEmitter();
@Output() Output = new EventEmitter();
form:FormGroup = new FormGroup({
  idgroup : new FormControl({value:'',disabled: true}),
  buisness :new FormControl('',[Validators.required]),
  role : new FormControl('', [Validators.required]),
})
ngOnInit(): void {
  this._networkingService.getGroupById(this.groupBusiness).subscribe(res=>{
  console.log("get group", res);
})

    // הבאת רשימת העסקים
    this._buisnessService.getBusinessNamesPictUser().subscribe(res=>{
      console.log("buisness list " , res);
      this.buisnessList = res ;
  })

  //בדיקה אם זה עידכון ע"י role 
  if(this.groupBusiness.Role){
    console.log(this.groupBusiness);
    this.form.patchValue({      
    idgroup: this.groupBusiness.GroupId,
    buisness: this.groupBusiness?
    [{buisnessName:this.groupBusiness.buisnessName}]:null,
    role: this.groupBusiness.Role
    })
    this.getGroupWithAllBuisness(this.groupBusiness.GroupId)

  }
  else{
    this.form.patchValue({
      idgroup: this.groupBusiness,
  });
  this.getGroupWithAllBuisness(this.groupBusiness)

  }
 
}
getGroupWithAllBuisness(IdGroup){
  this._networkingService.getGroupById(IdGroup).subscribe(res=>{
    this.businessArry=res; 
  })
}
dropdownBusinessesSettings = {
  singleSelection: true,
  idField: 'id',
  textField: 'buisnessName',
  searchPlaceholderText: 'התחילי להקליד את שם העסק',
  noDataAvailablePlaceholderText: 'לא נמצאו תוצאות',
  enableCheckAll: false,
  allowSearchFilter: true,
  closeDropDownOnSelection: false,
};

searchBuisness(event):any[] {
  if (event == '') {
    this.buisnessList = this.buisnessList;
  }
  else {
    this.buisnessList= this.buisnessList.filter(x =>
      x.buisnessName.includes(event) || x.buisnessName.includes(event));
  }
  return this.buisnessList;
}


submitForm(){
console.log(this.form.value,"form-value");
  if(this.form.valid){
      let formValue = this.form.value;
      this.newGroupBuisness.GroupId = Number(formValue.GroupId);
      this.newGroupBuisness.Role=formValue.role;


       if(this.groupBusiness.Role){
        this.newGroupBuisness.Id= this.groupBusiness.Id
        this.newGroupBuisness.buisnessName = this.groupBusiness.buisnessName;
        console.log(this.newGroupBuisness);
        this._networkingService.updateBuisnessFromGroup(this.newGroupBuisness).subscribe(res=>{
          Swal.fire('החברה התעדכנה בהצלחה!').then((val)=>{
            this._shaerdData.businessListForGroup.next[this.newGroupBuisness.Id]=this.newGroupBuisness;
            this.closed.emit(this.newGroupBuisness);
         this._funcService.closeDialog();
       });
     });
      this.groupBusiness= this.newGroupBuisness;
      }
      else{
        //בדיקה אם העסק קיים בקבוצה
      let sameBusiness = this.businessArry.filter(p=> p.buisnessName ==formValue.buisness[0].buisnessName);
      console.log(sameBusiness,"sameBusiness");
      if(sameBusiness[0]){
        Swal.fire('העסק כבר קיים בקבוצה!').then((val)=>{
       
        })
      }
      else {
        this.newGroupBuisness.BusinessId= Number(formValue.buisness[0].id);
        this.newGroupBuisness.buisnessName = formValue.buisness[0].buisnessName;
        this.newGroupBuisness.GroupId=this.groupBusiness;
        console.log(this.newGroupBuisness);
        this._networkingService.addBuisnessToGroup(this.newGroupBuisness).subscribe(res=>{
        this.newGroupBuisness.Id =res;
          Swal.fire('החברה נוספה בהצלחה!').then((val)=>{
        // this._shaerdData.businessListForGroup[this.newGroupBuisness.Id] = this.newGroupBuisness;
        this.closed.emit(this.newGroupBuisness);
        this._funcService.closeDialog();
       });
     });  
      }
       
    } 
  }
}
  close(){
    this._funcService.closeDialog();
 }
}
