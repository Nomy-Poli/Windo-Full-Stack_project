import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { AccountService } from 'src/app/services/account.service';
import { ValidatorService } from 'src/app/services/common/validator.service';
import { ToastrService } from 'ngx-toastr';
import { WrapperFuncService } from '../../services/wrapper-func.service';
import { BreadcrumbService } from 'src/app/services/breadcrumb.service';
@Component({
    selector: 'app-user-settings',
    templateUrl: './user-settings.component.html',
    styleUrls: ['./user-settings.component.css']
})
export class UserSettingsComponent implements OnInit {
    updatePasswordForm: FormGroup;
    oldPassword: FormControl;
    newPassword: FormControl;
    cnewPassword: FormControl;

    constructor(private fb: FormBuilder, private acct: AccountService, private validatorService: ValidatorService, private toastr: ToastrService,public _wrapperFuncService: WrapperFuncService
        ,public breadcrumbService: BreadcrumbService
        ) {
            breadcrumbService.setItem([
                { label: 'דף הבית', routerLink: ['/']/*, icon: 'pi pi-home'*/ },
                { label: 'סיסמה', routerLink: ['/myaccount/settings'] }
            ]);
        }  
    

    ngOnInit(): void {
        this.oldPassword = new FormControl('', [Validators.required]);
        this.newPassword = new FormControl('', [Validators.required, Validators.maxLength(15), Validators.minLength(8)]);
        this.cnewPassword = new FormControl('', [Validators.required, this.validatorService.MustMatch(this.newPassword)]);

        this.updatePasswordForm = this.fb.group({
            oldPassword: this.oldPassword,
            newPassword: this.newPassword,
            cnewPassword: this.cnewPassword
        });
    }

    onSubmit() {
        if (this.updatePasswordForm.valid) {
            this.acct.getUserProfile().subscribe(result => {
                if (result.Email) {
                    let userDetails = this.updatePasswordForm.value;
                    userDetails.email = result.Email;
                    this.acct.changePassword(userDetails).subscribe(
                        (result) => {
                            this.toastr.success(result.message);
                        },
                        (error) => {
                            if (error.error == undefined) this.toastr.error(error.message);
                            else this.toastr.error(error.error.message)
                        }
                    );
                }
            });
        }
    }
}
