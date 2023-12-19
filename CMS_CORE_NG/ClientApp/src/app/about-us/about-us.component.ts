import { Component, OnInit } from '@angular/core';
import { AccountService } from '../services/account.service';
import { WrapperSearchService } from '../services/wrapper-search.service';

@Component({
    selector: 'app-about-us',
    templateUrl: './about-us.component.html',
    styleUrls: ['./about-us.component.css']
})
export class AboutUsComponent implements OnInit {
    constructor(public _wrapperSearchService: WrapperSearchService, private acct: AccountService) { }

    ngOnInit(): void {
        this.acct.globalStateChanged.subscribe((state) => {
            this._wrapperSearchService.LoginStatus$.next(state.loggedInStatus);
        });
        this._wrapperSearchService.HomePage$.next(false);
        this.setBackgroundImage();

    }

    setBackgroundImage() {
        $('body').css({
            'background-image': 'none',
            'background-repeat': 'no-repeat',
            'background-size': 'cover'
        });
    }
}
