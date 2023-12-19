import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { AccountService } from '../services/account.service';
import { ToastrService } from 'ngx-toastr';
import { WrapperFuncService } from '../services/wrapper-func.service';

@Component({
    selector: 'app-forgot-password',
    templateUrl: './forgot-password.component.html',
    styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {
    imageUrl: string = '/assets/images/717856_64046121.jpg';
    insertForm: FormGroup;
    Email: FormControl;
    sendEmailCliked:boolean=false;
    isLoading:boolean=false;

    constructor(private acct: AccountService, private fb: FormBuilder
        , private toasterService: ToastrService
        , public _wrapperFuncService: WrapperFuncService) { }

    ngOnInit(): void {
        this.setBackgroundImage();

        // Initialize Controls
        this.Email = new FormControl('', [Validators.required, Validators.email]);

        this.insertForm = this.fb.group({
            Email: this.Email
        });
    }
    // sendEmail(){
    //     console.log(this.sendEmailCliked);
    //     console.log("you are smart");
    //     this.sendEmailCliked=true;
    //     console.log(this.sendEmailCliked);
    // }
    onSubmit() {
        // let userInfo = this.insertForm.value;
        // this.sendEmailCliked =true;
        this.isLoading=true;
        let userInfo = this.insertForm.get('Email').value;
        console.log(this.sendEmailCliked);
        this.acct.sendForgotPasswordEmail(userInfo).subscribe(
            result => {
                if (result && result.Message == 'Success') {
                    console.log("result",result);
                    this.isLoading=false;
 
                    this.toasterService.success('!כדי להמשיך, עלייך ללחוץ על כפתור האתחול המופיע בהודעה ששלחנו עכשיו לכתובת האימייל שלך. אם אינך מוצאת את ההודעה בתיבת הדואר הנכנס, מומלץ לבדוק גם בספאם', 'בקשתך לאתחול סיסמה התקבלה', { positionClass: 'toast-top-right' });
                    // $('#forgotPassCard').html('');
                    // $('#forgotPassCard').append(
                    //     "<div class='alert alert-success show'>" +
                    //     '<strong>נשלח!</strong> אנא בדוק בדוא"ל שלך הוראות לאיפוס סיסמה.' +
                    //     '</div>'
                    // );
                }

                else
                    if (result && result.Message == 'Failed')
                   {
                    this.isLoading=false;

                 this.toasterService.error('שם המשתמש אינו תקין, לא אימתת עדין את כתובת האימייל. חפשי את הודעת האימות בתיבת הדואר הנכנס שלך. אם לא מצאת אותה, בדקי גם בספאם, שם המשתמש או הסיסמה שגויים, נסי שוב, שימי לב, אפשר להציג את הסיסמה כדי לוודא שהקלדת אותה נכון', '', { positionClass: 'toast-top-right' });
                   
                   }


            },
            error => {
                        this.isLoading=false;
                        this.toasterService.error('שם המשתמש אינו תקין, לא אימתת עדין את כתובת האימייל. חפשי את הודעת האימות בתיבת הדואר הנכנס שלך. אם לא מצאת אותה, בדקי גם בספאם, שם המשתמש או הסיסמה שגויים, נסי שוב, שימי לב, אפשר להציג את הסיסמה כדי לוודא שהקלדת אותה נכון', '', { positionClass: 'toast-top-right' });
                        console.log("error");

            }
        );
    }
   

    /* Set the background image when page loads */
    setBackgroundImage() {
        $('body').css({
            'background-image': '#cccccc', /* 'url(' + this.imageUrl + '),  linear-gradient(rgba(255, 0, 0, 0.5), rgba(255, 0, 0, 0.5))',*/
            'background-repeat': 'no-repeat',
            'background-size': '100px 100px',
            //'background-size': 'cover'
        });
    }
}
