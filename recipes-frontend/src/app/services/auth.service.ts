import { Injectable } from '@angular/core';
import { HttpClient, HttpParams  } from '@angular/common/http';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';
import { Observable, ReplaySubject, Subscription, throwError } from 'rxjs';
import { User } from '../models/user.model';
import { BaseResponse } from '../models/baseResponse';
import { Refresh } from '../models/refresh.model';
import { GlobalDataService } from './globalData.service';
@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  BaseURL = "https://localhost:7137/api/OAuth/"

  ClientId = "192564777707-7onrl6ghb6nidm2hlofi3377cb7uj0c7.apps.googleusercontent.com";
  ClientSecret = "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";
  OAuthServerEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";
  TokenServerEndpoint = "hhttps://oauth2.googleapis.com/token";
  RedirectUrl = "http://localhost:4200";
  emailScope = "https://www.googleapis.com/auth/userinfo.email";
  profileScope = "https://www.googleapis.com/auth/userinfo.profile";
  profileData = "https://www.googleapis.com/oauth2/v3/userinfo";

  response: any;

  constructor(private httpClient: HttpClient, private router: Router, public global: GlobalDataService) {
  }

  RedirectLogin(){
    let params = new HttpParams();

    params = params.append('client_id', this.ClientId);
    params = params.append('redirect_uri', this.RedirectUrl);
    params = params.append('response_type', "code");
    params = params.append('scope', this.emailScope + " " + this.profileScope);
      
    const urlWithParams = `${this.OAuthServerEndpoint}?${params.toString()}`;

    window.location.href = urlWithParams;
  }

  async GetTokens(code: string){
    let params = new HttpParams();

    const request = {
      device: localStorage.getItem('device')!,
      code: code
    };

    await this.httpClient.post<User>(this.BaseURL + 'AuthByCode', request).toPromise().then(
      response => {
        localStorage.setItem('accessToken', response['accessToken']);
        localStorage.setItem('refreshToken', response['refreshToken']);
        this.global.id = response['id']
        this.global.name = response['name'];
        this.global.mail = response['mail'];
      }
    );
  }

  refreshToken(): Observable<{ accessToken: string; refreshToken: string }>{
    let params = new HttpParams();

    const request = {
      refreshToken: localStorage.getItem('refreshToken')!,
      device: localStorage.getItem('device')!
    };

    if(request.refreshToken == null)
    return throwError('Cannot find user data');
    return this.httpClient.post<Refresh>(this.BaseURL + 'Refresh', request)
  }

  logout() {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
    this.global.id = null;
    this.global.name = null;
    this.global.mail = null;
    this.global.isAuth = false;
  }
}