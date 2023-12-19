import { Component, EventEmitter, Input, OnInit, Output, SimpleChanges } from '@angular/core';
import { CheckboxRequiredValidator, FormControl, FormGroup, Validators } from '@angular/forms';
import { PrimeNGConfig } from 'primeng/api';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { AccountService } from 'src/app/services/account.service';
import { NetworkingGroupVM, NetworkingService } from 'src/app/services/Networking.service';
import { NoteService, NoteVM } from 'src/app/services/Note.service';
import { SharedDataService } from 'src/app/services/shared-data.service';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';

@Component({
  selector: 'app-create-note',
  templateUrl: './create-note.component.html',
  styleUrls: ['./create-note.component.scss']
})
export class CreateNoteComponent implements OnInit {

  constructor(private _noteService: NoteService,
    public _wrapperSearchService:WrapperSearchService,
    public _wrapperFuncService:WrapperFuncService,
    public _acct: AccountService,
    public _networkingService : NetworkingService,
    public _shaerdData : SharedDataService,
  //  private ref: DynamicDialogRef, public config: DynamicDialogConfig,
   private _primengConfig: PrimeNGConfig) { 
    _primengConfig.ripple = true;
   }
   groupList : NetworkingGroupVM []= []

   discriptionGroups = [];
    noteForm:FormGroup;
    @Input() noteToUpdate: NoteVM;
    @Input() boardId: number;
    @Input() isGroups: boolean;
    @Input() isTemech: boolean;
    @Output() closed = new EventEmitter();
    SubCategories = [];
    SubCategoriesAfterSearch = [];
    requiredField: boolean = false;
    lastBoard;
    // clickGroups = false;
  isLoading = false;
  isSuccess = false;
  
  get getCategorySubCategory() {
    return this.SubCategories.reduce((acc, curr) => {
      acc[curr.id] = curr;      
      return acc;
    }, {});
  }
  get getGroupDiscription() {
    return this._shaerdData.groupListForUser.value.reduce((acc, curr) => {
      acc[curr.Id] = curr;
      return acc;
    }, {});
  }
  get GroupToShere(){return this.noteForm.get('GroupToShere')}
  ngOnInit(): void {
    console.log("isGroups",this.isGroups);
    
    this.noteForm = new FormGroup({
      // categoryId: new FormControl(''),
      CategorySubCategoryId: new FormControl(),
      GroupToShere : new FormControl(null),
      Header:new FormControl('',[Validators.required, Validators.minLength(3),Validators.maxLength(120)]),
      Text:new FormControl('',[Validators.required, Validators.minLength(3),Validators.maxLength(500)]),
    });
    if(this.noteToUpdate){
      console.log("noteToUpdate---y",this.noteToUpdate);
      this.noteForm.patchValue({
        CategorySubCategoryId:   this.noteToUpdate.CategorySubCategory? 
          [{id: this.noteToUpdate.CategorySubCategoryId,subCategoryName:this.noteToUpdate.CategorySubCategory.subCategoryName}]: null,// this.config.data.note.CategorySubCategoryId,
        GroupToShere : this.noteToUpdate.NetworkingGroup?
          [{Id:this.noteToUpdate.Id,GroupName:this.noteToUpdate.NetworkingGroup.GroupName}]:null,
        Header: this.noteToUpdate.Header,// this.config.data.note.Header,
        Text: this.noteToUpdate.Text // this.config.data.note.Text,
      });
    }
    if(this.isGroups)
    {
      this.CheckValidator();
    }
    this.getCategoriesAndSubCategory();
    this.getAllGroups();
    this.setStatus();
  }

  dropdownCategoriessSettings = {
    clearSearchFilter:true,
    singleSelection: true,
    idField: 'id',
    textField: 'subCategoryName',
    searchPlaceholderText:'התחילי להקליד תחום או תת תחום שירות',
    noDataAvailablePlaceholderText:'לא נמצאו תוצאות',
    enableCheckAll:false,
    allowSearchFilter: true,
    closeDropDownOnSelection:true
  }

  dropdownGroupsSettings = {
    singleSelection: true,
    idField: 'Id',
    textField: 'GroupName',
    searchPlaceholderText:'התחילי להקליד את שם הקבוצה',
    noDataAvailablePlaceholderText:'לא נמצאו תוצאות',
    enableCheckAll:false,
    allowSearchFilter: true,
    closeDropDownOnSelection:true
  }
  setStatus() {
    (this.noteForm.get('GroupToShere').value) ? this.requiredField = true : 
    this.requiredField = false;
  }
  getAllGroups()
  {
    this._shaerdData.groupListForUser.subscribe(res=>{
      this.groupList = res;
      console.log("group list",res);      
    })      
  }
  getCategoriesAndSubCategory() {
    this._wrapperSearchService._listOfSuggestionSubject.subscribe(res => {
      this.SubCategories = res;
      console.log("this.SubCategories", this.SubCategories);
      this.SubCategoriesAfterSearch = res
    });
  }

