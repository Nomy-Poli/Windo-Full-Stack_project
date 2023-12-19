import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { AccountService } from 'src/app/services/account.service';
import { ToastrService } from 'ngx-toastr';
import { Address } from '../../interfaces/address';
import { Profile } from '../../interfaces/profile';
import { CountryService } from 'src/app/services/country.service';
import { Observable } from 'rxjs';
import { Country } from 'src/app/interfaces/country';
import Swal from 'sweetalert2';
import { BreadcrumbService } from 'src/app/services/breadcrumb.service';


@Component({
    selector: 'app-user-profile',
    templateUrl: './user-profile.component.html',
    styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {
    /* Pre-Load Properties */
    ProfileDetails: Profile;
    isProfileLoaded: boolean = false;
    // BillingAddress: Address;
    // ShippingAddress: Address;

    /* Properties for the profile form */
    updateProfileForm: FormGroup;
    userid: FormControl;
    email: FormControl;
    username: FormControl;
    phone: FormControl;
    birthdate: FormControl;
    // gender: FormControl;
    // displayname: FormControl;
    // address1: FormControl;
    // address2: FormControl;
    // country: FormControl;
    // city: FormControl;
    // state: FormControl;
    // postalcode: FormControl;
    // saddress1: FormControl;
    // saddress2: FormControl;
    // scountry: FormControl;
    // scity: FormControl;
    // sstate: FormControl;
    spostalcode: FormControl;
    profpicfile: FormControl;
    firstname: FormControl;
    lastname: FormControl;
    middlename: FormControl;
    // isTwoFactorOn: FormControl;
    // isPhoneVerified: FormControl;
    // isEmailVerified: FormControl;
    // isTermsAccepted: FormControl;
    // unit: FormControl;
    // sunit: FormControl;
    userRole: string;

    private genderValues = ['Male', 'Female'];
    country$: Observable<Country[]>;
    countries: Country[] = [];
    modalMessage: string;
    modalTitle: string;
    errorList: string[] = [];
    @ViewChild('datePicker') calendar:any;
    constructor(private fb: FormBuilder
        , private acct: AccountService
        , private toastr: ToastrService        
        , public breadcrumbService: BreadcrumbService
        ) {
            breadcrumbService.setItem([
                { label: 'דף הבית', routerLink: ['/']/*, icon: 'pi pi-home'*/},
                { label: 'פרופיל', routerLink: ['/myaccount'] }
            ]);
        }

    ngOnInit(): void {
        // this.BillingAddress = {
        //     addressId: 0,
        //     line1: '',
        //     line2: '',
        //     country: '',
        //     city: '',
        //     state: '',
        //     postalCode: '',
        //     type: '',
        //     unit: '',
        //     userId: ''
        // }
        // this.ShippingAddress = {
        //     addressId: 0,
        //     line1: '',
        //     line2: '',
        //     country: '',
        //     city: '',
        //     state: '',
        //     postalCode: '',
        //     type: '',
        //     unit: '',
        //     userId: ''
        // }
        this.loadUserProfile();
        // this.loadCountries();
        window.scroll(0, 0);
    }

    onSubmit() {
        if (this.updateProfileForm.valid) {
            let userDetails = this.updateProfileForm.value;
            // userDetails.country = this.ProfileDetails.billingAddress.country;
            // userDetails.scountry = this.ProfileDetails.shippingAddress.country;
            userDetails.gender = 'Female'
            // this.ProfileDetails.gender == 'null' || this.ProfileDetails.gender == '' ? 'Prefer Not To Say' : this.ProfileDetails.gender;

            // Swal.fire({
            //     title: 'הכנס את הסיסמה שלך',
            //     input: 'password',
            //     inputAttributes: {
            //         autocapitalize: 'off'
            //     },
            //     showCancelButton: true,
            //     confirmButtonText: 'עדכון',
            //     showLoaderOnConfirm: true,
            //     preConfirm: (password) => {
            //         userDetails.password = password;
                    this.acct.updateProfile(userDetails).subscribe(result => {
                
                        if(result){
                            this.ProfileDetails=userDetails;
                            Swal.fire('הפרטים התעדכנו בהצלחה!').then((val)=>{
                
                            });
                        }
        
                    })
    }}
                        // if (result.message == 'Pfailed') {
                        //     this.toastr.error('לא היה ניתו לעדכן את הפרופיל - הסיסמה לא תואמת');
                        // }
                        // else {
                        //     this.toastr.success(result.message);
                        //     this.acct.clearCache();
                        //     this.loadUserProfile();
                        //     this.isProfileLoaded = true;
                        // }
        //             },
        //                 err => {
        //                     this.toastr.error('ארעה שגיאה בעת עדכון הפרטים, נסי שוב');
        //                 });
        //         }
        //     }).then((result) => {
        //         if (result.dismiss) {
        //             this.isProfileLoaded = true;
        //         }
        //     });
        // } else {
        //     this.errorList = [];
        //     const controls = this.updateProfileForm.controls;

        //     for (let name in controls) {
        //         if (controls[name].invalid) {
        //             let errorDescription = '';
        //             if (controls[name].hasError('required')) {
        //                 switch (name) {
        //                     case 'isTermsAccepted':
        //                         errorDescription = 'נדרש לאשר קבלת תנאים';
        //                         this.errorList.push(errorDescription);
        //                         break;
        //                     case 'sscity':
        //                         errorDescription = ' נדרש עיר למישלוח';
        //                         this.errorList.push(errorDescription);
        //                         break;
        //                     case 'sstate':
        //                         errorDescription = ' נדרש מדינה ';
        //                         this.errorList.push(errorDescription);
        //                         break;
        //                     case 'saddress1':
        //                         errorDescription = ' נדרש כתובת אחת למישלוח';
        //                         this.errorList.push(errorDescription);
        //                         break;
        //                     case 'scity':
        //                         errorDescription = ' נדרש עיר למשלוח';
        //                         this.errorList.push(errorDescription);
        //                         break;
        //                     case 'spostalcode':
        //                         errorDescription = ' נדרש מיקוד למישלוח';
        //                         this.errorList.push(errorDescription);
        //                         break;
        //                     default:
        //                         switch (name) {
        //                             case 'displayname':
        //                                 errorDescription = ' שדה חובה : תאור שם';
        //                                 this.errorList.push(errorDescription);
        //                                 break;
        //                             case 'address1':
        //                                 errorDescription = ' שדה חובה : כתובת';
        //                                 this.errorList.push(errorDescription);
        //                                 break;
        //                             case 'city':
        //                                 errorDescription = ' שדה חובה : עיר';
        //                                 this.errorList.push(errorDescription);
        //                                 break;
        //                             case 'state':
        //                                 errorDescription = ' שדה חובה : מדינה';
        //                                 this.errorList.push(errorDescription);
        //                                 break;
        //                             case 'postalcode':
        //                                 errorDescription = ' שדה חובה : עיר';
        //                                 this.errorList.push(errorDescription);
        //                                 break;
        //                             default:
        //                                 errorDescription = ' שדה חובה ' + name;
        //                                 this.errorList.push(errorDescription);
        //                                 break;

        //                         }

        //                 }
        //             } else {
        //                 errorDescription = ' בדוק את ה ' + name;
        //                 this.errorList.push(errorDescription);
        //                 errorDescription = ' הערה : שם משתמש חייב להיות תואם לכתובת המייל, אנא ודא זאת.';
        //                 this.errorList.push(errorDescription);
        //             }
        //             controls[name].markAsTouched();
        //         }
        //     }
        //     console.log(this.errorList);
        //     this.showErrorModal();
        // }
    

    // loadCountries() {
    //     this.country$ = this.countryservice.getCountries();
    //     this.country$.subscribe((countrylist) => {
    //         this.countries = countrylist;
    //     });
    // }

    loadUserProfile() {
        this.acct.getUserProfile().subscribe((result) => {
            // result.useAddress.forEach((obj, index) => {
            //     if (obj.type == 'Billing') {
            //         this.BillingAddress = obj;
            //         for (let billingAddressObj in this.BillingAddress) {
            //             if (this.BillingAddress.hasOwnProperty(billingAddressObj)) {
            //                 if (this.BillingAddress[billingAddressObj] == 'null') {
            //                     this.BillingAddress[billingAddressObj] = '';
            //                 }
            //             }
            //         }
            //     }
            //     if (obj.type == 'Shipping') {
            //         this.ShippingAddress = obj;
            //         for (let shippingAddressObj in this.ShippingAddress) {
            //             if (this.ShippingAddress.hasOwnProperty(shippingAddressObj)) {
            //                 if (this.ShippingAddress[shippingAddressObj] == 'null') {
            //                     this.ShippingAddress[shippingAddressObj] = '';
            //                 }
            //             }
            //         }
            //     }
            // });
            this.ProfileDetails = {
                userid: result.UserId,
                userRole: result.UserRole,
                email: result.Email,
                username: result.Username,
                phone: result.Phone,
                birthday: result.Birthday,
                gender: result.Gender,
                displayname: result.Displayname,
                // billingAddress: this.BillingAddress,
                // shippingAddress: this.ShippingAddress,
                billingAddress: null,
                shippingAddress: null,
                firstname: result.Firstname,
                profpicfile: result.ProfilePic,
                lastname: result.Lastname,
                middlename: result.Middlename,
                isTwoFactorOn: result.IsTwoFactorOn,
                isPhoneVerified: result.IsPhoneVerified,
                isEmailVerified: result.IsEmailVerified,
                isTermsAccepted: result.IsTermsAccepted
            };
            this.createFormGroup();

            this.isProfileLoaded = true;

            console.log(this.ProfileDetails);
        });
    }

    createFormGroup() {
        this.userid = new FormControl(this.ProfileDetails.userid, [Validators.required]);
        this.email = new FormControl(this.ProfileDetails.email, [Validators.required, Validators.email]);
        this.username = new FormControl(this.ProfileDetails.username, [Validators.required, Validators.maxLength(30), Validators.minLength(5)]);
        this.phone = new FormControl(this.ProfileDetails.phone, [
            Validators.required,
            Validators.pattern('^\\(?([0-9]{3})\\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$')
        ]);
        this.birthdate = new FormControl(this.ProfileDetails.birthday, [Validators.required]);
        // this.gender = new FormControl(this.ProfileDetails.gender);
        // this.displayname = new FormControl(this.ProfileDetails.displayname, [Validators.required, Validators.maxLength(10)]);
        // this.unit = new FormControl(this.BillingAddress.unit, [Validators.maxLength(10)]);
        // this.address1 = new FormControl(this.BillingAddress.line1, [Validators.required, Validators.maxLength(50), Validators.minLength(2)]);
        // this.address2 = new FormControl(this.BillingAddress.line2, [Validators.maxLength(50)]);
        // this.country = new FormControl(this.BillingAddress.country, [Validators.required]);
        // this.city = new FormControl(this.BillingAddress.city, [Validators.required, Validators.maxLength(20), Validators.minLength(2)]);
        // this.state = new FormControl(this.BillingAddress.state, [Validators.required]);
        // this.postalcode = new FormControl(this.BillingAddress.postalCode, [Validators.required, Validators.maxLength(15), Validators.minLength(2)]);
        // this.sunit = new FormControl(this.ShippingAddress.unit, [Validators.maxLength(10)]);
        // this.saddress1 = new FormControl(this.ShippingAddress.line1, [Validators.maxLength(50)]);
        // this.saddress2 = new FormControl(this.ShippingAddress.line2, [Validators.maxLength(50)]);
        // this.scountry = new FormControl(this.ShippingAddress.country);
        // this.scity = new FormControl(this.ShippingAddress.city, [Validators.maxLength(20)]);
        // this.sstate = new FormControl(this.ShippingAddress.state);
        // this.spostalcode = new FormControl(this.ShippingAddress.postalCode, [Validators.maxLength(15)]);
        this.profpicfile = new FormControl(this.ProfileDetails.profpicfile);
        this.firstname = new FormControl(this.ProfileDetails.firstname, [Validators.required, Validators.maxLength(30), Validators.minLength(2)]);
        this.middlename = new FormControl(this.ProfileDetails.middlename, [Validators.maxLength(15)]);
        this.lastname = new FormControl(this.ProfileDetails.lastname, [Validators.required, Validators.maxLength(15), Validators.minLength(2)]);
        // this.isTwoFactorOn = new FormControl(this.ProfileDetails.isTwoFactorOn);
        // this.isPhoneVerified = new FormControl(this.ProfileDetails.isPhoneVerified);
        // this.isEmailVerified = new FormControl(this.ProfileDetails.isEmailVerified);
        // this.isTermsAccepted = new FormControl(this.ProfileDetails.isTermsAccepted, [Validators.requiredTrue]);

        this.updateProfileForm = this.fb.group({

            userid: this.userid,
            email: this.email,
            username: this.username,
            phone: this.phone,
            birthdate: this.birthdate,
            // gender: this.gender,
            // displayname: this.displayname,
            // unit: this.unit,
            // address1: this.address1,
            // address2: this.address2,
            // country: this.country,
            // city: this.city,
            // state: this.state,
            // postalcode: this.postalcode,
            // sunit: this.sunit,
            // saddress1: this.saddress1,
            // saddress2: this.saddress2,
            // scountry: this.scountry,
            // scity: this.scity,
            // sstate: this.sstate,
            spostalcode: this.spostalcode,
            firstname: this.firstname,
            middlename: this.middlename,
            lastname: this.lastname,
            // isTwoFactorOn: this.isTwoFactorOn,
            profpicfile: this.profpicfile,
            // isPhoneVerified: this.isPhoneVerified,
            // isEmailVerified: this.isEmailVerified,
            // isTermsAccepted: this.isTermsAccepted
        });
    }
    
   
    editDob() {
        // console.log( this.calendar);
        //  this.calendar.toggle();
        const $birthdate = $('#birthdate');

        $birthdate.removeAttr('disabled');
        $birthdate.datepicker({
            changeMonth: true,
            changeYear: true,
            yearRange: '1920:2099',
            onSelect: (dateText) => {
                this.birthdate.setValue(dateText);
            }
        });
    }

    // editGender() {
    //     const $addGenderDropdown = $('#gender');

    //     $addGenderDropdown.removeAttr('disabled');
    //     let genderOptionsValues =
    //         '<option [ngValue]="' +
    //             this.ProfileDetails.gender +
    //             '" disabled selected>' +
    //             (this.ProfileDetails.gender == '' || this.ProfileDetails.gender == null)
    //             ? 'Select Gender'
    //             : this.ProfileDetails.gender + '</option>';
    //     this.genderValues.forEach((obj, index) => {
    //         genderOptionsValues += '<option [ngValue]="' + obj + '">' + obj + '</option>';
    //     });

    //     $addGenderDropdown.append(genderOptionsValues);
    //     $addGenderDropdown.chosen();
    //     $addGenderDropdown.chosen().on('change', (event) => {
    //         this.ProfileDetails.gender = $(event.target).val().toString();
    //         console.log(this.ProfileDetails.gender);
    //     });
    // }

    // editAddress() {
    //     this.enableAddressControls();

    //     const $billingCountry = $('#country');
    //     const $billingState = $('#state');
    //     const $shippingCountry = $('#scountry');
    //     const $shippingState = $('#sstate');

    //     /* START Billing Country */
    //     let countryOptionsValues =
    //         '<option [ngValue]="' +
    //             this.ProfileDetails.billingAddress.country +
    //             '" disabled selected>' +
    //             (this.ProfileDetails.billingAddress.country == '' || this.ProfileDetails.billingAddress.country == null)
    //             ? 'Select Country'
    //             : this.ProfileDetails.billingAddress.country + '</option>';
    //     this.countries.forEach((obj, index) => {
    //         countryOptionsValues += '<option value="' + obj.id + '">' + obj.flag + ' ' + obj.name + '</option>';
    //     });

    //     $billingCountry.append(countryOptionsValues);
    //     $billingCountry.chosen();
    //     $billingCountry.chosen().on('change', (event) => {
    //         let countryId = $(event.target).val();

    //         let country = this.countries.filter(function (x) {
    //             return x.id === Number(countryId);
    //         });

    //         if (country.length > 0) {
    //             this.ProfileDetails.billingAddress.country = country[0].name.toString();

    //             let states = [];

    //             if (country[0].states !== null && country[0].states !== '') {
    //                 states = country[0].states.split('|');

    //                 let optionsValues = '<option value="Select State" disabled selected>בחר אזור</option>';

    //                 states.forEach((item, index) => {
    //                     optionsValues += '<option value="' + item + '">' + item + '</option>';
    //                 });

    //                 $billingState.html('');
    //                 $billingState.append(optionsValues);
    //                 $billingState.prop('disabled', false).trigger('chosen:updated');
    //             } else {
    //                 /* Disable DropDown as no states available */
    //                 $billingState.val(null);
    //                 $billingState.prop('disabled', true).trigger('chosen:updated');
    //             }
    //         }
    //         /* END Billing Country */
    //     });

    //     let shippingCountryOptionsValues =
    //         '<option [ngValue]="' +
    //             this.ProfileDetails.shippingAddress.country +
    //             '" disabled selected>' +
    //             (this.ProfileDetails.shippingAddress.country == '' || this.ProfileDetails.shippingAddress.country == null)
    //             ? 'Select Country'
    //             : this.ProfileDetails.shippingAddress.country + '</option>';
    //     this.countries.forEach((obj, index) => {
    //         shippingCountryOptionsValues += '<option value="' + obj.id + '">' + obj.flag + ' ' + obj.name + '</option>';
    //     });

    //     $shippingCountry.append(shippingCountryOptionsValues);
    //     $shippingCountry.chosen();
    //     $shippingCountry.chosen().on('change', (event) => {
    //         let scountryId = $(event.target).val();

    //         let scountry = this.countries.filter(function (x) {
    //             return x.id === Number(scountryId);
    //         });

    //         if (scountry.length > 0) {
    //             this.ProfileDetails.shippingAddress.country = scountry[0].name.toString();

    //             let states = [];

    //             if (scountry[0].states !== null && scountry[0].states !== '') {
    //                 states = scountry[0].states.split('|');

    //                 let soptionsValues = '<option value="Select State" disabled selected>בחר אזור</option>';

    //                 states.forEach((item, index) => {
    //                     soptionsValues += '<option value="' + item + '">' + item + '</option>';
    //                 });

    //                 $shippingState.html('');
    //                 $shippingState.append(soptionsValues);
    //                 $shippingState.prop('disabled', false).trigger('chosen:updated');
    //             } else {
    //                 /* Disable DropDown as no states available */
    //                 $shippingState.val(null);
    //                 $shippingState.prop('disabled', true).trigger('chosen:updated');
    //             }
    //         }
    //         /* END Billing Country */
    //     });
    // }

    // enableAddressControls() {
    //     const $address1 = $('#address1');
    //     const $address2 = $('#address2');
    //     const $country = $('#country');
    //     const $state = $('#state');
    //     const $unit = $('#unit');
    //     const $city = $('#city');
    //     const $postalcode = $('#postalcode');

    //     const $saddress1 = $('#saddress1');
    //     const $saddress2 = $('#saddress2');
    //     const $scountry = $('#scountry');
    //     const $sstate = $('#sstate');
    //     const $sunit = $('#sunit');
    //     const $scity = $('#scity');
    //     const $spostalcode = $('#spostalcode');

    //     $address1.removeAttr('disabled');
    //     $address2.removeAttr('disabled');
    //     $country.removeAttr('disabled');
    //     $unit.removeAttr('disabled');
    //     $state.removeAttr('disabled');
    //     $city.removeAttr('disabled');
    //     $postalcode.removeAttr('disabled');

    //     $saddress1.removeAttr('disabled');
    //     $saddress2.removeAttr('disabled');
    //     $scountry.removeAttr('disabled');
    //     $sstate.removeAttr('disabled');
    //     $sunit.removeAttr('disabled');
    //     $scity.removeAttr('disabled');
    //     $spostalcode.removeAttr('disabled');
    // }

    showErrorModal() {
        this.modalTitle = 'בעית עדכון';
        this.modalMessage = 'אנא בדוק שגיאות ונסה שוב';
        $('#errorModal').modal('show');
    }

    triggerInput() {
        $('#profpicfile').trigger('click');
    }

    onFileChanged(event) {
        if (event.target.files && event.target.files[0]) {
            let reader = new FileReader();
            let file = event.target.files[0];
            this.updateProfileForm.get('profpicfile').setValue(file);
            reader.readAsDataURL(file);
            reader.onload = () => {
                $('#profpic')
                    .find('img')
                    .attr('src', reader.result as string);
            };
        }
    }
}
