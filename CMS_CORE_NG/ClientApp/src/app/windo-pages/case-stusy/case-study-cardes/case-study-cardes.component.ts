import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import { AccountService } from 'src/app/services/account.service';
import { CaseStudyForCardsVM, CollaborationsService } from 'src/app/services/Collaboration.service';
import { ScoringService } from 'src/app/services/Scoring.service';
import { WrapperCollaborationsService } from 'src/app/services/wrapper-collaborations.service';

@Component({
  selector: 'app-case-study-cardes',
  templateUrl: './case-study-cardes.component.html',
  styleUrls: ['./case-study-cardes.component.scss']
})
export class CaseStudyCardesComponent implements OnInit, OnDestroy {
  // perPage: number = 8;
  @Input() deployment: string;  
  @Input() perPage: number;  
  start=1;
  end=1;
  numberOfCurrentPage = 1;
  numberOfCardsInOnePage = 8;
  totalnumberOfPages;
  /////
  isLoading: boolean = true;
  flagNext=true;
  flagPrev=false;
  isAllCS
  listCSToDisplay:CaseStudyForCardsVM[]=[];
  listCSTo$ = new BehaviorSubject<CaseStudyForCardsVM[]>(null);
  BuisnessId:any
  ListCSOfBuis:CaseStudyForCardsVM[]=[];
  constructor(private _collaborationsService: CollaborationsService
    , public _wrapperCollaborationsService: WrapperCollaborationsService, 
    public CollaborationsSer:CollaborationsService,
    public WrapperCollaborationsSer:WrapperCollaborationsService,
    public _acct: AccountService,
    private _scoringService: ScoringService,
    private router1:ActivatedRoute) {this.BuisnessId= this.router1.snapshot.paramMap.get('id'); }
 

  ngOnInit(): void {
    this.WrapperCollaborationsSer.isAllCS$ = new Observable<boolean>(observer => {
      setInterval(() => observer.next(false));
     });
     this.WrapperCollaborationsSer.isCSToBu$ = new Observable<boolean>(observer => {
      setInterval(() => observer.next(false));
     });
    this.end=this.perPage;
    
    if(this.BuisnessId!=null){ 
      this.GetCSByEmail(this.BuisnessId) 
      
      }
    else{
       this.GetAllCaseStudy();
      
     } 
     this.listCSTo$.subscribe(res=>{
       if(res){
        this.setPagingNumber(res)
       }
     })
     console.log("listCS " , this.listCSTo$);
    
  }




  setPagingNumber(list) {
    this.numberOfCurrentPage = 1;
    let tempNumPages = Math.floor(list.length / this.numberOfCardsInOnePage);
    // let tempNumPages = Math.floor(this.serverPagingObj.TotalRows / this.numberOfCardsInOnePage);
    this.totalnumberOfPages =tempNumPages + 1 ;
      // tempNumPages < list.length / this.numberOfCardsInOnePage ? tempNumPages + 1 : list.length;
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
  GetCSByEmail(idBuissness:number)
  {
      this.CollaborationsSer.getCSByBuissinesID(idBuissness).subscribe(res=>{
          if(res!=undefined)
          {
            this.listCSToDisplay=res
            this.listCSTo$.next(this.listCSToDisplay);
            this.WrapperCollaborationsSer.ifCSToDisplay$ = new Observable<number>(observer => {
              setInterval(() => observer.next(this.listCSToDisplay.length));
            });
            this.WrapperCollaborationsSer.isCSToBu$ = new Observable<boolean>(observer => {
              setInterval(() => observer.next(true));
             });
            // if(this.listCSToDisplay.length==0)
            // this.WrapperCollaborationsSer.isAllCS$ = new Observable<boolean>(observer => {
            //   setInterval(() => observer.next(false));
            //  });
            //  else
            // this.WrapperCollaborationsSer.isAllCS$ = new Observable<boolean>(observer => {
            //   setInterval(() => observer.next(true));
            //  });
          }  
      })
  }
  GetAllCaseStudy() {//משיכת רשימת כל הקייס סטאדיים

    this._collaborationsService.getAllCaseStudy().subscribe(res => {
      if (res != null){
      this.listCSToDisplay=res;
      this.listCSTo$.next(this.listCSToDisplay);
      this.isLoading=false;
      this._wrapperCollaborationsService.listOfAllCaseStudy = res;     
      this.WrapperCollaborationsSer.isAllCS$ = new Observable<boolean>(observer => {
        setInterval(() => observer.next(true));
       });
      
      //  this.WrapperCollaborationsSer.isAllCS$ = new Observable<boolean>(observer => {
      //   setInterval(() => observer.next(true));
      //  });
      // this._wrapperCollaborationsService.listOfAllCaseStudy = res;
        }
      // console.log(this._wrapperCollaborationsService.listOfAllCaseStudy);
    })
  }
  getRandomColor() {//הגרלת צבע במקרה שאין תמונה לקייס סטאדי
    var color = Math.floor(0x1000000 * Math.random()).toString(16);
    var rcolor  = '#' + ('000000' + color).slice(-6);
    $('#logo_img').css("background-color", rcolor);    
  }

  next(){
    if(this.end<this.listCSToDisplay.length)
    {
      this.flagNext=true
      this.flagPrev=true
      this.start=this.end;
      this.end=parseInt(this.start+'')+parseInt(this.perPage+'');}
    else{this.flagNext=false; this.flagPrev=true}
  }
  prev(){
    if(this.start>0){
      this.flagPrev=true
      this.flagNext=true
      this.end=this.start;
      this.start=parseInt(this.start+'')-parseInt(this.perPage+'');} 
    else{this.flagPrev=false;this.flagNext=true;}
  }

  ngOnDestroy(): void {
    this.listCSTo$.unsubscribe();
  }
}
