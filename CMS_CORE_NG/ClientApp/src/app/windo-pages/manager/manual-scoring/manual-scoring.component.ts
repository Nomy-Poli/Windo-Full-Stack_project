import {  ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DynamicDialogConfig } from 'primeng/dynamicdialog';
import { BuisnessService } from 'src/app/services/Buisness.service';
import { MultipleActions, ScoringService } from 'src/app/services/Scoring.service';
import { SharedDataService } from 'src/app/services/shared-data.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-manual-scoring',
  templateUrl: './manual-scoring.component.html',
  styleUrls: ['./manual-scoring.component.css']
})
export class ManualScoringComponent implements OnInit {

  constructor(
    private  buisnessService: BuisnessService ,
    private _scoringService : ScoringService,
    public WrapperSearchSer: WrapperSearchService ,
    public config: DynamicDialogConfig,
    public _shaerdData : SharedDataService,
    private cdref: ChangeDetectorRef


    
  ) { }
  formManualScore : FormGroup;
  businessList = [];
  selectedBuisness = [] ;
  operitioList = [] ;
  actionsList = [];
  selectedActionsList=[];
  dropdownBusinessesSettings = {
    singleSelection: false,
    idField: 'id',
    textField: 'buisnessName',
    searchPlaceholderText: 'התחילי להקליד את שם העסק',
    noDataAvailablePlaceholderText: 'לא נמצאו תוצאות',
    enableCheckAll: true,
    allowSearchFilter: true,
    closeDropDownOnSelection: false,
    selectAllText: 'Select All',
  }
  dropdownActonsSettings = {
    singleSelection: false,
    idField: 'id',
    textField: 'name',
    searchPlaceholderText: 'התחילי להקליד את שם הפעולה',
    noDataAvailablePlaceholderText: 'לא נמצאו תוצאות',
    enableCheckAll: true,
    allowSearchFilter: true,
    closeDropDownOnSelection: false,
    selectAllText: 'Select All',
  }
  get getBusiness() {
    return this.businessList.reduce((acc, curr) => {
      acc[curr.id] = curr;

      return acc;
    }, {});
  }
  get getActions() {
    return this._shaerdData.listOperition$.value.reduce((acc, curr) => {      
      acc[curr.Id] = curr;
      return acc;
    }, {});
  }

  ngOnInit(): void {
     this.formManualScore = new FormGroup({
     ListOfBuisness: new FormControl(null, Validators.required),
     ListOfActions : new FormControl(null, Validators.required)
    });
    this.getBusinesList();
    this.dropdownBusinessesSettings.singleSelection = false;
    this.getActionList();
  }
  getActionList()
  {
    this._shaerdData.listOperition$.subscribe(res=>{
          this.actionsList = res;
          console.log("this.actionsList",this.actionsList);
     });
     
  }
  ngAfterContentChecked() {
    this.cdref.detectChanges();
}
  getBusinesList() {
    this.buisnessService.getBusinessNamesPictures().subscribe(res => {
      this.businessList = res;//
      console.log("businessList",this.businessList);

    });
  }
  onSelectBusiness(event) {
    this.selectedBuisness.push(event.id)
    console.log("cdc",this.selectedBuisness);
  }
  OnBuisnessDeSelect(item: any){
    this.selectedBuisness = this.selectedBuisness.filter((el) => el !== item.id);
    console.log("after filter",this.selectedActionsList);

  }
  onSelectActions(event)
  {
    this.selectedActionsList.push(event.id)
  }
  OnActionDeSelect(item: any) {
    this.selectedActionsList = this.selectedActionsList.filter((el) => el !== item.id);
    console.log("after filter",this.selectedActionsList);
  }
  onSelectAllActions(event){
    console.log("event",event);
    event.forEach(element => {
      this.selectedActionsList.push(element.id)
    });    
  }
  onDeSelectAllBuisness(item :any){
     this.selectedBuisness = [] ;
  }
  onDeSelectAllActions(item :any){
     this.selectedActionsList =[] ;
  }
  onSelectAllBuisness(event){
    console.log("event",event);
    event.forEach(element => {
      this.selectedBuisness.push(element.id)
    }); 
  }
  submit(){
    if (this.formManualScore.valid){
      let multiple: MultipleActions = {
        buisnessId : this.selectedBuisness,
        scoringActionId : this.selectedActionsList
      }
      console.log("multiple",multiple);
      
       this._scoringService.addMultipleActions(multiple).subscribe(res=>{
        if(res){
          Swal.fire({
            icon: 'success',
            title: 'ההוספה בוצעה בהצלחה !',
            confirmButtonText:'יופי, תודה!'
          }) 
        }
      } );
    }
  }
}
