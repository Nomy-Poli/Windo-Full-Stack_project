import { Component, EventEmitter, Injectable, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AreaVm, BuisnessService, BusinessForCardVM } from 'src/app/services/Buisness.service';
import { NetworkingGroupVM, NetworkingService } from 'src/app/services/Networking.service';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';
import {formatDate, getLocaleEraNames} from '@angular/common';
import Swal from 'sweetalert2';
import { SharedDataService } from 'src/app/services/shared-data.service';

@Component({
  selector: 'app-networking-group-form',
  templateUrl: './networking-group-form.component.html',
  styleUrls: ['./networking-group-form.component.css']
})
export class NetworkingGroupFormComponent implements OnInit {

  constructor(
    public _funcService: WrapperFuncService,
    public _networkingService : NetworkingService,
    public _buisnessService : BuisnessService,
    public _shaerdData : SharedDataService
  ) { }
  @Output() closed = new EventEmitter();
  @Output() Output = new EventEmitter();
  group;
  newgroup;
  areaList: AreaVm[]=[];
  buisnessList : BusinessForCardVM[] = [] ;
  form:FormGroup = new FormGroup({
    groupName : new FormControl('', [Validators.required]),
    managerBuisnessName :new FormControl('', [Validators.required]),
    discription : new FormControl(''),
    area : new FormControl('')
  })
  ngOnInit(): void { 
    this.getBusinessList();
    this.getArea();

    if(this.group){
      console.log("group",this.group);
      this.form.patchValue({
        groupName : this.group.GroupName,
        managerBuisnessName: this.group.ManagerBusiness?
      [{id:this.group.ManagerBusinessId,buisnessName:this.group.ManagerBusiness.buisnessName}]:null,
        discription : this.group.Description,
        area :this.group.Area
      });
    }
  }


  getBusinessList(){
       // הבאת רשימת העסקים
       this._buisnessService.getListOfBuisnesses().subscribe(res=>{
        console.log("buisness list " , res);
        this.buisnessList = res ;
        // if(this.group)
        // this.form.patchValue({
        //   managerBuisnessName: this.buisnessList.find(x=> x.id === this.group.ManagerBusinessId),
        // });
    })
  }
  getArea(){
      // הבאת רשימת האזורים הקימים
      this._buisnessService.getAreasList().subscribe(res=>{
      console.log("area list", res)
      this.areaList=res;
      if(this.group)
      this.form.patchValue({
        area: this.areaList.find(x=> x.id === this.group.Area.id),
      });
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
  
  // dropdownBuisnessSettings = {
  //   clearSearchFilter:true,
  //   singleSelection: true,
  //   idField: 'id',
  //   textField: 'buisnessName',
  //   searchPlaceholderText:'התחילי להקליד שם עסק',
  //   noDataAvailablePlaceholderText:'לא נמצאו תוצאות',
  //   enableCheckAll:false,
  //   allowSearchFilter: true,
  //   closeDropDownOnSelection:true
  // }
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
    if(this.form.valid){
      this.newgroup = {} as NetworkingGroupVM;
      this.newgroup.ManagerBusiness= {} as BusinessForCardVM ;
      this.newgroup.Area= {} as AreaVm ;
      let formValue = this.form.value;
      console.log("form",formValue);    
      this.newgroup.GroupName = formValue.groupName;
      this.newgroup.Description=formValue.discription;
      this.newgroup.AreaId=Number(formValue.area.id);
      this.newgroup.Area=formValue.area;
      this.newgroup.CreationDate = new Date();
      this.newgroup.IsActive = true;
     
     console.log("newGroup",this.newgroup);

      if(this.group){
        this.newgroup.ManagerBusinessName =formValue.managerBuisnessName[0].buisnessName;
        this.newgroup.ManagerBusiness =formValue.managerBuisnessName[0];
        this.newgroup.ManagerBusinessId =Number(formValue.managerBuisnessName[0].id);
        this.newgroup.Id = this.group.Id;
        this._networkingService.updateGroup(this.newgroup).subscribe(res=>{
          Swal.fire('פרטי הקבוצה נשמרו במערכת').then((val)=>{ 
            this.newgroup.Id=res;
            this._shaerdData.groupList$[this.newgroup.Id] = this.newgroup;
           this.closed.emit(this.newgroup);
           this._funcService.closeDialog();
         });
       })

       this.group = this.newgroup; 
      }
      else{
        this.newgroup.ManagerBusinessName =formValue.managerBuisnessName[0].buisnessName;
        this.newgroup.ManagerBusiness =formValue.managerBuisnessName[0];
          this.newgroup.ManagerBusinessId =Number(formValue.managerBuisnessName[0].id);
          this._networkingService.createNewNetworkingGroup(this.newgroup).subscribe(res=>{
            // this.newgroup.ManagerBusiness= formValue.managerBuisnessName;
            // this.newgroup.Area= formValue.area;
            Swal.fire('פרטי הקבוצה נשמרו במערכת').then((val)=>{
              this.newgroup.Id=res;
            this.closed.emit(this.newgroup);
            this._funcService.closeDialog();
        });
      }) 
      }
    }
  }

  close(){
    this._funcService.closeDialog();
 }
}
