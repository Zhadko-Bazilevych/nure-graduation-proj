import { Injectable } from '@angular/core';
import { HttpClient, HttpParams  } from '@angular/common/http';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';
import { Observable, ReplaySubject, Subscription } from 'rxjs';
import { User } from '../models/user.model';
import { BaseResponse } from '../models/baseResponse';
import { Refresh } from '../models/refresh.model';

@Injectable({
  providedIn: 'root'
})
export class RandomService {
  BaseURL = "https://localhost:7137/api/Main/Register"

  response: any;

  constructor(private httpClient: HttpClient, private router: Router) {
  }

  random(code: Number){

    let params = new HttpParams();

    params = params.append('request', code.toString());
    
    this.httpClient.post<number>(`${this.BaseURL}?${params.toString()}`, { }).subscribe(
      response => {
        console.log(response)
      }

    )
  }
}