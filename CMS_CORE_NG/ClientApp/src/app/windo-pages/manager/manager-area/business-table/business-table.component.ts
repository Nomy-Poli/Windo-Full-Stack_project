import { Component, OnInit } from '@angular/core';
import { BreadcrumbService } from 'src/app/services/breadcrumb.service';
import { BuisnessService, BuisnessVm } from 'src/app/services/Buisness.service';
// import { UserModel, UserService } from 'src/app/services/user.service';
import { saveAs } from 'file-saver';



@Component({
  selector: 'app-business-table',
  templateUrl: './business-table.component.html',
  styleUrls: ['./business-table.component.css']
})
export class BusinessTableComponent implements OnInit {  
  public list: Array<BuisnessVm> = [];
  newList;
  // private listUsers:Array<UserModel> =[];

  constructor(
    public breadcrumbService: BreadcrumbService,
    public _buisnessService: BuisnessService,
    // public userService: UserService

  ) { 
    breadcrumbService.setItem([
      { label: 'דף הבית', routerLink: ['/']/*, icon: 'pi pi-home'*/ },
      { label: 'אזור מנהל', routerLink: ['/manager'] },
      { label: 'טבלת עסקים', routerLink: ['.'] }
    ]);

  }

  
  tabIndex =0;
  IndexTabChange = 0;
  groupBuisnessActive = false;
  ngOnInit(): void {
    console.log("tabindex",this.IndexTabChange);
    this._buisnessService.getListOfBuisnesses().subscribe((res)=>{
      this.list = res;
      console.log(this.list[0],"vvvvvvvv");
      
    });

  // this.userService.getUsers().subscribe((res)=>{
  //   this.listUsers =res;
  // })

  }


  exportExcel() {
    import('xlsx').then((xlsx) => {
        const worksheet = xlsx.utils.json_to_sheet(this.list);
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
    saveAs(
        data,
        fileName + '_export_' + new Date().getTime() + EXCEL_EXTENSION
    );
}

}
