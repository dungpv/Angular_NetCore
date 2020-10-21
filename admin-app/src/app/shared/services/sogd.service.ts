import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BaseService } from './base.service';
import { catchError, map } from 'rxjs/operators';
import { environment } from '@environments/environment';
import { Sogd } from '../models';


@Injectable({ providedIn: 'root' })
export class SogdService extends BaseService {
    private _sharedHeaders = new HttpHeaders();
    constructor(private http: HttpClient) {
        super();
        this._sharedHeaders = this._sharedHeaders.set('Content-Type', 'application/json');
    }

    GetSoGD() {
        return this.http.get<Sogd[]>(`${environment.apiUrl}/api/SoGD`, { headers: this._sharedHeaders })
            .pipe(map((response: Sogd[]) => {
                return response;
            }), catchError(this.handleError));
    }
}
