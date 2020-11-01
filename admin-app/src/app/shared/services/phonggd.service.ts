import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BaseService } from './base.service';
import { catchError, map } from 'rxjs/operators';
import { environment } from '@environments/environment';
import { Pagination, Phonggd } from '../models';


@Injectable({ providedIn: 'root' })
export class PhonggdService extends BaseService {
    private _sharedHeaders = new HttpHeaders();
    constructor(private http: HttpClient) {
        super();
        this._sharedHeaders = this._sharedHeaders.set('Content-Type', 'application/json');
    }
    add(entity: Phonggd) {
        return this.http.post(`${environment.apiUrl}/api/Phonggd`, JSON.stringify(entity), { headers: this._sharedHeaders })
            .pipe(catchError(this.handleError));
    }

    update(id: number, entity: Phonggd) {
        return this.http.put(`${environment.apiUrl}/api/Phonggd/${id}`, JSON.stringify(entity), { headers: this._sharedHeaders })
            .pipe(catchError(this.handleError));
    }
    delete(id) {
        return this.http.delete(environment.apiUrl + '/api/Phonggd/' + id, { headers: this._sharedHeaders })
            .pipe(
                catchError(this.handleError)
            );
    }
    getAllPaging(filter, pageIndex, pageSize, maSoGD) {
        return this.http.get<Pagination<Phonggd>>(`${environment.apiUrl}/api/Phonggd/filter?pageIndex=${pageIndex}&pageSize=${pageSize}&filter=${filter}&maSoGD=${maSoGD}&maNamHoc=2019`, { headers: this._sharedHeaders })
            .pipe(map((response: Pagination<Phonggd>) => {
                return response;
            }), catchError(this.handleError));
    }
    getDetail(id) {
        return this.http.get<Phonggd>(`${environment.apiUrl}/api/Phonggd/${id}`, { headers: this._sharedHeaders })
            .pipe(catchError(this.handleError));
    }
}
