import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PrimeNGConfig } from 'primeng/api';
import { BehaviorSubject } from 'rxjs';
import { async } from 'rxjs/internal/scheduler/async';
import { AccountService } from 'src/app/services/account.service';
import { BreadcrumbService } from 'src/app/services/breadcrumb.service';
import { BuisnessService } from 'src/app/services/Buisness.service';
import { NetworkingGroupVM, NetworkingService } from 'src/app/services/Networking.service';
import { BoardVM, NoteSearchParameters, NoteService, NoteVM, ReplayNoteMessageVM } from 'src/app/services/Note.service';
import { SharedDataService } from 'src/app/services/shared-data.service';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';
import Swal from 'sweetalert2';
import { NotesListComponent } from '../notes-list/notes-list.component';

@Component({
    selector: 'app-board',
    templateUrl: './board.component.html',
    styleUrls: ['./board.component.scss']
})

export class BoardComponent implements OnInit, OnDestroy {
    constructor(
        public breadcrumbService: BreadcrumbService,
        private _noteService: NoteService,
        public _acct: AccountService,
        public _activatedRoute: ActivatedRoute,
        public _funcService: WrapperFuncService,
        private _wrapperSearchService: WrapperSearchService,
        public _router: Router,
        private _buisnessService: BuisnessService,
        public _networkingService: NetworkingService,
        public _shaerdData: SharedDataService,
        public _wrapperFuncService: WrapperFuncService,
        private _primengConfig: PrimeNGConfig
    ) {
        this._acct.globalStateChanged.subscribe((state) => {
            this._wrapperSearchService.LoginStatus$.next(state.loggedInStatus);
        });
        this._wrapperSearchService.Username$ = this._acct.currentUserName;
        this._wrapperSearchService.HomePage$.next(false);
        this.breadcrumbService.setItem([
            { label: 'דף הבית', routerLink: ['/'] },
            { label: 'לוח מודעות', routerLink: ['/all-notes/' + true] }
        ]);
        _primengConfig.ripple = true;
    }

    @ViewChild('notesListC') notesListComp: NotesListComponent;

    isLoading = false;
    isDisplayForm = false;
    boardList: BoardVM[] = [];
    noteList: NoteVM[] = [];
    groupList: NetworkingGroupVM[] = []
    businessList = [];
    noteList$ = new BehaviorSubject<NoteVM[]>(null);
    noteListAfterSearch$ = new BehaviorSubject<NoteVM[]>(null);
    area: 'personal' | 'manager' | 'groups' | 'temech';
    boardId;
    isGroups: boolean;
    searchText = '';
    //#region paging
    searchForm: FormGroup;
    numberOfCurrentPage = 1;

    numberOfCardsInOnePage = 16;
    totalnumberOfPages;
    openNote;
    firstOne = true;
    validateRangeFlag = false;


    //#endregion
    get getGroupDiscription() {
        return this._shaerdData.groupList$.value.reduce((acc, curr) => {
            acc[curr.Id] = curr;
            return acc;
        }, {});
    }
    ngOnInit(): void {
        this._activatedRoute.queryParams.subscribe((params) => {
            if (this.firstOne) {
                if (params['boardId'] && params['boardId'] != 'null') {
                    this.getNotesByBoard(params['boardId']);
                } else if (params['my'] == 'true') {
                    this.getMyNote();
                } else {
                    this.getAllNotes();
                }
                if (params['note']) {
                    this.openNote = params['note'];
                }
                this.firstOne = false;
            }
        });

        this.noteList$.subscribe((res) => {
            if (res) this.setPagingNumber(res);
        });
        this.getBoardList();
        this.getBusinesList();
        this.getAllGroups();

        this.getCategoriesAndSubCategory();
        this.searchForm = new FormGroup({
            searchText: new FormControl(),
            Header: new FormControl(),
            BusinessIds: new FormControl(),
            CategorySubCategoryId: new FormControl(),
            CreationDate: new FormControl('', []),
            GroupIdS: new FormControl()
        });


    }
    rangeDates: Date[];
    validateRange() {
        var formValue = this.searchForm.value;
        if (formValue.CreationDate[0] != null && formValue.CreationDate[1] == null) {
            this.validateRangeFlag = true;
        }

        else {
            this.validateRangeFlag = false;
        }
    }

