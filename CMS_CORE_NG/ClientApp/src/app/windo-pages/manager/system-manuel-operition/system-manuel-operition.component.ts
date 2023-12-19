import { Component, OnInit } from '@angular/core';
import { ScoringService, ScroingOperationVM } from 'src/app/services/Scoring.service';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';
import { SharedDataService } from 'src/app/services/shared-data.service';
import { BehaviorSubject, Observable } from 'rxjs';

@Component({
  selector: 'app-system-manuel-operition',
  templateUrl: './system-manuel-operition.component.html',
  styleUrls: ['./system-manuel-operition.component.css']
})
export class SystemManuelOperitionComponent implements OnInit {

  constructor(
    private _scroingService: ScoringService,
    private _funcService:WrapperFuncService,
    public _shaerdData : SharedDataService,
  ) { }
  scoringOperition:ScroingOperationVM[] = [];
  listWithActionName =[] ;

  // ordersListAfterFilter:OrderServiceVM[] = [];

 ngOnInit(): void {
   this.getScoringOperition();
 }
 getScoringOperition() {
   this._scroingService.getScroingOperation(1).subscribe(res=>{
     this.scoringOperition = res;
     this.scoringOperition.forEach(element => {
      this.listWithActionName.push({id:element.Id, name: element.ScoringAction.ActionName, count: element.Count});
      })
     this._shaerdData.listOperition$.next(this.listWithActionName);
   });
  

 }

 editOperition(operition){  
  this._funcService.openSystemManuelOperitionForm(operition).subscribe((res: ScroingOperationVM)=>{
    operition = res;
    this._shaerdData.listOperition$.next(this.scoringOperition);
    this.scoringOperition.forEach(element => {
      this.listWithActionName.push({id:element.Id, name: element.ScoringAction.ActionName, count: element.Count});
      })
     this._shaerdData.listOperition$.next(this.listWithActionName);
  });
}
// addOperition(){
//   this._funcService.openSystemOperitionForm().subscribe(res=>{
//     console.log(res);
//     this.scoringOperition.push(res);
//   })
// }
}
