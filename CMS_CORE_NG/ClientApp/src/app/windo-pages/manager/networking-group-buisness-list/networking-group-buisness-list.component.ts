import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TreeNode } from 'primeng/api';
import {TreeTableModule} from 'primeng/treetable';
import { SharedDataService } from 'src/app/services/shared-data.service';
import { SelectItem } from 'primeng/api';
import { MessageService } from 'primeng/api';
import { NetworkingGroupBusinessVM, NetworkingGroupVM, NetworkingService } from 'src/app/services/Networking.service';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';
import Swal from 'sweetalert2';
import * as FileSaver from 'file-saver';



@Component({
  selector: 'app-networking-group-buisness-list',
  templateUrl: './networking-group-buisness-list.component.html',
  providers: [MessageService],
  styleUrls: ['./networking-group-buisness-list.component.css']
})
export class NetworkingGroupBuisnessListComponent implements OnInit {
  message = 'Hello!';
  constructor(
    private _httpClient: HttpClient,
    private _funcService:WrapperFuncService,
    public _networkingService : NetworkingService,
    private messageService: MessageService,
    public _shaerdData : SharedDataService,
    private route: ActivatedRoute  ) { }


//  @Output() IndexTabChange = new EventEmitter<number>();
  cols: any[];
  numBuisness :number;
  idGroup;
  // GroupBusiness :NetworkingGroupBusinessVM= {} as NetworkingGroupBusinessVM;
  businessArry : NetworkingGroupBusinessVM[] =[];
  clonedBuisness: { [s: string]: NetworkingGroupBusinessVM; } = {};
  roles = [];
  numberOfCurrentPage = 1;
  numberOfRowsInOnePage = 5;
  totalnumberOfPages;


  ngOnInit(): void {
    //  מילוי מערך התפקידים
    this.roles = [{label: 'מנהלת', value: 'מנהלת'},{label: 'חברה', value: 'חברה'}]

  }

  getGroupWithAllBuisness(IdGroup){
    this.idGroup=IdGroup;
    this._networkingService.getGroupById(IdGroup).subscribe(res=>{
      this._shaerdData.businessListForGroup.next(res);
      if (res) this.setPagingNumber(res);
      this.businessArry=res; 
      this.numBuisness =  this.businessArry.length;
    })
  }
 
  setPagingNumber(businessArry, numberOfCurrentPage = 1) {
    this.numberOfCurrentPage = numberOfCurrentPage;
    let tempNumPages = Math.floor(businessArry.length / this.numberOfRowsInOnePage);
    // let tempNumPages = Math.floor(this.serverPagingObj.TotalRows / this.numberOfCardsInOnePage);
    if (Math.floor(businessArry.length / tempNumPages) != this.numberOfRowsInOnePage)
        //אם אורך הרשימה אינו בכפולות של מספר השורות בעמוד
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
updateBusinessForGroup(groupBusiness :NetworkingGroupBusinessVM)
{
  console.log("bbb",groupBusiness);
  this._funcService.openNetworkingDetailsGroupForm(groupBusiness).subscribe((res :NetworkingGroupBusinessVM) =>{
    if(res){
      console.log("update",res);
      groupBusiness.GroupId = res.GroupId;
      groupBusiness.buisnessName = res.buisnessName;
      groupBusiness.Role = res.Role; 
  }
  });
}

  addBuisnessToGroup(){

    this._funcService.openNetworkingDetailsGroupForm(this.idGroup).subscribe(res=>{
    if(res)
      {
        console.log("מה שחוזר מהוספת חברה",res);
        this.businessArry.push(res);
        this._shaerdData.businessListForGroup.next(this.businessArry)
        this.numBuisness =  this.businessArry.length;
      }
    })    
 }

  // onRowEditInit(product: NetworkingGroupBusinessVM) {
  //   this.clonedBuisness[product.Id] = {...product};
  // }

// onRowEditSave(product: NetworkingGroupBusinessVM) {
//     if (product.Role != this.clonedBuisness[product.Id].Role) {
//         delete this.clonedBuisness[product.Id];
//         this._networkingService.updateBuisnessFromGroup(product).subscribe(res=>{
//           if(res)
//             this.messageService.add({severity:'success', summary: 'Success', detail:'העדכון נשמר בהצלחה'});
//         })
//     }
//     else {
//         this.messageService.add({severity:'error', summary: 'Error', detail:'לא שינית תפקיד'});
//     }
// }

  //  onRowEditCancel(product: NetworkingGroupBusinessVM, index: number) {
  //      this.businessArry[index] = this.clonedBuisness[product.Id];
  //      delete this.clonedBuisness[product.Id];
  //  }
  exportExcel() {
    import('xlsx').then((xlsx) => {
        const worksheet = xlsx.utils.json_to_sheet(this.businessArry);
        const workbook = { Sheets: { data: worksheet }, SheetNames: ['data'] };
        const excelBuffer: any = xlsx.write(workbook, {
            bookType: 'xlsx',
            type: 'array',
        });
        this.saveAsExcelFile(excelBuffer, 'tutorials');
    });
}

saveAsExcelFile(buffer: any, fileName: string): void {
    let EXCEL_TYPE =
'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8';
    let EXCEL_EXTENSION = '.xlsx';
    const data: Blob = new Blob([buffer], {
        type: EXCEL_TYPE,
    });
    FileSaver.saveAs(
        data,
        fileName + '_export_' + new Date().getTime() + EXCEL_EXTENSION
    );
}


  deleteBuisnessFromGroup(product: NetworkingGroupBusinessVM){
    console.log("buisness" ,product); 
    Swal.fire({
      title: 'האם את בטוחה שאת רוצה למחוק את העסק מהקבוצה??',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'כן, למחוק',
      cancelButtonText: 'לא, סליחה טעות'
    }).then((result) => {
      if (result.value) {   
      this._networkingService.deleteBuisnessFromGroup(product.Id).subscribe(res=>{
        console.log(res);
        this.businessArry = this.businessArry.filter(c=>c.Id!=product.Id);
        this._shaerdData.businessListForGroup.next(this.businessArry)
        this.numBuisness =  this.businessArry.length;
      });
      } 
    });
  }
}
