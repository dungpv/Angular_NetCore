import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BaseService } from './base.service';
import { catchError, map } from 'rxjs/operators';
import { environment } from '@environments/environment';
import { Pagination, Truong } from '../models';


@Injectable({ providedIn: 'root' })
export class TruongService extends BaseService {
    private _sharedHeaders = new HttpHeaders();
    constructor(private http: HttpClient) {
        super();
        this._sharedHeaders = this._sharedHeaders.set('Content-Type', 'application/json');
    }
    add(entity: Truong) {
        return this.http.post(`${environment.apiUrl}/api/Truong`, JSON.stringify(entity), { headers: this._sharedHeaders })
            .pipe(catchError(this.handleError));
    }

    update(id: number, entity: Truong) {
        return this.http.put(`${environment.apiUrl}/api/Truong/${id}`, JSON.stringify(entity), { headers: this._sharedHeaders })
            .pipe(catchError(this.handleError));
    }
    delete(id) {
        return this.http.delete(environment.apiUrl + '/api/Truong/' + id, { headers: this._sharedHeaders })
            .pipe(
                catchError(this.handleError)
            );
    }
    getAllPaging(filter, pageIndex, pageSize, maSoGD, maCapHoc) {
        return this.http.get<Pagination<Truong>>(`${environment.apiUrl}/api/Truong/filter?pageIndex=${pageIndex}&pageSize=${pageSize}&filter=${filter}&maSoGD=${maSoGD}
            &maNamHoc=2019 &maCapHoc=${maCapHoc}`, { headers: this._sharedHeaders })
            .pipe(map((response: Pagination<Truong>) => {
                return response;
            }), catchError(this.handleError));
    }
    getDetail(id) {
        return this.http.get<Truong>(`${environment.apiUrl}/api/Truong/${id}`, { headers: this._sharedHeaders })
            .pipe(catchError(this.handleError));
    }
    getTruongBySoGDByNamHoc(maSoGD, maCapHoc) {
        return this.http.get<Truong[]>(`${environment.apiUrl}/api/Truong/maSoGD=${maSoGD}
            &maNamHoc=2019&maCapHoc=${maCapHoc}`, { headers: this._sharedHeaders })
            .pipe(map((response: Truong[]) => {
                return response;
            }), catchError(this.handleError));
    }
}
