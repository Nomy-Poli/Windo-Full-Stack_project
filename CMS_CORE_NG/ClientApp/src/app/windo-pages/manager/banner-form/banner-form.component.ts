import { HttpClient } from '@angular/common/http';
import {
    Directive,
    Component,
    AfterContentChecked,
    EventEmitter,
    ChangeDetectorRef,
    Inject,
    OnInit,
    Optional,
    Output,
    HostListener,
    HostBinding
} from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { AdvertismentService, BannerVM } from 'src/app/services/advertisment.service';
import { BreadcrumbService } from 'src/app/services/breadcrumb.service';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';
import { MessageService as ToastService } from 'primeng/api';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { pairwise, startWith } from 'rxjs/operators';

import { BannersListComponent } from '../banners-list/banners-list.component';
import Swal from 'sweetalert2';
import { DomSanitizer } from '@angular/platform-browser';
import { style } from '@angular/animations';
import { localizedString } from '@angular/compiler/src/output/output_ast';

// import {
//   FileHandle
// } from '../image-drag.directive';
@Component({
    selector: 'app-banner-form',
    templateUrl: './banner-form.component.html',
    styleUrls: ['./banner-form.component.scss']
})
export class BannerFormComponent implements OnInit, AfterContentChecked {
    constructor(
        public _wrapperSearchService: WrapperSearchService,
        private _httpClient: HttpClient,
        private _acct: AccountService,
        public _funcService: WrapperFuncService,
        private _advertismentService: AdvertismentService,
        private cdref: ChangeDetectorRef,
        public _toast: ToastService,
        @Optional() @Inject('API_BASE_URL') apiBaseUrl?: string
    ) {
        this.apiBaseUrl = apiBaseUrl ? apiBaseUrl : '';
    }
    bannerId;
    @Output() closed = new EventEmitter();
    isLoading = false;
    isFormValueChanges = false;
    apiBaseUrl: String;
    banner: BannerVM;
    hasImg = [false, false, false];
    imgBeforeChanges = [false, false, false];
    MAX_SIZE: number = 307200;
    uploadedFile = [null, null];
    pictureFile = [null, null];
    pictureFileBeforeChanges = [null, null];
    isaddedFile = false;
    catalogServices = [];
    form: FormGroup = new FormGroup({
        Makat: new FormControl(null, Validators.required),
        Title: new FormControl(null, [Validators.required, Validators.maxLength(50)]),
        DefaultLink: new FormControl(null, Validators.required),
        PageName: new FormControl(null, Validators.required),
        PageID: new FormControl(null),
        PriceInPoints: new FormControl(null),
        Price: new FormControl(null),
        Height: new FormControl(null),
        Width: new FormControl(null)
    });

    MakatBeforeChanges: number;
    TitleBeforeChanges: string;
    DefaultLinkBeforeChanges: string;
    PageNameBeforeChanges: string;
    PageIDBeforeChanges: number;
    PriceInPointsBeforeChanges: number;
    PriceBeforeChanges: number;
    HeightBeforeChanges: number;
    WidthBeforeChanges: number;

    ngAfterContentChecked() {
        this.cdref.detectChanges();
    }
    submitToolTip = 'עדין לא נערכו שינויים בטופס!';
    onClick = false;
    ngOnInit(): void {
        this._advertismentService.getCatalogServices().subscribe((res) => {
            this.catalogServices = res;
        });
        if (this.bannerId) {
            this._advertismentService.getBanner(this.bannerId).subscribe((res) => {
                this.banner = res;
                console.log(res.Makat);

                if (this.banner.DefaultPicGuid != null && this.banner.DefaultPicGuid != '00000000-0000-0000-0000-000000000000') {
                    this.hasImg[0] = true;
                    setTimeout(() => {
                        $('#img1').attr('src', `../../../../../assets/advertisments/${this.banner.Makat}/default/${this.banner.DefaultPicGuid}.jpg`);
                    }, 1000);
                }
                if (this.banner.ExamplePicGuid != null && this.banner.ExamplePicGuid != '00000000-0000-0000-0000-000000000000') {
                    this.hasImg[1] = true;
                    setTimeout(() => {
                        $('#img2').attr('src', `../../../../../assets/advertisments/${this.banner.Makat}/example/${this.banner.ExamplePicGuid}.jpg`);
                    }, 1000);
                }
                if (this.banner.FormatPicGuid != null && this.banner.FormatPicGuid != '00000000-0000-0000-0000-000000000000') {
                    this.hasImg[2] = true;
                    setTimeout(() => {
                        $('#img3').attr('src', `../../../../../assets/advertisments/${this.banner.Makat}/format/${this.banner.FormatPicGuid}.jpg`);
                    }, 1000);
                }
                this.pictureFileBeforeChanges[1] = this.banner.FormatPicGuid;
                console.log('pictureFileBeforeChanges', this.pictureFileBeforeChanges[0]);

                this.hasImg.forEach((ele, index) => {
                    this.imgBeforeChanges[index] = ele;
                });
                console.log('imgBeforeChanges', this.imgBeforeChanges);

                this.form.patchValue({
                    Makat: res.Makat,
                    Title: res.Title,
                    DefaultLink: res.DefaultLink,
                    PageName: res.PageName,
                    PageID: res.PageID,
                    PriceInPoints: res.PriceInPoints,
                    Price: res.Price,
                    Height: res.Height,
                    Width: res.Width
                });

                this.MakatBeforeChanges = res.Makat;
                this.HeightBeforeChanges = res.Height;
                this.DefaultLinkBeforeChanges = res.DefaultLink;
                this.PageIDBeforeChanges = res.PageID;
                this.PageNameBeforeChanges = res.PageName;
                this.PriceBeforeChanges = res.Price;
                this.PriceInPointsBeforeChanges = res.PriceInPoints;
                this.TitleBeforeChanges = res.Title;
                this.WidthBeforeChanges = res.Width;
            });
        } else {
            this.banner = {} as BannerVM;
        }
    }
    filesDropped(event, index) {
        this.hasImg[index - 1] = true;
        console.log('hhh', event);
        let reader = new FileReader();
        this.pictureFile[index - 1] = event[0].file;
        reader.readAsDataURL(event[0].file);
        reader.onload = () => {
            $('#img' + index).attr('src', reader.result as string);
        };
        this.pictureFileBeforeChanges[index - 1] = this.pictureFile[index - 1];

        console.log('pic=', reader.result);

        console.log('upload-files', this.pictureFile[index - 1]);
    }

