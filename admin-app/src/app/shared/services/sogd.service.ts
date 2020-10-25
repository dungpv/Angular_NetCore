import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BaseService } from './base.service';
import { catchError, map } from 'rxjs/operators';
import { environment } from '@environments/environment';
import { Pagination, Sogd } from '../models';


@Injectable({ providedIn: 'root' })
export class SogdService extends BaseService {
    private _sharedHeaders = new HttpHeaders();
    constructor(private http: HttpClient) {
        super();
        this._sharedHeaders = this._sharedHeaders.set('Content-Type', 'application/json');
    }
    add(entity: Sogd) {
        return this.http.post(`${environment.apiUrl}/api/SoGD`, JSON.stringify(entity), { headers: this._sharedHeaders })
            .pipe(catchError(this.handleError));
    }

    update(ma: string, entity: Sogd) {
        return this.http.put(`${environment.apiUrl}/api/SoGD/${ma}`, JSON.stringify(entity), { headers: this._sharedHeaders })
            .pipe(catchError(this.handleError));
    }
    GetSoGD() {
        return this.http.get<Sogd[]>(`${environment.apiUrl}/api/SoGD`, { headers: this._sharedHeaders })
            .pipe(map((response: Sogd[]) => {
                return response;
            }), catchError(this.handleError));
    }
    getAllPaging(filter, pageIndex, pageSize) {
        return this.http.get<Pagination<Sogd>>(`${environment.apiUrl}/api/SoGD/filter?pageIndex=${pageIndex}&pageSize=${pageSize}&filter=${filter}`, { headers: this._sharedHeaders })
            .pipe(map((response: Pagination<Sogd>) => {
                return response;
            }), catchError(this.handleError));
    }
    getDetail(ma) {
        return this.http.get<Sogd>(`${environment.apiUrl}/api/SoGD/${ma}`, { headers: this._sharedHeaders })
            .pipe(catchError(this.handleError));
    }
}
