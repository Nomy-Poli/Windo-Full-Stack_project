import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { ToastrService } from 'ngx-toastr';
import { BreadcrumbService } from 'src/app/services/breadcrumb.service';

@Component({
    selector: 'app-user-activity',
    templateUrl: './user-activity.component.html',
    styleUrls: ['./user-activity.component.css']
})
export class UserActivityComponent implements OnInit {
    userActivities: any = [];

    constructor(private acct: AccountService, private toastr: ToastrService
          ,public breadcrumbService: BreadcrumbService
        ) {
            breadcrumbService.setItem([
                { label: 'דף הבית', routerLink: ['/']/*, icon: 'pi pi-home'*/},
                { label: 'פעילויות', routerLink: ['/activity'] }
            ]);
        }  
    ngOnInit(): void {
        this.loadUserActivity();
    }

    loadUserActivity() {
        this.acct
            .getUserActivity()
            .toPromise()
            .then(result => {
                this.userActivities = result.data;
                this.toastr.success(result.message);
            });
    }
}