  searchCategory(event):any[] {
    if (event == '') {
      this.SubCategoriesAfterSearch = this.SubCategories;
    }
    else {
      this.SubCategoriesAfterSearch = this.SubCategories.filter(x =>
        x.categoryName.includes(event) || x.subCategoryName.includes(event));
    }
    return this.SubCategoriesAfterSearch;

  }
  OnDeSelect(item: any) {
    this.noteForm.get('GroupToShere').reset();
    this.noteForm.get('GroupToShere').updateValueAndValidity();
    console.log("after filter",this.GroupToShere);
  }
  activ(id){
    console.log('isgroup3',this.isGroups)
    this.isGroups = false;
    if(this.noteToUpdate)
       this.lastBoard= this.boardId;
    this.boardId = id;
    this.noteForm.get('GroupToShere').clearValidators();
    this.noteForm.get('GroupToShere').updateValueAndValidity()
  }
  // CheckValidator(){
  //   this.isGroups = true;
  //   this.boardId = 0;
  //   this.noteForm.get('GroupToShere').setValidators(Validators.required) 
  //   this.noteForm.get('GroupToShere').updateValueAndValidity()
  // }
  CheckValidator() {
    this.isGroups = true;
    this.boardId = 0;
    console.log('isgroup2',this.isGroups);

    if (this.isGroups) {
      this.noteForm.get('GroupToShere').clearValidators();
    } else {
      this.noteForm.get('GroupToShere').setValidators([Validators.required]);
    }
  
    this.noteForm.get('GroupToShere').updateValueAndValidity();
  }
  createNote(){
   

    console.log(this.noteForm);
    let note:NoteVM;
    this.isSuccess = true;
    if(this.noteToUpdate){
      this.noteToUpdate.Text = this.noteForm.get('Text').value;
      this.noteToUpdate.Header = this.noteForm.get('Header').value,
      this.noteToUpdate.CategorySubCategoryId = this.noteForm.get('CategorySubCategoryId').value? this.noteForm.get('CategorySubCategoryId').value[0]?.id: null,
      this.noteToUpdate.GroupId = this.noteForm.get('GroupToShere').value ? this.noteForm.get('GroupToShere').value[0]?.Id :null,
       this.isLoading = true;
      this._noteService.updateNote(this.noteToUpdate).subscribe(res=>{
        this.isLoading = false;
        console.log("loading=" , this.isLoading);
        if(this.noteToUpdate.CategorySubCategoryId)
          this.noteToUpdate.CategorySubCategory = this.getCategorySubCategory[this.noteToUpdate.CategorySubCategoryId];
        if(this.noteToUpdate.GroupId)
          this.noteToUpdate.NetworkingGroup = this.getGroupDiscription[this.noteToUpdate.GroupId];
          this.closed.emit(this.noteToUpdate);
        setTimeout(() => {
          this._wrapperFuncService.closeDialog();
          // this.isLoading = true;

          // this.ref.close(this.noteToUpdate);
        }, 3000);

        console.log(res);
      
      });
     

    }
    else{
       note = {
        Id: 0 ,
        BusinessId: this._acct.currentBusiness.value.id,
        CreatetionDate: new Date(),
        IsActive: true,
        Text: this.noteForm.get('Text').value,
        Header: this.noteForm.get('Header').value,
        CategorySubCategoryId: this.noteForm.get('CategorySubCategoryId').value? this.noteForm.get('CategorySubCategoryId').value[0]?.id: null,
        GroupId : this.noteForm.get('GroupToShere').value ? this.noteForm.get('GroupToShere').value[0]?.Id :null,
        NumberOfViews:0
      } 
      this.isLoading = true;
      console.log("loading=" , this.isLoading);
      //let boardId = this.config.data?.boardId? this.config.data?.boardId: 1;
      this._noteService.createNote(note,this.boardId).subscribe(noteRes=>{
      
        if(noteRes.GroupId)
        noteRes.NetworkingGroup = this.getGroupDiscription[noteRes.GroupId];

        this.isLoading = false;
        console.log("loading=" , this.isLoading);
                this.closed.emit(noteRes);
        setTimeout(() => {
          this._wrapperFuncService.closeDialog();
          // this.isLoading = true;

          // this.ref.close(note);

        }, 3000);
      
      });
      // console.log(this.isLoading);

    }
    
    
    
  }
  // ngOnChanges(changes: SimpleChanges) {
  //   if (changes['isGroups'] && changes['isGroups'].currentValue !== changes['isGroups'].previousValue) {
  //     this.CheckValidator();
  //   }
  // }

  close(){
    this._wrapperFuncService.closeDialog();
  }
}

