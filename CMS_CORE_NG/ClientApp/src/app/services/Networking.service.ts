/* tslint:disable */
/* eslint-disable */
//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.9.4.0 (NJsonSchema v10.3.1.0 (Newtonsoft.Json v12.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------
// ReSharper disable InconsistentNaming

import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpResponseBase } from '@angular/common/http';

export const BASE_URL = new InjectionToken<string>('BASE_URL');

export interface INetworkingService {
    createNewNetworkingGroup(model: NetworkingGroupVM | null): Observable<number>;
    getAllGroups(): Observable<NetworkingGroupVM[] | null>;
    getAllGroupsForUser(buisnessId: number): Observable<NetworkingGroupVM[] | null>;
    getGroupById(groupId: number): Observable<NetworkingGroupBusinessVM[] | null>;
    freezingGroup(groupId: number): Observable<boolean>;
    updateGroup(model: NetworkingGroupVM | null): Observable<boolean>;
    addBuisnessToGroup(model: NetworkingGroupBusinessVM | null): Observable<number>;
    deleteBuisnessFromGroup(buisnessId: number): Observable<boolean>;
    updateBuisnessFromGroup(model: NetworkingGroupBusinessVM | null): Observable<boolean>;
}

@Injectable()
export class NetworkingService implements INetworkingService {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
    }

    createNewNetworkingGroup(model: NetworkingGroupVM | null): Observable<number> {
        let url_ = this.baseUrl + "/api/Networking/CreateNewNetworkingGroup";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(model);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
                "Accept": "application/json"
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processCreateNewNetworkingGroup(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processCreateNewNetworkingGroup(<any>response_);
                } catch (e) {
                    return <Observable<number>><any>_observableThrow(e);
                }
            } else
                return <Observable<number>><any>_observableThrow(response_);
        }));
    }

    protected processCreateNewNetworkingGroup(response: HttpResponseBase): Observable<number> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            result200 = _responseText === "" ? null : <number>JSON.parse(_responseText, this.jsonParseReviver);
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<number>(<any>null);
    }

    getAllGroups(): Observable<NetworkingGroupVM[] | null> {
        let url_ = this.baseUrl + "/api/Networking/getAllGroups";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processGetAllGroups(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processGetAllGroups(<any>response_);
                } catch (e) {
                    return <Observable<NetworkingGroupVM[] | null>><any>_observableThrow(e);
                }
            } else
                return <Observable<NetworkingGroupVM[] | null>><any>_observableThrow(response_);
        }));
    }

    protected processGetAllGroups(response: HttpResponseBase): Observable<NetworkingGroupVM[] | null> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            result200 = _responseText === "" ? null : <NetworkingGroupVM[]>JSON.parse(_responseText, this.jsonParseReviver);
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<NetworkingGroupVM[] | null>(<any>null);
    }

    getAllGroupsForUser(buisnessId: number): Observable<NetworkingGroupVM[] | null> {
        let url_ = this.baseUrl + "/api/Networking/getAllGroupsForUser?";
        if (buisnessId === undefined || buisnessId === null)
            throw new Error("The parameter 'buisnessId' must be defined and cannot be null.");
        else
            url_ += "buisnessId=" + encodeURIComponent("" + buisnessId) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processGetAllGroupsForUser(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processGetAllGroupsForUser(<any>response_);
                } catch (e) {
                    return <Observable<NetworkingGroupVM[] | null>><any>_observableThrow(e);
                }
            } else
                return <Observable<NetworkingGroupVM[] | null>><any>_observableThrow(response_);
        }));
    }

    protected processGetAllGroupsForUser(response: HttpResponseBase): Observable<NetworkingGroupVM[] | null> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            result200 = _responseText === "" ? null : <NetworkingGroupVM[]>JSON.parse(_responseText, this.jsonParseReviver);
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<NetworkingGroupVM[] | null>(<any>null);
    }

    getGroupById(groupId: number): Observable<NetworkingGroupBusinessVM[] | null> {
        let url_ = this.baseUrl + "/api/Networking/getGroupById?";
        if (groupId === undefined || groupId === null)
            throw new Error("The parameter 'groupId' must be defined and cannot be null.");
        else
            url_ += "groupId=" + encodeURIComponent("" + groupId) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processGetGroupById(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processGetGroupById(<any>response_);
                } catch (e) {
                    return <Observable<NetworkingGroupBusinessVM[] | null>><any>_observableThrow(e);
                }
            } else
                return <Observable<NetworkingGroupBusinessVM[] | null>><any>_observableThrow(response_);
        }));
    }

    protected processGetGroupById(response: HttpResponseBase): Observable<NetworkingGroupBusinessVM[] | null> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            result200 = _responseText === "" ? null : <NetworkingGroupBusinessVM[]>JSON.parse(_responseText, this.jsonParseReviver);
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<NetworkingGroupBusinessVM[] | null>(<any>null);
    }

    freezingGroup(groupId: number): Observable<boolean> {
        let url_ = this.baseUrl + "/api/Networking/FreezingGroup?";
        if (groupId === undefined || groupId === null)
            throw new Error("The parameter 'groupId' must be defined and cannot be null.");
        else
            url_ += "groupId=" + encodeURIComponent("" + groupId) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processFreezingGroup(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processFreezingGroup(<any>response_);
                } catch (e) {
                    return <Observable<boolean>><any>_observableThrow(e);
                }
            } else
                return <Observable<boolean>><any>_observableThrow(response_);
        }));
    }

    protected processFreezingGroup(response: HttpResponseBase): Observable<boolean> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            result200 = _responseText === "" ? null : <boolean>JSON.parse(_responseText, this.jsonParseReviver);
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<boolean>(<any>null);
    }

    updateGroup(model: NetworkingGroupVM | null): Observable<boolean> {
        let url_ = this.baseUrl + "/api/Networking/UpdateGroup";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(model);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
                "Accept": "application/json"
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processUpdateGroup(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processUpdateGroup(<any>response_);
                } catch (e) {
                    return <Observable<boolean>><any>_observableThrow(e);
                }
            } else
                return <Observable<boolean>><any>_observableThrow(response_);
        }));
    }

    protected processUpdateGroup(response: HttpResponseBase): Observable<boolean> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            result200 = _responseText === "" ? null : <boolean>JSON.parse(_responseText, this.jsonParseReviver);
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<boolean>(<any>null);
    }

    addBuisnessToGroup(model: NetworkingGroupBusinessVM | null): Observable<number> {
        let url_ = this.baseUrl + "/api/Networking/AddBuisnessToGroup";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(model);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
                "Accept": "application/json"
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processAddBuisnessToGroup(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processAddBuisnessToGroup(<any>response_);
                } catch (e) {
                    return <Observable<number>><any>_observableThrow(e);
                }
            } else
                return <Observable<number>><any>_observableThrow(response_);
        }));
    }

    protected processAddBuisnessToGroup(response: HttpResponseBase): Observable<number> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            result200 = _responseText === "" ? null : <number>JSON.parse(_responseText, this.jsonParseReviver);
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<number>(<any>null);
    }

    deleteBuisnessFromGroup(buisnessId: number): Observable<boolean> {
        let url_ = this.baseUrl + "/api/Networking/DeleteBuisnessFromGroup?";
        if (buisnessId === undefined || buisnessId === null)
            throw new Error("The parameter 'buisnessId' must be defined and cannot be null.");
        else
            url_ += "buisnessId=" + encodeURIComponent("" + buisnessId) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processDeleteBuisnessFromGroup(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processDeleteBuisnessFromGroup(<any>response_);
                } catch (e) {
                    return <Observable<boolean>><any>_observableThrow(e);
                }
            } else
                return <Observable<boolean>><any>_observableThrow(response_);
        }));
    }

    protected processDeleteBuisnessFromGroup(response: HttpResponseBase): Observable<boolean> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            result200 = _responseText === "" ? null : <boolean>JSON.parse(_responseText, this.jsonParseReviver);
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<boolean>(<any>null);
    }

    updateBuisnessFromGroup(model: NetworkingGroupBusinessVM | null): Observable<boolean> {
        let url_ = this.baseUrl + "/api/Networking/UpdateBuisnessFromGroup";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(model);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
                "Accept": "application/json"
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processUpdateBuisnessFromGroup(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processUpdateBuisnessFromGroup(<any>response_);
                } catch (e) {
                    return <Observable<boolean>><any>_observableThrow(e);
                }
            } else
                return <Observable<boolean>><any>_observableThrow(response_);
        }));
    }

    protected processUpdateBuisnessFromGroup(response: HttpResponseBase): Observable<boolean> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            result200 = _responseText === "" ? null : <boolean>JSON.parse(_responseText, this.jsonParseReviver);
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<boolean>(<any>null);
    }
}

