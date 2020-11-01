import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BaseService } from './base.service';
import { catchError, map } from 'rxjs/operators';
import { environment } from '@environments/environment';
import { DMTinh, DMCumThiDua, DMHuyen, BaseDanhMuc, DMNhomCapHoc } from '../models';
import { SystemConstants } from '@app/shared/constants';

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
    getDMTinhByMa(ma) {
        return this.http.get<DMTinh>(`${environment.apiUrl}/api/DMTinh/${ma}`, { headers: this._sharedHeaders })
            .pipe(catchError(this.handleError));
    }
    getDMCumThiDua() {
        return this.http.get<DMCumThiDua[]>(`${environment.apiUrl}/api/DMCumThiDua`, { headers: this._sharedHeaders })
            .pipe(map((response: DMCumThiDua[]) => {
                return response;
            }), catchError(this.handleError));
    }
    getDMCumThiDuaByMa(ma) {
        return this.http.get<DMCumThiDua>(`${environment.apiUrl}/api/DMCumThiDua/${ma}`, { headers: this._sharedHeaders })
            .pipe(catchError(this.handleError));
    }
    getDMHuyen(maTinh) {
        return this.http.get<DMHuyen[]>(`${environment.apiUrl}/api/DMHuyen?maTinh=${maTinh}&maNamHoc=${SystemConstants.MA_NAM_HOC}`, { headers: this._sharedHeaders })
            .pipe(map((response: DMHuyen[]) => {
                return response;
            }), catchError(this.handleError));
    }
    getDMHuyenByMa(ma) {
        return this.http.get<DMHuyen>(`${environment.apiUrl}/api/DMHuyen/filter?ma=${ma}&maNamHoc=${SystemConstants.MA_NAM_HOC}`, { headers: this._sharedHeaders })
            .pipe(catchError(this.handleError));
    }
    getDMHuyenAll() {
        return this.http.get<DMHuyen[]>(`${environment.apiUrl}/api/DMHuyen?maNamHoc=${SystemConstants.MA_NAM_HOC}`, { headers: this._sharedHeaders })
            .pipe(map((response: DMHuyen[]) => {
                return response;
            }), catchError(this.handleError));
    }

    getDMCapHoc() {
        return this.http.get<BaseDanhMuc[]>(`${environment.apiUrl}/api/DMCapHoc`, { headers: this._sharedHeaders })
            .pipe(map((response: BaseDanhMuc[]) => {
                return response;
            }), catchError(this.handleError));
    }
    getDMCapHocByMa(ma) {
        return this.http.get<BaseDanhMuc>(`${environment.apiUrl}/api/DMCapHoc/${ma}`, { headers: this._sharedHeaders })
            .pipe(catchError(this.handleError));
    }
    getDMLoaiHinh(maCapHoc) {
        return this.http.get<BaseDanhMuc[]>(`${environment.apiUrl}/api/DMLoaiHinh?maNamHoc=${SystemConstants.MA_NAM_HOC}&maCapHoc=${maCapHoc}`, { headers: this._sharedHeaders })
            .pipe(map((response: BaseDanhMuc[]) => {
                return response;
            }), catchError(this.handleError));
    }
    getDMLoaiHinhByMa(ma) {
        return this.http.get<BaseDanhMuc>(`${environment.apiUrl}/api/DMLoaiHinh/${ma}`, { headers: this._sharedHeaders })
            .pipe(catchError(this.handleError));
    }
    getDMLoaiTruong(maCapHoc) {
        return this.http.get<BaseDanhMuc[]>(`${environment.apiUrl}/api/DMLoaiTruong?maCapHoc=${maCapHoc}`, { headers: this._sharedHeaders })
            .pipe(map((response: BaseDanhMuc[]) => {
                return response;
            }), catchError(this.handleError));
    }
    getDMLoaiTruongByMa(ma) {
        return this.http.get<BaseDanhMuc>(`${environment.apiUrl}/api/DMLoaiTruong/${ma}`, { headers: this._sharedHeaders })
            .pipe(catchError(this.handleError));
    }
    getDMNhomCapHoc(maCapHoc) {
        return this.http.get<DMNhomCapHoc[]>(`${environment.apiUrl}/api/DMNhomCapHoc?maCapHoc=${maCapHoc}`, { headers: this._sharedHeaders })
            .pipe(map((response: DMNhomCapHoc[]) => {
                return response;
            }), catchError(this.handleError));
    }
    getDMNhomCapHocByMa(ma) {
        return this.http.get<DMNhomCapHoc>(`${environment.apiUrl}/api/DMNhomCapHoc/${ma}`, { headers: this._sharedHeaders })
            .pipe(catchError(this.handleError));
    }
}
