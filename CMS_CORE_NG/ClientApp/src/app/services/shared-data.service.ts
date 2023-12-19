import { Injectable } from '@angular/core';
import { BehaviorSubject, merge, Observable, Subject } from 'rxjs';
import { scan, shareReplay } from 'rxjs/operators';
import { NetworkingGroupVM ,NetworkingGroupBusinessVM} from './Networking.service';
import { ScroingOperationVM } from './Scoring.service';

@Injectable({
  providedIn: 'root'
})
export class SharedDataService {
  listOperition$ = new BehaviorSubject<ScroingOperationVM[]>(null);
 groupList$ = new BehaviorSubject<NetworkingGroupVM[]>(null);
 public groupListForUser = new BehaviorSubject<NetworkingGroupVM[]>(null);
 public $groupListForUser = this.groupListForUser.asObservable();// הפונקציה שנרשמים אליה
 public businessListForGroup = new BehaviorSubject<NetworkingGroupBusinessVM[]>(null);
 public $businessListForGroup = this.businessListForGroup.asObservable();// הפונקציה שנרשמים אליה
  ////////////////////////////////////////////////////////////
  // private groupListInitial = new BehaviorSubject<NetworkingGroupVM[]>([]);
  // groupListInitial$ = this.groupListInitial.asObservable();

  // SetggroupListInitial(groups:NetworkingGroupVM[]){
  //   this.groupListInitial.next(groups)
  // }
  // private grouptModifiedAction = new Subject<NetworkingGroupVM>( );
  // grouptModifiedAction$ = this.grouptModifiedAction.asObservable();
  // SetgrouptModifiedAction(group:NetworkingGroupVM){
  //   this.grouptModifiedAction.next(group)
  // }

  // groupList$: Observable<NetworkingGroupVM[]> = merge(
  //   this.groupListInitial$,
  //   this.grouptModifiedAction$
  // ).pipe(
  //   scan((groupList: NetworkingGroupVM[], group: NetworkingGroupVM) => this.modifyGroups(groupList, group)),
  //   shareReplay(1)
  // );

  constructor() {
   }
  //  modifyGroups(groupList: NetworkingGroupVM[], group: NetworkingGroupVM): NetworkingGroupVM[] {
  //   if (!groupList.find(g=>g.Id==group.Id)) {
  //     // Return a new array from the array of products + new product
    //   return [
    //     ...groupList,
    //     { ...group }
    //   ];
    // }
  //   else  {
      
  //     // Filter out the deleted product
  //     return groupList.filter(p => p.Id !== group.Id);
  //   }
    ////
    // if (product.status === StatusCode.Updated) {
    //   // Return a new array with the updated product replaced
    //   return products.map(p => p.id === product.id ?
    //     { ...product, status: StatusCode.Unchanged } : p);
    // }
  }


