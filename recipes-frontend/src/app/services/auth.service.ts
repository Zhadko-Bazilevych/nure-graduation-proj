import { Injectable } from '@angular/core';
import { HttpClient, HttpParams  } from '@angular/common/http';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';
import { Observable, ReplaySubject, Subscription } from 'rxjs';
import { User } from '../models/user.model';
import { BaseResponse } from '../models/baseResponse';
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

  constructor(private httpClient: HttpClient, private router: Router) {
  }

  logout() {
    localStorage.removeItem('auth_token');
    localStorage.removeItem('role');
    localStorage.removeItem('id');
    localStorage.removeItem('mail');
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

  GetTokens(code: string){

    let params = new HttpParams();

    const request = {
      device: localStorage.getItem('device')!,
      code: code
    };

    this.httpClient.post<User>(this.BaseURL + 'AuthByCode', request).subscribe(
      response => {
        localStorage.setItem('name', response['name'])
        localStorage.setItem('mail', response['mail'])
        localStorage.setItem('accessToken', response['accessToken'])
        localStorage.setItem('refreshToken', response['refreshToken'])
      }

    )
  }

}