import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { BehaviorSubject } from 'rxjs';
import { NetworkingGroupVM, NetworkingService } from 'src/app/services/Networking.service';
import { SharedDataService } from 'src/app/services/shared-data.service';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';
import Swal from 'sweetalert2';
import { NetworkingGroupBuisnessListComponent } from '../networking-group-buisness-list/networking-group-buisness-list.component';

@Component({
  selector: 'app-networking-group-list',
  templateUrl: './networking-group-list.component.html',
  styleUrls: ['./networking-group-list.component.css']
})
export class NetworkingGroupListComponent implements OnInit {

  constructor(
    private _funcService:WrapperFuncService,
    public _networkingService : NetworkingService,
    public _shaerdData : SharedDataService,
    
    
  ) { }
  @Output() IndexTabChange = new EventEmitter<number>();
  numberOfCurrentPage = 1;
  numberOfRowsInOnePage = 5;
  totalnumberOfPages;
  //TODO: SWITCH TO SUBJECT
   groupList : NetworkingGroupVM []= []
  ngOnInit(): void {
    this.getAllGroups();
  }
  getAllGroups()
  {
    this._shaerdData.groupList$.subscribe(res=>{
      if (res) this.setPagingNumber(res);
      this.groupList = res;
      console.log("group list", this.groupList);    
    })
  }
  setPagingNumber(groupList, numberOfCurrentPage = 1) {
    this.numberOfCurrentPage = numberOfCurrentPage;
    let tempNumPages = Math.floor(groupList.length / this.numberOfRowsInOnePage);
    // let tempNumPages = Math.floor(this.serverPagingObj.TotalRows / this.numberOfCardsInOnePage);
    if (Math.floor(groupList.length / tempNumPages) != this.numberOfRowsInOnePage)
        //אם אורך הרשימה אינו בכפולות של מספר הכרטיסים בעמוד
        this.totalnumberOfPages = tempNumPages + 1;
    else this.totalnumberOfPages = tempNumPages;
    //tempNumPages < list.length / this.numberOfCardsInOnePage ? tempNumPages + 1 : list.length;
}
onPageChange(event) {
  if (this.numberOfCurrentPage <= this.totalnumberOfPages) {
      let tempStart = event.page * this.numberOfRowsInOnePage;
      let tempEnd;
      tempEnd = tempStart + this.numberOfRowsInOnePage;
      this.numberOfCurrentPage = event.page + 1;
  }
}
  addGroup(){
    this._funcService.openNetworkingGroupForm().subscribe(res=>{
      this.groupList.push(res);
      this._shaerdData.groupList$.next(this.groupList)
    })
  }
  buisnessDetails(idGroup)
  {
    console.log("id group", idGroup);
    this.IndexTabChange.emit(idGroup);
  }
  UpdateGroup(group: NetworkingGroupVM)
  {
    this._funcService.openNetworkingGroupForm(group).subscribe((res: NetworkingGroupVM) =>{
     
     if(res){
         console.log("update",res);
         group.GroupName = res.GroupName;
         group.Description = res.Description;
         group.ManagerBusiness =res.ManagerBusiness;
         group.Area = res.Area;
     }
    });
  }
  deleteGroup(Group :NetworkingGroupVM)
  {
   
    console.log("idGroup" ,Group.Id);
    
    Swal.fire({
      title: 'האם את בטוחה שאת רוצה למחוק את הקבוצה??',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'כן, למחוק',
      cancelButtonText: 'לא, סליחה טעות'
    }).then((result) => {
      if (result.value) {
       this._networkingService.freezingGroup(Group.Id).subscribe(res=>{
         console.log("מה שחוזר מ DEL",res);
        this.groupList = this.groupList.filter(c=>c.Id!=Group.Id);
         this._shaerdData.groupList$.next(this.groupList);
       });
      } 
    });
  }
  

}
