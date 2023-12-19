
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
// import { AngularEditorConfig } from '@kolkov/angular-editor';
import { AccountService } from 'src/app/services/account.service';
import { BuisnessService } from 'src/app/services/Buisness.service';
import { FromTable, MessageService, MessageVM } from 'src/app/services/Message.service';
import { NetworkingGroupVM, NetworkingService } from 'src/app/services/Networking.service';
import { SharedDataService } from 'src/app/services/shared-data.service';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';

@Component({
  selector: 'app-new-messag',
  templateUrl: './new-messag.component.html',
  styleUrls: ['./new-messag.component.scss']

})
export class NewMessagComponent implements OnInit {
  formMessage: FormGroup;
  businessList = [];
  ifSuc: boolean = false;
  isManager:boolean;
  businessGroupArry;
  constructor(public _messageService: MessageService, public _acct: AccountService,
    private _buisnessService: BuisnessService, public _funcService: WrapperFuncService,
    public WrapperSearchSer: WrapperSearchService, private ref: DynamicDialogRef, public config: DynamicDialogConfig,
    public _shaerdData : SharedDataService,
    public _networkingService : NetworkingService) { }
  


  formats = ['background', 'bold', 'color', 'font', 'italic', 'link', 'size', 'underline'];
  dropdownBusinessesSettings = {
    singleSelection: false,
    idField: 'id',
    textField: 'buisnessName',
    searchPlaceholderText: 'התחילי להקליד את שם העסק',
    noDataAvailablePlaceholderText: 'לא נמצאו תוצאות',
    enableCheckAll: false,
    allowSearchFilter: true,
    closeDropDownOnSelection: false,
    // itemsShowLimit: 3,
    // limitSelection: 21
  }
  dropdownGroupsSettings = {
    singleSelection: true,
    idField: 'Id',
    textField: 'GroupName',
    searchPlaceholderText: 'התחילי להקליד את שם הקבוצה',
    noDataAvailablePlaceholderText: 'לא נמצאו תוצאות',
    enableCheckAll: false,
    allowSearchFilter: true,
    closeDropDownOnSelection: true,
  }
  Subject;
  isLoading = false;
  groupList : NetworkingGroupVM []= [];
  ngOnInit(): void {
    this.Subject = this.config.data?.subject? this.config.data?.subject:'';
    this.formMessage = new FormGroup({
      Subject: new FormControl({value:this.Subject ,disabled:this.Subject? true: false},[Validators.required,Validators.minLength(3),Validators.maxLength(80)]),
      MessageText: new FormControl(''),
      // CollaborationType: new FormControl('0'),
      ListMessagesTo: new FormControl('',[Validators.maxLength(10)]),
      ListMessagesToGroup: new FormControl([])
    });
    this.getBusinesList();
    this.getAllGroups();
  }
  get getBusiness() {
    return this.businessList.reduce((acc, curr) => {
      acc[curr.id] = curr;
      return acc;
    }, {});
  }
  
  getAllGroups()
    {
     console.log(this.formMessage.value.ListMessagesToGroup);

           console.log(this.formMessage.value.ListMessagesTo);

      this._shaerdData.groupList$.subscribe(res=>{
        this.groupList = res;
        console.log("group list", this.groupList);    
      })
    }
  
  getBusinesList() {
    if (!this.WrapperSearchSer.shrunkenBuisnessList) {
      this._buisnessService.getBusinessNamesPictUser().subscribe(res => {
        console.log(res);
        this.WrapperSearchSer.shrunkenBuisnessList = res;
        this.businessList = res;
        if (this.config.data?.businessId) {
          this.setFirstBusiness();
        }
      });
    }
    else {
      this.businessList = this.WrapperSearchSer.shrunkenBuisnessList;
      if (this.config.data?.businessId) {
        this.setFirstBusiness();
      }
    }

  }

  setFirstBusiness() {
    let findBusiness = this.businessList.find(x => x.id == this.config.data?.businessId)
    if (findBusiness) {
      this.formMessage.get('ListMessagesTo').setValue([{ id: findBusiness.id, buisnessName: findBusiness.buisnessName }]);
    }
  }
  close() {
    $('.model').hide();
  }
  getGroupWithAllBuisness(IdGroup){
    this._networkingService.getGroupById(IdGroup).subscribe(res=>{
      this._shaerdData.businessListForGroup.next(res);
      this.businessGroupArry=res; 
      console.log(this.businessGroupArry);
      
    })
  }


send() {
  console.log(this.businessGroupArry);
  
    if (this.formMessage.valid) {
      console.log(this.formMessage.value);
      // var name=FromTable[Number(this.formMessage.get('CollaborationType').value)];
      let newMessage: MessageVM = {
        Id: "00000000-0000-0000-0000-000000000000",
        BusinessIdFrom: this._acct.currentBusiness.value.id,
        EmailFrom: this._acct.Email.value,
        ParentMessagesId: null, //this.message.Id,
        MessageText: this.formMessage.get('MessageText').value,
        Subject: this.formMessage.get('Subject').value,
        CreatedAt: new Date(),
        ListMessagesTo: []
      }
      // if (this.formMessage.get('CollaborationType').value && this.formMessage.get('CollaborationType').value != "0") {
      //   newMessage.CollaborationType = parseInt(this.formMessage.get('CollaborationType').value);
      // }
if(this.formMessage.value.ListMessagesToGroup.length>0){
    this._networkingService.getGroupById(this.formMessage.value.ListMessagesToGroup[0].Id).subscribe(res=>{
        this._shaerdData.businessListForGroup.next(res);
        this.businessGroupArry=res; 
        console.log(this.businessGroupArry);
    
        if(this.businessGroupArry){
          this.businessGroupArry.map(mto => {
            newMessage.ListMessagesTo.push({ Id: 0, BusinessIdTo: mto.BusinessId, IsNew: true ,BusinessIdFrom: newMessage.BusinessIdFrom});
          });}})}
          else{
            let ListMessagesTo: { id: number, buisnessName: string }[] = this.formMessage.get('ListMessagesTo').value;
            if (ListMessagesTo && ListMessagesTo.length) {
              ListMessagesTo.map(mto => {
                newMessage.ListMessagesTo.push({ Id: 0, BusinessIdTo: mto.id, IsNew: true , BusinessIdFrom: newMessage.BusinessIdFrom});
              });
            }
          }
          console.log(newMessage.ListMessagesTo);
          this.isLoading = true;
          this._messageService.createMessage(newMessage).subscribe((res) => {
            
            this.ifSuc = true
            this.isLoading = false;
            setTimeout(() => {
              this.ref.close(res.Id);
            }, 2000);
            console.log("res");
            //window.location.reload();
          });

        }
      }
      
      // this.formMessage.value.ListMessagesTo =[{}]
    }


