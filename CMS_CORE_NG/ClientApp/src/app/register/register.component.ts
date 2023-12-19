import { Component, OnInit } from '@angular/core';
import { CountryService } from '../services/country.service';
import { Observable } from 'rxjs';
import { Country } from '../interfaces/country';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { ValidatorService } from '../services/common/validator.service';
import { ToastrService } from 'ngx-toastr';
import Swal from 'sweetalert2';
import { AccountService } from '../services/account.service';
import { Router } from '@angular/router';
import { BreadcrumbService } from '../services/breadcrumb.service';
import { AuthenticationService } from '../services/authentication.service';
import { SocialUser } from 'angularx-social-login';
import { ExternalAuthModel } from '../interfaces/externalAuthModel';
import { WrapperFuncService } from '../services/wrapper-func.service';

declare let $: any;

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
    imageUrl: string = '/assets/images/717856_64046121.jpg';
    showPassword: boolean = false;
    country$: Observable<Country[]>;
    countries: Country[];
    registerForm: FormGroup;
    errorList: string[];
    modalMessage: string;
    modalTitle: string;
    isRegistrationInProcess: boolean = false;
    //    username: FormControl;
    email: FormControl;
    firstname: FormControl;
    lastname: FormControl;
    password: FormControl;
    phone: FormControl;
    terms: FormControl;
    flag: boolean = false;

    // confirmEmail: FormControl;
    // cpassword: FormControl;
    // country: FormControl;
    // gender: FormControl;
    dob: FormControl;

    /*כניסה עם google*/
    invalidLogin: boolean;
    showError: boolean;
    externalAuth: ExternalAuthModel = {
        idToken: '',
        provider: '',
        ComeFromLogin: false

    };
    showManual = false;
    isLoading = false;
    constructor(
        private countryservice: CountryService,
        private fb: FormBuilder,
        private validatorService: ValidatorService,
        public toastrservice: ToastrService,
        private acct: AccountService,
        private router: Router,
        private _authService: AuthenticationService,
        public breadcrumbService: BreadcrumbService,
        public _wrapperFuncService: WrapperFuncService) {
        breadcrumbService.setItem([
            { label: 'דף הבית', routerLink: ['/']/*, icon: 'pi pi-home'*/},
            { label: 'רישום ', routerLink: ['/register'] }
        ]);
    }

    ngOnInit(): void {
        // this.username = new FormControl(this.email);
        this.firstname = new FormControl('', [
            Validators.required,
            Validators.maxLength(10),
            Validators.minLength(2)
        ]);
        this.lastname = new FormControl('', [
            Validators.required,
            Validators.maxLength(10),
            Validators.minLength(2)
        ]);
        this.email = new FormControl('', [
            Validators.required,
            Validators.email
        ]);
        // this.confirmEmail = new FormControl('', [
        //     Validators.required,
        //     this.validatorService.MustMatch(this.email)
        // ]);
        this.password = new FormControl('', [
            Validators.required,
            Validators.maxLength(15),
            Validators.minLength(5)
        ]);
        // this.cpassword = new FormControl('', [
        //     Validators.required,
        //     this.validatorService.MustMatch(this.password)
        // ]);
        // this.country = new FormControl('', [Validators.required]);
        this.phone = new FormControl('', [
            Validators.required,
            Validators.pattern(
                '^\\(?([0-9]{3})\\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$'
            )
        ]);
        // this.gender = new FormControl('', [Validators.required]);
        // this.dob = new FormControl('', [Validators.required]);
        this.terms = new FormControl('', [Validators.required]);

        this.registerForm = this.fb.group({
            username: this.email,
            firstname: this.firstname,
            lastname: this.lastname,
            email: this.email,
            password: this.password,
            phone: this.phone,
            terms: this.terms
            // country: this.country,
            // gender: this.gender,
            // dob: this.dob,
            // confirmEmail: this.confirmEmail,
            // cpassword: this.cpassword,
        });

        // this.setbackgroundImage();
        // this.loadCountries();

        // $(() => {
        //     $('#datepicker').datepicker({
        //         changeMonth: true,
        //         changeYear: true,
        //         yearRange: '1920:2099',
        //         onSelect: (dateText) => {
        //             this.dob.setValue(dateText);
        //         }
        //     });
        // });
    }
    showInsertPassword() {
        this.showPassword = !this.showPassword;
    }
    onSubmit() {
        this.isRegistrationInProcess = true;
        let userDetails = this.registerForm.value;
        this.errorList = [];
        this.isLoading = true;
        this.acct
            .register(
                userDetails.username,
                userDetails.firstname,
                userDetails.lastname,
                userDetails.password,
                userDetails.email,
                // userDetails.country,
                userDetails.phone,
                // userDetails.gender,
                // userDetails.dob,
                userDetails.terms
            )
            .subscribe(
                (result) => {
                    if (result.message == 'ההרשמה הצליחה') {
                        this.isLoading = false;
                        let timerInterval;
                        Swal.fire({
                            title: 'שמחים שהצטרפת אלינו, וכבר רוצים להכיר את העסק שלך מקרוב!',
                            width: 800,
                            html:
                                'רגע לפני כן, חפשי בבקשה את הודעת האימות ששלחנו לכתובת האימייל שלך.<br>על מנת להשלים את התהליך הרישום יש ללחוץ על כפתור האימות המופיע בה.</br></br> מיד לאחר מכן תועברי למסך הכניסה לאתר.<br><strong>מחכים לפגוש אותך שם!</strong></br></br> אם אינך מוצאת את ההודעה בתיבת הדואר הנכנס, <strong>מומלץ לבדוק גם בספאם</strong> </br></br>האימייל ישלח בעוד:<b></b> ' ,
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
                            // this._wrapperFuncService.openLoginDialog();
                        });
                        this.isRegistrationInProcess = false;
                    }
                },
                error => {
                    if (error.status == 500 || error == 'undefined') {
                        this.isLoading = false;
                        this.toastrservice.info(
                            'אירעה שגיאה בעיבוד בקשה זו. בדוק פרטים או נסה שוב.',
                            '',
                            { positionClass: 'toast-top-right' }
                        );
                    }
                    if (error.error && error.error.Value) {
                        this.errorList = [];
                        for (let i = 0; i < error.error.value.length; i++) {
                            this.errorList.push(error.error.value[i]);
                        }
                        this.showModalError();
                    }
                    else if (error.Value) {
                        for (const key in error.Value) {
                                const element = error.Value[key];
                                switch (key) {
                                    case "DuplicateUserName":
                                        this.registerForm.get('username').setErrors({"DuplicateUserName":element})
                                        break;
                                        case "DuplicateEmail":
                                            this.registerForm.get('email').setErrors({"DuplicateEmail":element})
                                            break;
                                        case "PasswordRequiresNonAlphanumeric":
                                            this.registerForm.get('password').setErrors({"PasswordRequiresNonAlphanumeric":element})
                                            break;
                                        case "PasswordRequiresLower":
                                            this.registerForm.get('password').setErrors({"PasswordRequiresLower":element})
                                            break;
                                        case "PasswordRequiresUpper":
                                            this.registerForm.get('password').setErrors({"PasswordRequiresUpper":element})
                                            break;
                                        case "PasswordRequiresDigit":
                                            this.registerForm.get('password').setErrors({"PasswordRequiresDigit":element})
                                            break;
                                    default:
                                        this.showModalError();
                                        break;
                                }
                        }
                        // for (let i = 0; i < error.Value.length; i++) {
                        //     this.errorList.push(error.Value[i]);
                        // }
                        //this.showModalError();
                    }
                    this.isLoading = false;
                    this.isRegistrationInProcess = false;
                }
            );
    }

    showModalError() {
        this.modalTitle = 'שגיאת הרשמה';
        this.modalMessage = 'ההרשמה לא הצליחה';
        $('#errorModal').modal('show');
    }

    setbackgroundImage() {
        $('body').css({
            'max-height': '100%',
            'background-color': '#cccccc',/*    'background-image': 'url(' + this.imageUrl + ')',*/
            'background-size': '100% 100%',
            'background-repeat': 'no-repeat',
            //'background-size': 'cover'
        });
    }

    loadCountries() {
        this.country$ = this.countryservice.getCountries();
        this.country$.subscribe((countrylist) => {
            this.countries = countrylist;
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
                    ComeFromLogin: false
                }
                this.validateExternalAuth(this.externalAuth);
            }, error => console.log(error));
    }
    public validateExternalAuth(externalAuth: ExternalAuthModel) {
        this._authService.externalLogin(externalAuth)
            .subscribe(res => {
                localStorage.setItem("token", res.Token);
                this.invalidLogin = false;
                // $('body').css({ 'background-image': 'none' });
                if (res && res.Token) {
                    // this.invalidLogin = false;
                    $('body').css({ 'background-color': '#fff' });
                    this._wrapperFuncService.closeDialog();
                    this.router.navigateByUrl(this.acct.returnUrl);
                }
                else if (res.Message == 'Success') {
                    let timerInterval;
                    Swal.fire({
                        title: 'שמחים שהצטרפת אלינו, וכבר רוצים להכיר את העסק שלך מקרוב!',
                        html:
                        'רגע לפני כן, חפשי בבקשה את הודעת האימות ששלחנו לכתובת האימייל שלך.<br>על מנת להשלים את התהליך הרישום יש ללחוץ על כפתור האימות המופיע בה.</br></br> מיד לאחר מכן תועברי למסך הכניסה לאתר.<br><strong>מחכים לפגוש אותך שם!</strong></br></br> אם אינך מוצאת את ההודעה בתיבת הדואר הנכנס, <strong>מומלץ לבדוק גם בספאם</strong> </br></br>האימייל ישלח בעוד:<b></b> ' ,
                        timer: 12000,
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
                        // this._wrapperFuncService.openLoginDialog();
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
                    this.isRegistrationInProcess = false;
                }
                else if (res.Message == 'ConfirmEmailAgain') {
                    let timerInterval;
                    Swal.fire({
                        title: 'שליחת המייל שוב בוצעה בהצלחה!',
                        html:
                        'רגע לפני כן, חפשי בבקשה את הודעת האימות ששלחנו לכתובת האימייל שלך.<br>על מנת להשלים את התהליך הרישום יש ללחוץ על כפתור האימות המופיע בה.</br></br> מיד לאחר מכן תועברי למסך הכניסה לאתר.<br><strong>מחכים לפגוש אותך שם!</strong></br></br> אם אינך מוצאת את ההודעה בתיבת הדואר הנכנס, <strong>מומלץ לבדוק גם בספאם</strong> </br></br>האימייל ישלח בעוד:<b></b> ' ,
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
                        // this._wrapperFuncService.openLoginDialog();
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
                else
                    if (res.Message == 'Failed') {
                        Swal.fire({
                            title:
                                'ההרשמה לא הצליחה, היתה בעיה כלשהיא',
                            icon: 'error',
                            showClass: {
                                popup:
                                    'אנא בדוק שוב'
                            }
                        });

                    }

            },
                error => {
                    if (error.status == 500) {
                        this.toastrservice.info(
                            'אירעה שגיאה בעיבוד בקשה זו. בדוק פרטים או נסה שוב.',
                            '',
                            { positionClass: 'toast-top-right' }
                        );
                    }
                    if (error.error && error.error.value) {
                        this.errorList = [];
                        for (let i = 0; i < error.error.value.length; i++) {
                            this.errorList.push(error.error.value[i]);
                        }
                        this.showModalError();
                    }

                    this.isRegistrationInProcess = false;
                }
            );
    }
}
