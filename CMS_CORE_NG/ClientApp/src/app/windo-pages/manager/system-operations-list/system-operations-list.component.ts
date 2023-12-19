import { Component, OnInit } from '@angular/core';
import { ScoringService, ScroingOperationVM,ScoringAction } from 'src/app/services/Scoring.service';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-system-operations-list',
  templateUrl: './system-operations-list.component.html',
  styleUrls: ['./system-operations-list.component.css']
})
export class SystemOperationsListComponent implements OnInit {

  constructor(
    private _scroingService: ScoringService,
    private _funcService:WrapperFuncService
  ) { }

  scoringOperition:ScroingOperationVM[] = [];
  // ordersListAfterFilter:OrderServiceVM[] = [];

 ngOnInit(): void {
   this.getScoringOperition();
 }
 getScoringOperition() {
   this._scroingService.getScroingOperation(2).subscribe(res=>{
     this.scoringOperition = res;
     console.log("i am esti",this.scoringOperition);
   });
 }
//  deleteOperition(operitionId){
//   Swal.fire({
//     title: 'האם את בטוחה שאת רוצה למחוק את הפעולה??',
//     icon: 'warning',
//     showCancelButton: true,
//     confirmButtonText: 'כן, למחוק',
//     cancelButtonText: 'לא, סליחה טעות'
//   }).then((result) => {
//     if (result.value) {
//       this.scoringOperition = this.scoringOperition.filter(x=>x.Id!= operitionId);
//       console.log("operition list:",this.scoringOperition);
      
//      this._scroingService.deleteScroingOperation(operitionId).subscribe(res=>{
//        console.log(res);
//        this.scoringOperition = this.scoringOperition.filter(x=>x.Id!= operitionId);
//       });
//     } 
//   });
  
//  }
 editOperition(operition){  
  this._funcService.openSystemOperitionForm(operition).subscribe((res: ScroingOperationVM)=>{
    operition = res;
  });
}
addOperition(){
  this._funcService.openSystemOperitionForm().subscribe(res=>{
    console.log(res);
    this.scoringOperition.push(res);
  })
}
}