    getBoardList() {

        this._noteService.getBoards().subscribe((res) => {
            this._wrapperSearchService.boardList = res;
            this.boardList = res;
            console.log("boards",res);
            
        });
    }
    setPagingNumber(list, numberOfCurrentPage = 1) {
        this.numberOfCurrentPage = numberOfCurrentPage;
        let tempNumPages = Math.floor(list.length / this.numberOfCardsInOnePage);
        // let tempNumPages = Math.floor(this.serverPagingObj.TotalRows / this.numberOfCardsInOnePage);
        if (Math.floor(list.length / tempNumPages) != this.numberOfCardsInOnePage)
            //אם אורך הרשימה אינו בכפולות של מספר הכרטיסים בעמוד
            this.totalnumberOfPages = tempNumPages + 1;
        else this.totalnumberOfPages = tempNumPages;
        //tempNumPages < list.length / this.numberOfCardsInOnePage ? tempNumPages + 1 : list.length;
    }
  getAllNotes() {
        this.boardId = null;
        this.area = null;
        this.isLoading = true;
        let searchParameters: NoteSearchParameters = {};
        this._acct.currentBusiness.subscribe((business) => {
            if (business) {
                if (this._acct.isManager) {
                    searchParameters = { IsManager: true };
                }
                else {
                    searchParameters = { BusinessId: business.id };
                }
            }
            this._noteService.getBoardsWithNotes(searchParameters).subscribe((res) => {
                // this._wrapperSearchService._notesListSubject.next(res.Data);
                console.log("getBoardsWithNotes", res);

                this.putNotes(res);
            });
        });
    }

    getNotesByBoard(boardId) {
        if (this.boardId != boardId) {
            this.boardId = boardId;
            this.area = null;
            this.isLoading = true;
            var searchObj: NoteSearchParameters = { BoardId: boardId };
            this._noteService.getBoardsWithNotes(searchObj).subscribe((res) => {
                console.log('getNotesByBoard', res);
                this.putNotes(res);
            });
        }
        else {
            this.getAllNotes();
        }
    }

    getMyGroupNote() {
        if (this.area != 'groups') {
            this.area = 'groups';
            this.boardId = null;
            this._acct.currentBusiness.subscribe((business) => {
                if (business) {
                    let searchParameters: NoteSearchParameters = { ForGroups: true, BusinessId: business.id };
                    this.isLoading = true;
                    this._noteService.getBoardsWithNotes(searchParameters).subscribe((res) => {
                        this.putNotes(res);
                    });
                }
            });
        } else {
            this.getAllNotes();
        }
    }
   
    getMyNote() {
        if (this.area != 'personal') {
            this.area = 'personal';
            this.boardId = null;
            this._acct.currentBusiness.subscribe((business) => {
                if (business) {
                    let searchParameters: NoteSearchParameters = { BusinessId: business.id, getMyNote: true };
                    this.isLoading = true;
                    this._noteService.getBoardsWithNotes(searchParameters).subscribe((res) => {
                        this.putNotes(res);
                    });
                }
            });
        } else {
            this.getAllNotes();
        }
    }
    

    managerArea() {
        if (this.area != 'manager') {
            if (this.area == 'personal' || this.boardId != null) {
                this.getAllNotes();
            }
            this.area = 'manager';
        } else {
            this.getAllNotes();
        }
    }

    putNotes(notes: NoteVM[]) {
        this.searchForm.reset();
        console.log(notes);
        this.noteList = [];
        for (let i = 0; i < notes.length; i++) {
            const note = notes[i];
            if (i == 3 || i == 9) {
                this.noteList.push({ Id: 0, BusinessId: 0 });
            }
            this.noteList.push(note);
        }
        console.log(this.noteList);

        this.isLoading = false;
        this.noteList$.next(this.noteList);
        console.log("this.noteList$", this.noteList$);

        if (this.openNote) {
            let note = notes.find((x) => x.Id == this.openNote);
            if (note) this.notesListComp.openNote(note);
            this.openNote = null;
        }
    }
    get getCategorySubCategory() {
        return this.SubCategories.reduce((acc, curr) => {
            acc[curr.id] = curr;
            return acc;
        }, {});
    }
    getCategoriesAndSubCategory() {
        this._wrapperSearchService._listOfSuggestionSubject.subscribe((res) => {
            this.SubCategories = res;
            this.SubCategoriesAfterSearch = res;
        });
    }

