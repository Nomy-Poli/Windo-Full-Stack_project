import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/services/account.service';
import { NetworkingGroupVM, NetworkingService } from 'src/app/services/Networking.service';
import { NoteVM, NoteWithReplayVM } from 'src/app/services/Note.service';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';


@Component({
    selector: 'app-notes-list',
    templateUrl: './notes-list.component.html',
    styleUrls: ['./notes-list.component.scss']
})
export class NotesListComponent implements OnInit {
    constructor(
        public _acct: AccountService,
        private _funcService: WrapperFuncService,
        private _router: Router,
        public _networkingService: NetworkingService
    ) { }

    @Input() notesList: any[];
    @Input() area: 'personal' | 'manager' | 'groups'|'temech';
    @Input() deployment: 'row' | 'list' = 'list';

    @Output() newNoteClicked = new EventEmitter();
    @Output() newMessageClicked = new EventEmitter();
    @Output() updateNoteClicked = new EventEmitter();
    @Output() deleteNoteClicked = new EventEmitter();
    groupList: NetworkingGroupVM[] = [];
    openCreateNote;
    ngOnInit(): void {
        console.log("area", this.area);

        console.log("note list", this.notesList);
        // this.getAllGroups();
    }

    openOptions(note) {
        note['displayOptions'] = true;
    }
    toggleOption(note, event) {
        // if (event.target.id != "open-note" && note['displayOptions']) {
        //   this.closeOptions(note);
        // }
        if (note.Id != 0) {
            if (this.deployment == 'row') {
                this._router.navigate(['/notes'], { queryParams: { note: note.Id } });
                return;
            }
            if (this.area != 'personal') {
                this._router.navigate(['/notes'], { queryParams: { note: note.Id } });

                this.openNote(note);
            } else {
                this.updateNote(note);
            }
        }
    }

    closeOptions(note) {
        note['displayOptions'] = false;
    }
    createCollaboration(note) {
        this.closeOptions(note);
        this.newMessageClicked.emit(note);
    }
    goToBusiness(note) {
        this.closeOptions(note);
        //this._router.navigate(['/business-view',note.Business.userId])
    }
    goToReport(note) {
        this.closeOptions(note);
    }
    updateNote(note: NoteVM) {
        this.closeOptions(note);
        this.updateNoteClicked.emit(note);
    }
    deleteNote(note: NoteVM) {
        this.deleteNoteClicked.emit(note);
        this.closeOptions(note);
    }

    openNote(note) {
        // if (!this._router.url.includes('note=')) {

        // }
        this._funcService.openNote(note).subscribe((note: NoteWithReplayVM) => {
            let n = this.notesList.find((x) => x.Id == note.Id);
            n.ReplayCount = note.ReplayToNotes.length;
        });
        // }
        // else{
        //   this._router.navigate(['/notes'], { queryParams: { note: note.Id } });
        // }
    }

    getBackgroungColor(note: NoteVM) {
        let color = '';
        switch (this.area) {

            case 'personal':
                color = '#33cead';
                break;
            case 'manager':
                color = '#e2e42b';
                break;
            case 'groups':
                color = '#fcd998';
                break
            default:
                if (note.Boards && note.Boards[0]) {
                    color = note.Boards[0].Color;
                } else {
                    color = '#fcd998';
                }
                break;
        }
        return { 'background-color': color };
    }

    sliceHeader(header: string) {
        if (header.length <= 20)
            return header;
        return header.slice(0, 20).slice(0, header.slice(0, 20).lastIndexOf(' ')) + '...';
    }
    replaceDate(LastUpdateDate:Date){

    }
}
