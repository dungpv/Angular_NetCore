import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BaseService } from './base.service';
import { catchError, map } from 'rxjs/operators';
import { environment } from '@environments/environment';
import { DMTinh, DMCumThiDua } from '../models';


@Injectable({ providedIn: 'root' })
export class DanhMucService extends BaseService {
    private _sharedHeaders = new HttpHeaders();
    constructor(private http: HttpClient) {
        super();
        this._sharedHeaders = this._sharedHeaders.set('Content-Type', 'application/json');
    }

    getDMTinh() {
        return this.http.get<DMTinh[]>(`${environment.apiUrl}/api/DMTinh`, { headers: this._sharedHeaders })
            .pipe(map((response: DMTinh[]) => {
                return response;
            }), catchError(this.handleError));
    }
    getDMCumThiDua() {
        return this.http.get<DMCumThiDua[]>(`${environment.apiUrl}/api/DMCumThiDua`, { headers: this._sharedHeaders })
            .pipe(map((response: DMCumThiDua[]) => {
                return response;
            }), catchError(this.handleError));
    }
}