    searchCategory(event): any[] {
        if (event == '') {
            this.SubCategoriesAfterSearch = this.SubCategories;
        } else {
            this.SubCategoriesAfterSearch = this.SubCategories.filter((x) => x.categoryName.includes(event) || x.subCategoryName.includes(event));
        }
        return this.SubCategoriesAfterSearch;
    }
    resetFillterList() {
        this.searchForm.reset();
        this.noteList$.next(this.noteList);
        this.validateRangeFlag = false;

    }
    //#region search
    toggleSearch(event) {
        this.isDisplayForm = !this.isDisplayForm;
        event.stopPropagation();
    }
    closeSearch() {
        this.isDisplayForm = false;
    }
    searchNotes() {
        let list: NoteVM[] = [];
        var formValue = this.searchForm.value;
        console.log("GroupIdS", formValue.GroupIdS);

        this.noteList.forEach((note) => {
            if (note.Id) {
                if (
                    !formValue.searchText ||
                    note.Header.includes(formValue.searchText) ||
                    note.Text.includes(formValue.searchText) ||
                    (note.CategorySubCategory &&
                        (note.CategorySubCategory.categoryName.includes(formValue.searchText) ||
                            note.CategorySubCategory.subCategoryName.includes(formValue.searchText))) ||
                    (note.Business && note.Business.buisnessName.includes(formValue.searchText)) ||
                    // חיפוש לפי קבוצת נטורקינג 
                    (note.NetworkingGroup && note.NetworkingGroup.GroupName.includes(formValue.searchText))

                )
                    if (!formValue.Header || note.Header.includes(formValue.Header)) {
                        if (!formValue.CategorySubCategoryId || note.CategorySubCategoryId == formValue.CategorySubCategoryId[0].id) {
                            if (!formValue.GroupIdS || note.GroupId == formValue.GroupIdS[0].Id) {
                                if (this.area == 'personal' || !formValue.BusinessIds || formValue.BusinessIds[0].id == note.BusinessId) {
                                    if (
                                        !formValue.CreationDate ||
                                        (formValue.CreationDate[0] <= new Date(note.CreatetionDate) &&
                                            formValue.CreationDate[1] >= new Date(note.CreatetionDate))
                                    ) {
                                        list.push(note);
                                    }
                                    else {
                                        console.log("vhgh");


                                    }


                                }
                            }

                        }
                    }

            }
        });
        this.noteList$.next(list);
        this.isDisplayForm = false;
    }
    dropdownCategoriessSettings = {
        singleSelection: true,
        idField: 'id',
        textField: 'subCategoryName',
        searchPlaceholderText: 'התחילי להקליד תחום או תת תחום שירות',
        noDataAvailablePlaceholderText: 'לא נמצאו תוצאות',
        enableCheckAll: false,
        allowSearchFilter: true,
        closeDropDownOnSelection: true
    };
    dropdownBusinessesSettings = {
        singleSelection: true,
        idField: 'id',
        textField: 'buisnessName',
        searchPlaceholderText: 'התחילי להקליד את שם העסק',
        noDataAvailablePlaceholderText: 'לא נמצאו תוצאות',
        enableCheckAll: false,
        allowSearchFilter: true,
        closeDropDownOnSelection: false,
        // itemsShowLimit: 3,
        limitSelection: 10
    };
    dropdownGroupsSettings = {
        singleSelection: true,
        idField: 'Id',
        textField: 'GroupName',
        searchPlaceholderText: 'התחילי להקליד את שם הקבוצה',
        noDataAvailablePlaceholderText: 'לא נמצאו תוצאות',
        enableCheckAll: false,
        allowSearchFilter: true,
        closeDropDownOnSelection: true
    }
    SubCategories = [];
    SubCategoriesAfterSearch = [];
    get getBusiness() {
        return this.businessList.reduce((acc, curr) => {
            acc[curr.id] = curr;
            return acc;
        }, {});
    }
    getBusinesList() {
        if (!this._wrapperSearchService.shrunkenBuisnessList) {
            this._buisnessService.getBusinessNamesPictUser().subscribe((res) => {
                console.log(res);
                this._wrapperSearchService.shrunkenBuisnessList = res;
                this.businessList = res;
            });
        } else {
            this.businessList = this._wrapperSearchService.shrunkenBuisnessList;
        }
    }
    getAllGroups() {
        this._shaerdData.groupList$.subscribe(res => {
            this.groupList = res;
            console.log("group list", res);
        })
    }

