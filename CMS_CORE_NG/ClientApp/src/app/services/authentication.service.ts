import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ObservableStore } from "@codewithdan/observable-store";
import { SocialAuthService } from "angularx-social-login";
import { GoogleLoginProvider } from "angularx-social-login";
import { CookieService } from "ngx-cookie-service";
import { BehaviorSubject } from "rxjs";
import { map } from "rxjs/operators";
import Swal from "sweetalert2";
import { ExternalAuthModel } from "../interfaces/externalAuthModel";
import { StoreState } from "../interfaces/store-state";
import { AccountService } from "./account.service";
@Injectable({
  providedIn: 'root'
})
export class AuthenticationService extends ObservableStore<StoreState> {
  private baseUrlLoginWithGoogle: string = '/api/v1/account/ExternalLogin';

  //private loginStatus = new BehaviorSubject<boolean>(this.accountService.checkLoginStatus());
  //  private UserName = new BehaviorSubject<string>(this.cookieService.get('username'));
  //  private UserRole = new BehaviorSubject<string>(this.cookieService.get('userRole'));
  //  private Email = new BehaviorSubject<string>(null);
  constructor(private _externalAuthService: SocialAuthService, private http: HttpClient
    , private accountService: AccountService, private cookieService: CookieService) {
    super({ logStateChanges: true, trackStateHistory: true });

    this.accountService.loginStatus.subscribe((result) => {
      this.setState({ loggedInStatus: result }, 'LOGGED_IN_STATUS');
    });

  }

  public signInWithGoogle = () => {
    return this._externalAuthService.signIn(GoogleLoginProvider.PROVIDER_ID);
  }
  /*יציאה מ google*/
  public signOutExternal = () => {
    localStorage.setItem("OpenMegaMenue", "yes");//open the mega menue? -open only if its not google user
    this._externalAuthService.signOut();
  }

  /*כניסה עם google*/
  public externalLogin(externalAuth: ExternalAuthModel) {
    // Check if there is an active two factor session
    let isSessionActive = localStorage.getItem('isSessionActive');

    if (isSessionActive == '0' || isSessionActive == undefined || !(isSessionActive == '1')) {
      // Create a Login Model Object to send to API
      return this.http
        .post<any>(
          this.baseUrlLoginWithGoogle, externalAuth)
        .pipe(
          map((result) => {

            // First we need to update some values in localstorage before we return the result to client
            // Here we are checking if the user received an result and the result contains a auth token
            if (result && result.Token) {
              //אם הצליח - אם הוא תקין ואפשר לתת לו אפשרות כניסה למערכת
              this.accountService.loginStatus.next(true);
              this.accountService.UserName.next(this.cookieService.get('username'));
              this.accountService.UserRole.next(this.cookieService.get('userRole'));
              this.accountService.Email.next(this.cookieService.get('email'));
              this.accountService.FirstName.next(this.cookieService.get('firstName'));
              this.accountService.LastName.next(this.cookieService.get('lastName'));
              // this.wrapperSearchService._logInUserIdSubject.next(result.userId);
              localStorage.setItem("logInUserId", result.userId);
              localStorage.setItem("logInUserEmail", this.accountService.Email.value);
              localStorage.setItem("OpenMegaMenue", "no");
              // localStorage.setItem("logInUserLastName", this.accountService.LastName.value);
              // localStorage.setItem("logInUserFirstName", this.accountService.FirstName.value);
              // Next Get the Users Profile
              this.accountService.getUserProfile()
                .toPromise()
                .then(() => {
                  console.log('User Profile Fetched');
                });
            }
            return result;
          })
        );
    }
    else {
      Swal.fire({
        title: 'Session Active',
        text: 'ההפעלה הקודמת כבר פעילה. האם אתה רוצה לסיים את ההפעלה הקודמת?',
        icon: 'info',
        showCancelButton: true,
        confirmButtonText: 'End Session'
      })
      return null;
      //.then((result) => {
      //   if (result.value) {
      //     // Clear session
      //     this.sendExpiryNotification();
      //     window.location.reload();
      //   }
      // });
      // return new Observable();
    }
  }
}
