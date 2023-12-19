import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { AccountService } from './account.service';
import { CookieService } from 'ngx-cookie-service';
import { Observable, BehaviorSubject } from 'rxjs';
import { ObservableStore } from '@codewithdan/observable-store';
import { StoreState } from '../interfaces/store-state';
import { take, map } from 'rxjs/operators';
import { WrapperFuncService } from './wrapper-func.service';
import Swal from 'sweetalert2';

@Injectable({
    providedIn: 'root'
})
export class AuthGuardService extends ObservableStore<StoreState> implements CanActivate {
    private loginStatus$ = new BehaviorSubject<boolean>(this.acct.checkLoginStatus());

    constructor(
        private acct: AccountService,
        private router: Router,
        private cookieService: CookieService,
        public _wrapperFuncService: WrapperFuncService
    ) {
        super({ logStateChanges: true, trackStateHistory: true });

        this.acct.globalStateChanged.subscribe((state) => {
            this.loginStatus$.next(state.loggedInStatus);
        });
    }
    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
        return this.loginStatus$.pipe(
            take(1),
            map((loginStatus: boolean) => {
                const destination: string = state.url;

                // To check if user is not logged in
                if (!loginStatus) {
                    this.router.navigate([this.router.url], { queryParams: { returnUrl: state.url } });
                    this._wrapperFuncService.openLoginDialog();
                    return false;
                }
                if (destination.includes('/message')) {
                    console.log(route.params['toEmail']);
                    //check if current user has business
                    // if(!this.acct.currentBusiness.value){
                    //     return false;
                    // }
                    if (route.queryParams.toEmail == this.acct.Email.value || !route.queryParams.toEmail) {
                        return true;
                    } else {
                        this.router.navigate([this.router.url], { queryParams: { returnUrl: state.url } });
                        this._wrapperFuncService.openLoginDialog();
                        Swal.fire({
                            title: 'את לא מחוברת לחשבון הנכון הכנסי מהחשבון שאליו נשלחה ההודעה ',
                            icon: 'error',
                            showClass: {
                                popup: 'animate__animated animate__fadeInDown'
                            },
                            hideClass: {
                                popup: 'animate__animated animate__fadeOutUp'
                            }
                        }).then((val) => {
                            return false;
                        });
                    }
                } else if (destination.includes('create-cs')) {
                    if (this.acct.isManager) {
                        return true;
                    }
                } else {
                    // if the user is already logged in
                    switch (destination) {
                        case '/myaccount': {
                            if (this.cookieService.get('userRole') === 'Customer') {
                                return true;
                            }
                            break;
                        }
                        case '/myaccount/settings': {
                            if (this.cookieService.get('userRole') === 'Customer') {
                                return true;
                            }
                            break;
                        }
                        case '/myaccount/activity': {
                            if (this.cookieService.get('userRole') === 'Customer') {
                                return true;
                            }
                            break;
                        }
                        case '/collaboration-list':
                            if (this.acct.isManager) {
                                return true;
                            }
                            break;
                        case '/orders-list':
                            if (this.acct.isManager) {
                                return true;
                            }
                            break;
                        case '/manager':
                            if (this.acct.isManager) {
                                return true;
                            }
                            break;
                        case '/order-service-requests-list':
                            if (this.acct.isManager) {
                                return true;
                            }
                            break;
                        default:
                            return true;
                    }
                }

                Swal.fire({
                    title: 'אין לך הרשאת גישה ',
                    icon: 'error',
                    showClass: {
                        popup: 'animate__animated animate__fadeInDown'
                    },
                    hideClass: {
                        popup: 'animate__animated animate__fadeOutUp'
                    }
                });
                return false;
            })
        );
    }
}