    //#endregion
    openCreateNote(board?: BoardVM) {


        console.log("this.board", this.boardId);

        this._funcService.openCreateNote(board ? board.Id : this.boardId, this.area == 'groups' ? true : false).subscribe((res) => {
            if (res) {
                console.log("last date",res.LastUpdateDate);   
                this.noteList.push(res);
                this.noteList.sort((a, b) => {
                    if (a.LastUpdateDate > b.LastUpdateDate) return -1;
                    else if (a.LastUpdateDate < b.LastUpdateDate) return 1;
                    return 0;
                });
                console.log("this.noteList",this.noteList);
                
                this.noteList$.next(this.noteList);
                // let list =  this._wrapperSearchService._notesListSubject.getValue();
                // list.find(x=>x.Id == board.Id).Notes.push(res);
                // this._wrapperSearchService._notesListSubject.next(list);
            }
        });
    }
    onPageChange(event) {
        if (this.numberOfCurrentPage <= this.totalnumberOfPages) {
            let tempStart = event.page * this.numberOfCardsInOnePage;
            let tempEnd;
            tempEnd = tempStart + this.numberOfCardsInOnePage;
            this.numberOfCurrentPage = event.page + 1;
            window.scrollTo({ top: 0, behavior: 'smooth' });
        }
    }
    replay() { }
    createCollaboration(note) {
        if (this._wrapperSearchService.LoginStatus$.value && this._acct.currentBusiness.value) {
            this._funcService.openNewMassage(note.BusinessId, 'בתגובה למודעה ' + note.Header).subscribe((res) => {
                console.log(res);
                let replay: ReplayNoteMessageVM = {
                    Id: 0,
                    BusinessId: this._acct.currentBusiness.value.id,
                    NoteId: note.Id,
                    MessageId: res,
                    CreationDate: new Date()
                };
                this._noteService.createReplayToNoteMessage(replay).subscribe((res) => {
                    console.log(res);
                });
            });
        }
    }

    updateNote(note: NoteVM) {
        let boardId = note.Boards[0]?.Id;
        console.log("what is a board", boardId);
        if (boardId == undefined) {
            this.isGroups = true;
        }
        this._funcService.openCreateNote(boardId, this.isGroups, note).subscribe((noteRes: NoteVM) => {
            if (noteRes) {
                note.Header = noteRes.Header;
                (note.Text = noteRes.Text), (note.CategorySubCategoryId = noteRes.CategorySubCategoryId);
                note.CategorySubCategory = noteRes.CategorySubCategory;
                note.NetworkingGroup = noteRes.NetworkingGroup;
            }
        });
    }
    deleteNote(note: NoteVM) {
        Swal.fire({
            title: 'האם את בטוחה שאת רוצה למחוק את המודעה?',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonText: 'לא, סליחה טעות',
            confirmButtonText: 'כן. אני רוצה למחוק'
        }).then((val) => {
            if (val && val.isConfirmed) {
                let reason = this.area == 'personal' ? 1 : 2;
                this._noteService.deleteNote(note.Id, reason).subscribe((res) => {
                    console.log(res);
                    this.noteList = this.noteList.filter((x) => x.Id != note.Id);
                    this.noteList$.next(this.noteList);
                    this._wrapperFuncService.closeDialog();

                });
            }
        });
    }
    ngOnDestroy(): void {
        this.noteList$.unsubscribe();
    }
    emailsAboatNews(emailTo) {
        this._noteService.emailsAboatNews(emailTo).subscribe((res) => {
            console.log("ddd", res);
        });
    }

}
