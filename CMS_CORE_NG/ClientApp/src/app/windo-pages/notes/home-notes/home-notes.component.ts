import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { BoardForCardVM, NoteSearchParameters, NoteService, NoteVM } from 'src/app/services/Note.service';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';

@Component({
  selector: 'app-home-notes',
  templateUrl: './home-notes.component.html',
  styleUrls: ['./home-notes.component.scss']
})
export class HomeNotesComponent implements OnInit {

  constructor(private _noteService: NoteService,
    public _wrapperSearchService: WrapperSearchService,
    public _funcService: WrapperFuncService,
    public _acct: AccountService,) { }

  ngOnInit(): void {
    this.getLatestNotes();
    this.getTemechNote();

  }
  latestNotesByBoards: NoteVM[] = [];
  latestTemechNote: NoteVM[] = [];
  getTemechNote() {
    let searchParameters: NoteSearchParameters = { BoardId: 3, Latest: 4 };
    this._noteService.getBoardsWithNotes(searchParameters).subscribe(res => {
      console.log(res);
      this.latestTemechNote = res;
    });
  }
   getLatestNotes() {
    let searchParameters: NoteSearchParameters = {};
    console.log("buisnesssIs?", this._acct.currentBusiness);
      this._acct.currentBusiness.subscribe(async (business) => {
        if (business != null) {
          searchParameters = { BusinessId: business.id, Latest: 12 };
        }
        else {
          searchParameters = { Latest: 12 };
        }
        if (this._acct.isManager)
          searchParameters = { IsManager: true, Latest: 12 }
        console.log("searchParameters",searchParameters);
        
        await this._noteService.getBoardsWithNotes(searchParameters).subscribe(res => {
          console.log(res);
          this.latestNotesByBoards = res;
        });
      });
  }
  createNote(board: BoardForCardVM) {
    this._funcService.openCreateNote(board.Id).subscribe(res => {
      if (res && res.Id) {
        board.Notes.push(res);
      }
    });
  }
}
