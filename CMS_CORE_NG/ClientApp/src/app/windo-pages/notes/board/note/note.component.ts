import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from 'src/app/services/account.service';
import Swal from 'sweetalert2';
import { BreadcrumbService } from 'src/app/services/breadcrumb.service';
import { NoteReplayVM, NoteService, NoteWithReplayVM, ReplayNoteMessageVM } from 'src/app/services/Note.service';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';
import { ScoringService } from 'src/app/services/Scoring.service';
import { MessagesModule } from 'primeng/messages';
import { Message } from 'primeng/api';

@Component({
    selector: 'app-note',
    templateUrl: './note.component.html',
    styleUrls: ['./note.component.scss']
})
export class NoteComponent implements OnInit, OnDestroy {
    constructor(
        public breadcrumbService: BreadcrumbService,
        private _noteService: NoteService,
        public _acct: AccountService,
        public _activatedRoute: ActivatedRoute,
        public _funcService: WrapperFuncService,
        private _wrapperSearchService: WrapperSearchService,
        public _router: Router,
        private _scoringService: ScoringService
    ) {
        // this._acct.globalStateChanged.subscribe((state) => {
        //   this._wrapperSearchService.LoginStatus$.next(state.loggedInStatus);
        // });
        // this._wrapperSearchService.Username$ = this._acct.currentUserName;
        // this._wrapperSearchService.HomePage$.next(false);
        // this.breadcrumbService.setItem([
        //   { label: 'דף הבית', routerLink: ['/'] },
        //   { label: 'כל המודעות', routerLink: ['/all-notes/' + false] },
        //   { label: 'פרטי מודעה', routerLink: ['/note/' + this._activatedRoute.snapshot.params['id']] }
        // ]);
    }
    ngOnDestroy(): void {
        if (this._router.url.includes('notes')) this._router.navigate(['/notes'], { queryParams: {} });
    }
    messages: Message[] | undefined;

    @Input() noteId;
    @Input() note: NoteWithReplayVM;
    @Output() closed = new EventEmitter();
    isReplayOpen: 'update' | 'new' = null;
    sucsessReplay: boolean = false;
    replayInEdit = null;
    form: FormGroup;
    isCollapsed = true;
    sentToServer = false;
    ngOnInit(): void {
        console.log("this.note", this.note);
        this.messages = [{ severity: 'success', summary: '', detail: ' התגובה נשלחה בהצלחה !' }];
        // window.scroll(0,0);
        // this.noteId = this._activatedRoute.snapshot.params['id'];
        // this._noteService.getNoteById(this.noteId).subscribe(res=>{
        //   this.note = res;
        // });
        //  זימון הפונקציה שוסיפה ניקוד על פעולה
        this._acct.currentBusiness.subscribe(res => {
            if (res != null) {
                this._scoringService.getScoreToBusiness(10, res.id).subscribe(res => {
                    console.log("res", res);
                });
            }
        });
        this._noteService.getNoteReplays(this.noteId).subscribe((res) => {
            this.note.ReplayToNotes = res;
        });
        this.form = new FormGroup({
            Text: new FormControl('', [Validators.required, Validators.maxLength(300)])
        });
    }

    close() {
        this.closed.emit(this.note);
        this._funcService.closeDialog();
    }
    openReplay() {
        this.isReplayOpen = 'new';
    }
    ToggleAndChangeTheIcon(collapse) {
        collapse.toggle();
    }
    replay() {
        if (!this.sentToServer) {
            this.sentToServer = true;

            if (this.replayInEdit) {
                this.replayInEdit.Text = this.form.value['Text'];
                this._noteService.updateReplayNote(this.replayInEdit).subscribe((res) => {
                    this.note.ReplayToNotes.find(x => x.Id == this.replayInEdit.Id).Text = this.replayInEdit.Text;
                    this.form.get('Text').setValue('');
                    this.sucsessReplay =true;
                    this.isReplayOpen = null;
                    this.replayInEdit = null;
                    this.sentToServer = false;



                })
            }
            else {
                let replay: NoteReplayVM = {
                    NoteId: this.note.Id,
                    BusinessId: this._acct.currentBusiness.value.id,
                    Id: 0,
                    Text: this.form.value['Text']
                };
                this._noteService

                    .createReplayToNote(replay, this.note.BusinessId, this.note.Business.userId, this._acct.currentBusiness.value.buisnessName)
                    .subscribe((res) => {
                        this.sentToServer = false;
                        console.log(res);
                        replay.Id = res;
                        this.sucsessReplay = true;
                        replay.Business = this._acct.currentBusiness.value;
                        this.note.ReplayToNotes.push(replay);
                        console.log("repAY", this.sucsessReplay);
                        this.isReplayOpen = null;
                        this.form.get('Text').setValue('');
                        setTimeout(() => {
                            this.sucsessReplay = false;
                          }, 5000);

                    });
            }
        }
    }
    deleteReplayNote(replay: NoteReplayVM) {
        Swal.fire({
            title: 'האם את בטוחה שאת רוצה למחוק את התגובה?',
            showCancelButton: true,
            confirmButtonColor: '#3085D6',
            cancelButtonText: 'לא, סליחה טעות',
            confirmButtonText: 'כן. אני רוצה למחוק'
        }).then((val) => {
            if (val && val.isConfirmed) {
                this._noteService.deleteReplayNote(replay.Id, replay.BusinessId).subscribe((res) => {
                    // console.log("this is my res",res);
                    this.note.ReplayToNotes = this.note.ReplayToNotes.filter(x => x.Id != replay.Id);
                    this.closed.emit(this.note);
                });
            }
        });

        // this.deleteNoteClicked.emit(note);
        // this.closeOptions(note);
    }
    updateReplayNote(replay: NoteReplayVM) {
        this.replayInEdit = replay;
        this.isReplayOpen = 'update';
        this.form.get('Text').setValue(replay.Text);
    }



    //   replayToOwner(){
    //     this.close();
    //     this._funcService.openNewMassage(this.note.BusinessId+'', "בתגובה למודעה " + this.note.Header).subscribe(res => {
    //       console.log(res);
    //       let replay: ReplayNoteMessageVM = {
    //         Id: 0,
    //         BusinessId: this._acct.currentBusiness.value.id,
    //         NoteId: this.note.Id,
    //         MessageId: res,
    //         CreationDate: new Date()
    //       };
    //       this._noteService.createReplayToNoteMessage(replay).subscribe(res => {
    //         replay.Id = res;
    //         this.note.ReplayNoteMessages.push(replay);
    //       });
    //     });
    //   }
}
