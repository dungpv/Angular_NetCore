import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BaseService } from './base.service';
import { catchError } from 'rxjs/operators';
import { Function } from '../models';
import { environment } from '@environments/environment';

@Injectable({ providedIn: 'root' })

export class FunctionsService extends BaseService {
    constructor(private http: HttpClient) {
        super();

    }
}
