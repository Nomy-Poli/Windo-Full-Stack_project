//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.19.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v12.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

/* tslint:disable */
/* eslint-disable */
// ReSharper disable InconsistentNaming

import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpResponseBase } from '@angular/common/http';

export const BASE_URL = new InjectionToken<string>('BASE_URL');

export interface IMistamshimService {
    getUsers(): Observable<ApplicationUser[] | null>;
}

@Injectable()
export class MistamshimService implements IMistamshimService {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
    }

    getUsers(): Observable<ApplicationUser[] | null> {
        let url_ = this.baseUrl + "/api/Mistamshim/GetUsers";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processGetUsers(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processGetUsers(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<ApplicationUser[] | null>;
                }
            } else
                return _observableThrow(response_) as any as Observable<ApplicationUser[] | null>;
        }));
    }

    protected processGetUsers(response: HttpResponseBase): Observable<ApplicationUser[] | null> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            result200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as ApplicationUser[];
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<ApplicationUser[] | null>(null as any);
    }
}

export interface IdentityUserOfString {
    Id?: string | undefined;
    UserName?: string | undefined;
    NormalizedUserName?: string | undefined;
    Email?: string | undefined;
    NormalizedEmail?: string | undefined;
    EmailConfirmed: boolean;
    PasswordHash?: string | undefined;
    SecurityStamp?: string | undefined;
    ConcurrencyStamp?: string | undefined;
    PhoneNumber?: string | undefined;
    PhoneNumberConfirmed: boolean;
    TwoFactorEnabled: boolean;
    LockoutEnd?: Date | undefined;
    LockoutEnabled: boolean;
    AccessFailedCount: number;
}

export interface IdentityUser extends IdentityUserOfString {
}

export interface ApplicationUser extends IdentityUser {
    Notes?: string | undefined;
    DisplayName?: string | undefined;
    Firstname?: string | undefined;
    Middlename?: string | undefined;
    Lastname?: string | undefined;
    Gender?: string | undefined;
    ProfilePic?: string | undefined;
    Birthday?: string | undefined;
    IsProfileComplete: boolean;
    Terms: boolean;
    IsEmployee: boolean;
    UserRole?: string | undefined;
    AccountCreatedOn: Date;
    RememberMe: boolean;
    IsActive: boolean;
    UserAddresses?: AddressModel[] | undefined;
}

export interface AddressModel {
    AddressId: number;
    Line1?: string | undefined;
    Line2?: string | undefined;
    Unit?: string | undefined;
    Country: string;
    State?: string | undefined;
    City?: string | undefined;
    PostalCode?: string | undefined;
    Type?: string | undefined;
    UserId?: string | undefined;
    User?: ApplicationUser | undefined;
}

export class ApiException extends Error {
    message: string;
    status: number;
    response: string;
    headers: { [key: string]: any; };
    result: any;

    constructor(message: string, status: number, response: string, headers: { [key: string]: any; }, result: any) {
        super();

        this.message = message;
        this.status = status;
        this.response = response;
        this.headers = headers;
        this.result = result;
    }

    protected isApiException = true;

    static isApiException(obj: any): obj is ApiException {
        return obj.isApiException === true;
    }
}

function throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): Observable<any> {
    if (result !== null && result !== undefined)
        return _observableThrow(result);
    else
        return _observableThrow(new ApiException(message, status, response, headers, null));
}

function blobToText(blob: any): Observable<string> {
    return new Observable<string>((observer: any) => {
        if (!blob) {
            observer.next("");
            observer.complete();
        } else {
            let reader = new FileReader();
            reader.onload = event => {
                observer.next((event.target as any).result);
                observer.complete();
            };
            reader.readAsText(blob);
        }
    });
}