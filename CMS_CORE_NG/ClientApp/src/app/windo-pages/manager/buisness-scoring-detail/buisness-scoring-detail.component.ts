import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { BusinessForScoringVM, BusinessScoringsDetailVM, ScoringService } from 'src/app/services/Scoring.service';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';

@Component({
  selector: 'app-buisness-scoring-detail',
  templateUrl: './buisness-scoring-detail.component.html',
  styleUrls: ['./buisness-scoring-detail.component.css']
})
export class BuisnessScoringDetailComponent implements OnInit {
  BuisnessId:number;
  numberOfCurrentPage = 1;
  numberOfCardsInOnePage = 5;
  totalnumberOfPages;
  constructor(
    private _scoringService: ScoringService,
    public _funcService: WrapperFuncService,
  ) { }
  @Output() closed = new EventEmitter();
  @Input() scoring: BusinessScoringsDetailVM;
  listOfScoringDetails :BusinessScoringsDetailVM[] = [];
  ngOnInit(): void {
    console.log("BuisnessID",this.BuisnessId);
    this.getListOfDetails();
  }
  getListOfDetails(){
    this._scoringService.getBusinessScoringsDetailById(this.BuisnessId).subscribe((res =>{
        this.listOfScoringDetails =res;
        this.setPagingNumber(res);
        console.log("i am Detailbuisness",this.listOfScoringDetails);
      }));
  }
  setPagingNumber(list) {
    this.numberOfCurrentPage = 1;
    let tempNumPages = Math.floor(list.length / this.numberOfCardsInOnePage);
    this.totalnumberOfPages =tempNumPages + 1 ;
    console.log("totalnumberOfPages",this.totalnumberOfPages);  
  }
  onPageChange(event) {
    if (this.numberOfCurrentPage <= this.totalnumberOfPages) {
      let tempStart = event.page * this.numberOfCardsInOnePage;

      let tempEnd;
      tempEnd = tempStart + this.numberOfCardsInOnePage;
      this.numberOfCurrentPage = event.page + 1;
    }
    else {

    }
  }
  close() {
    this.closed.emit(this.scoring);
    this._funcService.closeDialog();
}
}
