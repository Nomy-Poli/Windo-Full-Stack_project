import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AccountService } from 'src/app/services/account.service';
import { AdvertismentService, BannerVM } from 'src/app/services/advertisment.service';
import { BreadcrumbService } from 'src/app/services/breadcrumb.service';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';
import { WrapperSearchService } from 'src/app/services/wrapper-search.service';

@Component({
    selector: 'app-baners-catalog',
    templateUrl: './baners-catalog.component.html',
    styleUrls: ['./baners-catalog.component.scss']
})
export class BanersCatalogComponent implements OnInit {
    bannersList: BannerVM[] = [];
    constructor(
        public _wrapperSearchService: WrapperSearchService,
        private _acct: AccountService,
        public _funService: WrapperFuncService,
        public breadcrumbService: BreadcrumbService,
        private _advertismentService: AdvertismentService,
        private _wrapperFuncService: WrapperFuncService,
        private _activateRoute: ActivatedRoute
    ) {
        breadcrumbService.setItem([
            { label: 'דף הבית', routerLink: ['/'] /*, icon: 'pi pi-home'*/ },
            { label: 'אפשרויות פרסום', routerLink: ['.'] }
        ]);
        this._acct.globalStateChanged.subscribe((state) => {
            this._wrapperSearchService.LoginStatus$.next(state.loggedInStatus);
        });
        this._wrapperSearchService.Username$ = this._acct.currentUserName;
        this._wrapperSearchService.HomePage$.next(false);
    }

    ngOnInit(): void {
        this.getBanners();
    }

    getBanners() {
        this._advertismentService.getBanners().subscribe((res) => {
            this.bannersList = res;
            this._activateRoute.queryParams.subscribe((params) => {
                if (params['makat'] != null) {
                    let b = this.bannersList.find((x) => x.Makat == parseInt(params['makat']) );
                    if (b) {
                        b['showDetails'] = true;
                    }
                } else {
                    this.bannersList[0]['showDetails'] = true;
                }
            });

            console.log(res);
        });
    }

    openBannerDialog(banner) {
        this._wrapperFuncService.openBannerDialog(banner);
    }

    editBanner(banner) {
        this._funService.openBannerForm(banner.Id).subscribe((res) => {
            console.log(res);
            banner = res;
        });
    }

    openRequestDialog(banner?) {
        this._wrapperFuncService.openRequestOrderDialog(banner?.Makat);
    }
}
