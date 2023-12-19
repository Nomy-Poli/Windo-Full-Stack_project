import { Component, OnInit } from '@angular/core';
import { BusinessForScoringVM, ScoringService, BusinessScoringsDetailVM, ScroingOperationVM } from 'src/app/services/Scoring.service';
import { PaginatorModule } from 'primeng/paginator';
import { BehaviorSubject } from 'rxjs';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-buisness-scoring-list',
  templateUrl: './buisness-scoring-list.component.html',
  styleUrls: ['./buisness-scoring-list.component.css']
})
export class BuisnessScoringListComponent implements OnInit {

  constructor(
    private _scoringService: ScoringService,
    private __funcService: WrapperFuncService
  ) { }
  numberOfCurrentPage = 1;
  numberOfCardsInOnePage = 8;
  totalnumberOfPages;
  listOfBuisness: BusinessForScoringVM[] = [];
  listOfScoringDetails: BusinessScoringsDetailVM[] = [];
  lenghtListOfBuisness: number;
  operition: ScroingOperationVM = {} as ScroingOperationVM;
  isLoading = false;

  ngOnInit(): void {
    this.getListOfBuisness();

  }
  getListOfBuisness() {
    this._scoringService.getListOfBuisnessesWithScoring().subscribe((res => {
      this.listOfBuisness = res;
      if (res)
        this.setPagingNumber(res);
      console.log("i am buisness ss", this.listOfBuisness);
    }));
  }

  setPagingNumber(list) {
    this.numberOfCurrentPage = 1;
    let tempNumPages = Math.floor(list.length / this.numberOfCardsInOnePage);
    this.totalnumberOfPages = tempNumPages + 1;
    console.log("cdcd", this.totalnumberOfPages);
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

  resetCount(buisnessId) {
    if (buisnessId) {
      debugger;
      this.isLoading = true;
      this._scoringService.resetCount(buisnessId).subscribe((res => {
        if (res) {
          this.isLoading = false;
          Swal.fire({
            icon: 'success',
            title: 'האיפוס בוצע בהצלחה!',
            confirmButtonText: 'יופי, תודה!'
          })
        }
      }));
    }
  }

  seeScore(buisnessId) {
    this.__funcService.OpenScoringDetail(buisnessId);
  }


}
