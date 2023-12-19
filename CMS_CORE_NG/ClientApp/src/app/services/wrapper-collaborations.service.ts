import { Injectable } from '@angular/core';
import { Observable, observable } from 'rxjs';
import { BarterDealVM, CollaborationTypeVM, JointProjectVM, PaidTransactionVM } from './Buisness.service';
import { CaseStudyForCardsVM, CaseStudyVM } from './Collaboration.service';

@Injectable({
  providedIn: 'root'
})
export class WrapperCollaborationsService {

  paidTrnsactionModel: PaidTransactionVM;
  paidTrnsactionPicture;

  barterDealModel: BarterDealVM;
  barterDealPictures;

  collaborationType: CollaborationTypeVM;

  JoinProjectModel: JointProjectVM;
  JoinProjectPicture;

  // case study
  fromTableCaseStudy: FromTableCaseStudyEnum;
  listOfAllCaseStudy: CaseStudyForCardsVM[] = [];
  listLastPicCS:CaseStudyForCardsVM[] = [];

  ifCSToDisplay$:Observable<number>;
  isAllCS$:Observable<boolean>;
  isCSToBu$:Observable<boolean>;
  constructor() { }
}

export enum FromTableCaseStudyEnum {
  PaidTransaction = 1,
  BarterDeal = 2,
  JointProject = 3
}