export interface NetworkingGroupVM {
    Id: number;
    GroupName?: string | undefined;
    ManagerBusinessId?: number | undefined;
    ManagerBusinessName?: string | undefined;
    ManagerBusinessEmail?: string | undefined;
    Description?: string | undefined;
    City?: string | undefined;
    AreaId?: number | undefined;
    CreationDate: Date;
    IsActive?: boolean | undefined;
    ManagerBusiness?: BusinessForCardVM | undefined;
    Area?: AreaVm | undefined;
}

export interface BusinessForCardVM {
    id: number;
    userId?: string | undefined;
    buisnessName?: string | undefined;
    businessEmailAddress?: string | undefined;
    actionDiscription?: string | undefined;
    isdisplayBusinessOwnerName?: boolean | undefined;
    ispayingBuisness?: boolean | undefined;
    isburterBuisness?: boolean | undefined;
    iscollaborationBuisness?: boolean | undefined;
    logoPictureId?: string | undefined;
    listOfAll4buisnessCategory?: BuisnessCategoryVm[] | undefined;
    index?: number | undefined;
    barterProduct1?: string | undefined;
    barterProduct2?: string | undefined;
    lastupdatedStartDate?: Date | undefined;
    ownerName?: string | undefined;
    Score?: number | undefined;
    buisnessCategory1?: BuisnessCategoryVm[] | undefined;
    buisnessCategory2?: BuisnessCategoryVm[] | undefined;
    buisnessCategory3?: BuisnessCategoryVm[] | undefined;
    buisnessCategory4?: BuisnessCategoryVm[] | undefined;
    buisnessAreaList1?: BuisnessAreaVm[] | undefined;
}

export interface BuisnessCategoryVm {
    businessId?: number | undefined;
    categoryId: number;
    subCategoryId: number;
    combinationtId: number;
    isPossibleInBarter: boolean;
    categoryName?: string | undefined;
    subCategoryName?: string | undefined;
}

export interface BuisnessAreaVm {
    id?: number | undefined;
    buisnessId?: number | undefined;
    areaId: number;
}

export interface AreaVm {
    id: number;
    name?: string | undefined;
}

export interface NetworkingGroupBusinessVM {
    Id: number;
    BusinessId?: number | undefined;
    buisnessName?: string | undefined;
    GroupId?: number | undefined;
    Role?: string | undefined;
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
                observer.next((<any>event.target).result);
                observer.complete();
            };
            reader.readAsText(blob);
        }
    });
}