    //#checkValidation
    checkValidation() {
        let valid: Boolean = false;
        valid = this.form.valid;

        for (let i = 0; i < this.imgBeforeChanges.length; i++) {
            if (this.imgBeforeChanges[i] != this.hasImg[i]) {
                return false;
            }
        }
        if (valid && this.isaddedFile == true) return false;

        if (valid && this.checkIfDirty()) {
            return false;
        }
        if (!this.checkIfDirty()) {
            this.submitToolTip = 'עדין לא נערכו שינויים בטופס!';
            return true;
        } else {
            this.submitToolTip = 'אין אפשרות לעדכן יש פרטים שעדיין לא הושלמו!';
            return true;
        }
    }
    checkIfDirty() {
        if (
            this.MakatBeforeChanges != this.form.value.Makat ||
            this.DefaultLinkBeforeChanges != this.form.get('DefaultLink').value ||
            this.HeightBeforeChanges != this.form.get('Height').value ||
            this.PageIDBeforeChanges != this.form.get('PageID').value ||
            this.PageNameBeforeChanges != this.form.get('PageName').value ||
            this.PriceBeforeChanges != this.form.get('Price').value ||
            this.PriceInPointsBeforeChanges != this.form.get('PriceInPoints').value ||
            this.TitleBeforeChanges != this.form.get('Title').value ||
            this.WidthBeforeChanges != this.form.get('Width').value
        )
            return true;

        return false;
    }
    //#region upload image
    onWorkFileChanged(event, index) {
        if (event.target.files && event.target.files[0]) {
            // if (event.target.files[0].size < this.MAX_SIZE) {
            this.hasImg[index - 1] = true;
            let reader = new FileReader();
            this.pictureFile[index - 1] = event.target.files[0];
            reader.readAsDataURL(event.target.files[0]);
            reader.onload = () => {
                $('#img' + index).attr('src', reader.result as string);
            };
            this.isaddedFile = true;
            console.log('upload-files', this.pictureFile[index - 1]);
            // }
            // else{
            //   this._toast.add({severity:'error', detail: 'יש להעלות קובץ ששוקל עד 300KB'})
            // }
        }
    }
    triggerWorkInput(index) {
        $('#workpicfile' + index).trigger('click');
    }
    RemoveImage(index) {
        this.hasImg[index - 1] = false;
        $('#img' + index).attr('src', ' ');
    }
    close() {
        this._funcService.closeDialog();
    }

    submitForm() {
        this.onClick = true;
        console.log('banner', this.form);
        if (this.form.valid) {
            let formValue = this.form.value;
            this.banner.Makat = formValue.Makat;
            this.banner.Title = formValue.Title;
            this.banner.DefaultLink = formValue.DefaultLink;
            this.banner.PageName = formValue.PageName;
            this.banner.PageID = formValue.PageID;
            this.banner.PriceInPoints = formValue.PriceInPoints;
            this.banner.Price = formValue.Price;
            this.banner.Height = formValue.Height;
            this.banner.Width = formValue.Width;
            const formD = new FormData();
            formD.append('model', JSON.stringify(this.banner));
            formD.append('defaultPicture', this.pictureFile[0]);
            formD.append('examplePicture', this.pictureFile[1]);
            formD.append('formatPicture', this.pictureFile[2]);

          
            if (!this.checkValidation()) {
                this._advertismentService.updateBanner();

                this.isLoading = true;
                console.log('loading=', this.isLoading);
                return this._httpClient
                    .post(this.apiBaseUrl + `/api/Advertisment/updateBanner`, formD, {
                        headers: { 'Content-Type': [] },
                        reportProgress: true
                    })
                    .subscribe((res: number) => {
                        this.isLoading = false;
                        console.log('loading=', this.isLoading);

                        console.log('res=', res);
                        // console.log(this.beforeChanges);
                        console.log(this.form.value);

                        this.banner.Id = res;

                        Swal.fire('פרטי הבאנר נשמרו במערכת').then((_val) => {
                            this.closed.emit(this.banner);
                            this._funcService.closeDialog();
                        });
                    });
            }
        }
    }
    //#endregion
}
