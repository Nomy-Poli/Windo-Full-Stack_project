import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import Swal from 'sweetalert2';
import { AccountService } from '../services/confirmEmailAccount.service';
import { WrapperFuncService } from '../services/wrapper-func.service';
import { WrapperSearchService } from '../services/wrapper-search.service';

@Component({
  selector: 'app-confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrls: ['./confirm-email.component.css']
})
export class ConfirmEmailComponent implements OnInit, OnDestroy {

  _isSucceededSubject: BehaviorSubject<string> = new BehaviorSubject(null);
  isSucceeded$ = this._isSucceededSubject.asObservable()

  constructor(public _confirmEmailService: AccountService
    , private route: ActivatedRoute
    , private _wrapperSearchService: WrapperSearchService
    , public _wrapperFuncService: WrapperFuncService
    , private router: Router) {
    console.log('ActivatedRoute - Constructor');
    this.route.queryParams.subscribe(params => {
      this.confirmEmail(params['UserId'], params['Code']);
    });
  }
 

  ngOnInit(): void {
    this._wrapperSearchService.HomePage$.next(false);
  }

  confirmEmail(UserId: string, Code: string) {
    this._confirmEmailService.confirmEmail(UserId, Code).subscribe(res => {
      if (res)
        this._isSucceededSubject.next(res);
    });
    let timerInterval;
    Swal.fire({
      title: 'אנו מעבירים אותך לדף הכניסה',
      html:
        'זה יקח לנו <b></b> שניות .',
      timer: 10000,
      allowOutsideClick: false,
      onBeforeOpen: () => {
        Swal.showLoading();
        timerInterval = setInterval(() => {
          Swal.getContent().querySelector(
            'b'
          ).textContent = String(
            (Swal.getTimerLeft() / 1000).toFixed(0)
          );
        }, 100);
      },
      onClose: () => {
        clearInterval(timerInterval);
      }
    }).then((r) => {
      Swal.stopTimer();
      this._wrapperFuncService.openLoginDialog();
    });
  }
  ngOnDestroy(): void {
    this._isSucceededSubject.unsubscribe();
  }
  // sendToLogin(){
  //     let timerInterval;
  //     Swal.fire({
  //         title: 'אנו נפנה אותך לדף ההרשמה',
  //         timer: 10000,
  //         allowOutsideClick: false,
  //         onBeforeOpen: () => {
  //             Swal.showLoading();
  //             timerInterval = setInterval(() => {
  //                 Swal.getContent().querySelector(
  //                     'b'
  //                 ).textContent = String(
  //                     (Swal.getTimerLeft() / 1000).toFixed(0)
  //                 );
  //             }, 100);
  //         },
  //         onClose: () => {
  //             clearInterval(timerInterval);
  //         }
  //     }).then((r) => {
  //         Swal.stopTimer();
  //         this.openLoginDialog();
  //     });
  //   }
}
