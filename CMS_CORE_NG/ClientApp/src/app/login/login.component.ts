import { Component, OnDestroy, OnInit } from '@angular/core';
import {
  FormGroup,
  FormControl,
  FormBuilder,
  Validators
} from '@angular/forms';
import { BehaviorSubject } from 'rxjs';
import { AccountService } from '../services/account.service';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import Swal from 'sweetalert2';
import { AuthenticationService } from '../services/authentication.service';
import { SocialUser } from 'angularx-social-login';
import { ExternalAuthModel } from '../interfaces/externalAuthModel';
import { WrapperSearchService } from '../services/wrapper-search.service';
import { WrapperFuncService } from '../services/wrapper-func.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit, OnDestroy {
  imageUrl: string = '/assets/images/717856_64046121.jpg';
  insertForm: FormGroup;
  
  Username: FormControl;
  Password: FormControl;
  RememberMe: FormControl;
  showPassword: boolean = false;
  flag: boolean = false;

  // returnUrl: string;
  ErrorMessage: string;
  invalidLogin: boolean;
  forgetPasswordForm: FormGroup;
  Email: FormControl;

  LoginStatus$ = new BehaviorSubject<boolean>(null);
  /*כניסה עם google*/
  showError: boolean
  externalAuth: ExternalAuthModel = {
    idToken: '',
    provider: '',
    ComeFromLogin: false
  }
  constructor(
    private _acct: AccountService,
    
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    public toasterService: ToastrService,
    private _authService: AuthenticationService,
    public _wrapperSearchService: WrapperSearchService,
    public _wrapperFuncService: WrapperFuncService
  ) { }


  ngOnInit(): void {
    this._acct.globalStateChanged.subscribe((state) => {
      this._wrapperSearchService.LoginStatus$.next(state.loggedInStatus);
    });

    if (this._wrapperSearchService.LoginStatus$.getValue()) {
      this.router.navigate(['/']);
    }

    // Initialize Form Controls
    this.Username = new FormControl('', [Validators.required, Validators.pattern('[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$')]);
    this.Password = new FormControl('', [Validators.required]);
    this.RememberMe = new FormControl(false);

    // Initialize FormGroup using FormBuilder
    this.insertForm = this.fb.group({
      Username: this.Username,
      Password: this.Password,
      RememberMe: this.RememberMe,   
      // RememberMe: this.RememberMe
    });

    // get return url from route parameters or default to '/'
    this._acct.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';

    // Initialize Controls
    this.Email = new FormControl('', [Validators.required, Validators.email]);

    this.forgetPasswordForm = this.fb.group({
      Email: this.Email
    });
    this.rememberMe();
  }
  rememberMe(){
    let isRem = localStorage.getItem('rememberMe');
    if(isRem=='yes')
    {
      var a=
       this.insertForm.get('Username').setValue(localStorage.getItem("username"))  
       this.insertForm.get('Password').setValue(localStorage.getItem("password"))  
       console.log(this.insertForm.get('Username'))
    }
  }
  password() {
    this.showPassword = !this.showPassword;
  }
  // ShowPassword() {
  //   var x = document.getElementById("password");
  //   if (x.type === "password") {
  //     x.type = "text";
  //   } else {
  //     x.type = "password";
  //   }
  // }
  onSubmitforgetPasswordForm() {
    let userInfo = this.forgetPasswordForm.value;
    this._acct.sendForgotPasswordEmail(userInfo.Email).subscribe(
      (result) => {
        if ((result && result.message == 'Success')) {
          $('#forgotPassCard').html('');
          $('#forgotPassCard').append(
            "<div class='alert alert-success show'>" + '<strong>נשלח!</strong> אנא בדוק בדוא"ל שלך הוראות לאיפוס סיסמה.' + '</div>'
          );
        }
        else if ((result && result.message == 'Failed')) {
          $('#forgotPassCard').html('');
          $('#forgotPassCard').append(
            "<div class='alert alert-danger show'>" + '<strong> שגיאה! </strong>.' + '</div>'
          );
        }
        this._wrapperFuncService.closeDialog();
      },
      (error) => {
        this.toasterService.error('אירעה שגיאה בעיבוד בקשה זו.', '', { positionClass: 'toast-top-right' });
        this._wrapperFuncService.closeDialog();
      }
    );
  }

  onSubmit() {
    let userlogin = this.insertForm.value;
    this._acct.login(userlogin.Username, userlogin.Password, userlogin.RememberMe).subscribe(

      (result) => {
        this.invalidLogin = false;
        $('body').css({ 'background-image': 'none' });
        this._wrapperFuncService.closeDialog();
        this.router.navigateByUrl(this._acct.returnUrl);
      },
      (error) => {
        this.invalidLogin = true;
        this.ErrorMessage = error;
        if (error == undefined) {
          this.toasterService.warning('שם משתמש / סיסמה שגויים. אנא בדוק אישורים ונסה שוב', '', { positionClass: 'toast-top-right' });
          // window.location.reload();
          return false;
        }
        if (error.status == 500) {
          this.toasterService.info('הצוות שלנו עובד על מנת לתקן שגיאה זו. נסה שוב מאוחר יותר.', '', { positionClass: 'toast-top-right' });
        }
        if (error.status == 401) {
          if (error.error.loginError) {
            if (error.error.loginError == 'נדרש קוד אימות') {
              localStorage.setItem('codeExpiry', error.error.expiry);
              localStorage.setItem('twoFactorToken', error.error.twoFactorToken);
              localStorage.setItem('isSessionActive', '1');
              localStorage.setItem('user_id', error.error.userId);
              this.router.navigate(['/send-code']);
              return false;
            }
            if (error.error.loginError == 'חשבון נעול') {
              Swal.fire({
                title: 'חשבונך נעול, אנא צור קשר עם התמיכה.',
                icon: 'error',
                showClass: {
                  popup: 'animate__animated animate__fadeInDown'
                },
                hideClass: {
                  popup: 'animate__animated animate__fadeOutUp'
                }
              });
              return false;
            }
            if (error.error.loginError == 'דוא"ל לא אושר') {
              Swal.fire({
                title: 'הדוא"ל שלך לא מאומת. אנא אשר דוא"ל',
                icon: 'error',
                showClass: {
                  popup: 'animate__animated animate__fadeInDown'
                },
                hideClass: {
                  popup: 'animate__animated animate__fadeOutUp'
                }
              });
              return false;
            } else {
              this.toasterService.error(error.error.loginError, '', { positionClass: 'toast-top-right' });
              return false;
            }
          } else {
            this.toasterService.error('שם משתמש / סיסמה שגויים. אנא בדוק אישורים ונסה שוב', '', {
              positionClass: 'toast-top-right',
              timeOut: 7000
            });
            return false;
          }
        } else {
          this.toasterService.warning('שם משתמש / סיסמה שגויים. אנא בדוק אישורים ונסה שוב', '', { positionClass: 'toast-top-right' });
          return false;
        }
      }
    );
  }
  setbackgroundImage() {
    $('body').css({
      'background-image': '#cccccc' /*'url(' + this.imageUrl + ')',*/,
      'background-repeat': 'no-repeat',
      'background-size': '100% 100%'
    });
  }
  /*login with google*/
  public externalLogin = () => {
    this.showError = false;
    this._authService.signInWithGoogle()
      .then(res => {
        const user: SocialUser = { ...res };
        console.log(user);
        this.externalAuth = {
          provider: user.provider,
          idToken: user.idToken,
          ComeFromLogin: true
        }
        this.validateExternalAuth(this.externalAuth);
      }, error => console.log(error));
  }
  private validateExternalAuth(externalAuth: ExternalAuthModel) {
    this._authService.externalLogin(externalAuth)
      .subscribe(res => {
        if (res.Message == 'Exists') {
          Swal.fire({
            title:
              'שם משתמש זה כבר קיים',
            icon: 'error',
            showClass: {
              popup:
                'אנא בדוק שוב'
            }
          });
        }
        //אם קיים אבל לא אימת מייל
        else if (res.Message == 'Success') {
          let timerInterval;
          Swal.fire({
            title: 'רשמנו אותך, ההרשמה בוצעה בהצלחה!',
            html:
              'אנא בדוק אם כתובת הדוא"ל שלך כוללת אימות.<br>ההרשמה מפנה שוב לדף הכניסה <b></b>.',
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
          }).then((r) => {
            Swal.fire({
              title: 'לא נשלח לך מייל?',
              text: 'בדוק אם נשלח ואם לא לחץ על שליחה',
              icon: 'info',
              showCancelButton: true,
              confirmButtonText: 'שליחה',
              cancelButtonText: 'ביטול'
            }).then((result) => {
              if (result.value) {
                this.externalAuth.ComeFromLogin = false;
                this.validateExternalAuth(this.externalAuth)
              } else if (result.dismiss === Swal.DismissReason.cancel) {
                Swal.fire(
                  'ביטלת את שליחת המייל פעם נוספת',
                )
              }
            })
          });
        }
        else if (res.Message == 'ConfirmEmailAgain') {
          let timerInterval;
          Swal.fire({
            title: 'שליחת המייל שוב בוצעה בהצלחה!',
            html:
              'אנא בדוק אם כתובת הדוא"ל שלך כוללת אימות.<br>ההרשמה מפנה שוב לדף הכניסה <b></b>.',
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
          }).then((r) => {
            Swal.fire({
              title: 'לא נשלח לך מייל?',
              text: 'בדוק אם נשלח ואם לא לחץ על שליחה',
              icon: 'info',
              showCancelButton: true,
              confirmButtonText: 'שליחה',
              cancelButtonText: 'ביטול'
            }).then((result) => {
              if (result.value) {
                this.validateExternalAuth(this.externalAuth)

              } else if (result.dismiss === Swal.DismissReason.cancel) {
                Swal.fire(
                  'ביטלת את שליחת המייל פעם נוספת',
                )
              }
            })
          });
        }
        else if (res.Message == 'NotConfirmEmail') {
          Swal.fire({
            title:
              'משתמש יקר, נרשמת אך לא אימתת את כתובת המייל שלך, אנא פנה לתיבת המייל ואשר ',
            icon: 'info',
            showClass: {
              popup:
                'אנא בדוק שוב'
            }
          });
        }
        else if (res && res.Token) {
          this.invalidLogin = false;
          $('body').css({ 'background-image': 'none' });
          this._wrapperFuncService.closeDialog();
          this.router.navigateByUrl(this._acct.returnUrl);
        }
        else {
          Swal.fire({
            title:
              'אנו מצטערים, אך אירע שגיאה מסויימת, בדוק שהזנת פרטים תקינים',
            icon: 'error',
            showClass: {
              popup:
                'אנא בדוק שוב'
            }
          });
        }
      //  window.location.reload();
      },
        error => {
          this.invalidLogin = true;
          this.ErrorMessage = error;
          if (error.status == 500) {
            this.toasterService.info(
              'הצוות שלנו עובד על מנת לתקן שגיאה זו. נסה שוב מאוחר יותר.',
              '',
              { positionClass: 'toast-top-right' }
            );
          }
          if (error.status == 401) {
            if (error.error.loginError) {
              if (
                error.error.loginError == 'נדרש קוד אימות'
              ) {
                localStorage.setItem(
                  'codeExpiry',
                  error.error.expiry
                );
                localStorage.setItem(
                  'twoFactorToken',
                  error.error.twoFactorToken
                );
                localStorage.setItem('isSessionActive', '1');
                localStorage.setItem(
                  'user_id',
                  error.error.userId
                );
                this.router.navigate(['/send-code']);
                return false;
              }
              if (error.error.loginError == 'חשבון נעול') {
                Swal.fire({
                  title:
                    'חשבונך נעול, אנא צור קשר עם התמיכה.',
                  icon: 'error',
                  showClass: {
                    popup:
                      'animate__animated animate__fadeInDown'
                  },
                  hideClass: {
                    popup:
                      'animate__animated animate__fadeOutUp'
                  }
                });
                return false;
              }
              if (
                error.error.loginError == 'דוא"ל לא אושר'
              ) {
                Swal.fire({
                  title:
                    'הדוא"ל שלך לא מאומת. אנא אשר דוא"ל',
                  icon: 'error',
                  showClass: {
                    popup:
                      'animate__animated animate__fadeInDown'
                  },
                  hideClass: {
                    popup:
                      'animate__animated animate__fadeOutUp'
                  }
                });
                return false;
              } else {
                this.toasterService.error(
                  error.error.loginError,
                  '',
                  { positionClass: 'toast-top-right' }
                );
                return false;
              }
            } else {
              this.toasterService.error(
                'שם משתמש / סיסמה שגויים. אנא בדוק אישורים ונסה שוב',
                '',
                {
                  positionClass: 'toast-top-right',
                  timeOut: 3000
                }
              );
              return false;
            }
          } else {
            this.toasterService.warning(
              'אירעה שגיאה בעיבוד בקשה זו - וודא שנרשמת עם שם המשתמש וסיסמה שהכנסת.',
              '',
              { positionClass: 'toast-top-right' }
            );
            return false;
          }
        }
      );
  }
  SendEmailAgain(){
   this._wrapperFuncService.openDialogSendEmailAgain();
  }
  ngOnDestroy(): void {
    this.LoginStatus$.unsubscribe();
  }
}
