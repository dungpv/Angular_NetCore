import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BaseService } from './base.service';
import { catchError, map } from 'rxjs/operators';
import { environment } from '@environments/environment';
import { Pagination, Lop } from '../models';


@Injectable({ providedIn: 'root' })
export class LopService extends BaseService {
    private _sharedHeaders = new HttpHeaders();
    constructor(private http: HttpClient) {
        super();
        this._sharedHeaders = this._sharedHeaders.set('Content-Type', 'application/json');
    }
    add(entity: Lop) {
        console.log(JSON.stringify(entity));
        return this.http.post(`${environment.apiUrl}/api/Lop`, JSON.stringify(entity), { headers: this._sharedHeaders })
            .pipe(catchError(this.handleError));
    }

    update(id: number, entity: Lop) {
        return this.http.put(`${environment.apiUrl}/api/Lop/${id}`, JSON.stringify(entity), { headers: this._sharedHeaders })
            .pipe(catchError(this.handleError));
    }
    delete(id) {
        return this.http.delete(environment.apiUrl + '/api/Lop/' + id, { headers: this._sharedHeaders })
            .pipe(
                catchError(this.handleError)
            );
    }
    getAllPaging(filter, pageIndex, pageSize, maSoGD, maTruong, maCapHoc) {
        return this.http.get<Pagination<Lop>>(`${environment.apiUrl}/api/Lop/filter?pageIndex=${pageIndex}&pageSize=${pageSize}&filter=${filter}&maSoGD=${maSoGD}
            &maNamHoc=2019&maTruong=${maTruong}&maCapHoc=${maCapHoc}`, { headers: this._sharedHeaders })
            .pipe(map((response: Pagination<Lop>) => {
                return response;
            }), catchError(this.handleError));
    }
    getDetail(id) {
        return this.http.get<Lop>(`${environment.apiUrl}/api/Lop/${id}`, { headers: this._sharedHeaders })
            .pipe(catchError(this.handleError));
    }
}
