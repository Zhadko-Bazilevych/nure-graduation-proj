import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';
import { Observable, ReplaySubject, Subscription } from 'rxjs';
import { User } from '../models/user.model';
import { BaseResponse } from '../models/baseResponse';
@Injectable({
  providedIn: 'root'
})
export class AuthenticationServiceOld {
  BaseRoute = "https://localhost:44332/api/Account";
  private userSubject = new ReplaySubject<User | null>;
  public user$ = this.userSubject.asObservable();

  constructor(private httpClient: HttpClient, private router: Router) {
    var token = localStorage.getItem('auth_token');
    var role = localStorage.getItem('role')
    var id = localStorage.getItem('id');
    var mail = localStorage.getItem('mail');

    if (token && role) {
      const user = {
        Token: token,
        Role: role,
        Id: id,
        Mail: mail
      } as User;

      this.userSubject.next(user);
    }
  }

  rxSubscriptions: Subscription[] = [];
  user: User | null;
  Local() {
    const sub = this.user$.subscribe(user => { this.user = user;});
    this.rxSubscriptions.push(sub);
    this.rxSubscriptions.forEach(x => x.unsubscribe());
    return this.user
  }

  logout() {
    localStorage.removeItem('auth_token');
    localStorage.removeItem('role');
    localStorage.removeItem('id');
    localStorage.removeItem('mail');
    this.userSubject.next(null);
  }



  Login(email: string, password: string, rememberMe: boolean): Observable<any> {
    const request = {
      email: email,
      password: password
    };
    const route = `${this.BaseRoute}/Login`;

    return this.httpClient.post<User>(route, request).pipe(map((resp: any) => {
      if (resp.code == 200) {
        if (rememberMe) {
          localStorage.setItem('auth_token', resp.token);
          localStorage.setItem('role', resp.role);
          localStorage.setItem('id', resp.id);
          localStorage.setItem('mail', resp.mail);
        }
        const user = {
          Token: resp.token,
          Role: resp.role,
          Id: resp.id,
          Mail: resp.mail
        } as User;
        
        this.userSubject.next(user);
      }
      return resp;
    }
    )
    )
  }

  Register(email: string, password: string, confirmPassword: string, phone: string) {
    const request = {
      email: email,
      password: password,
      confirmPassword: confirmPassword,
      phone: phone
    };
    const route = `${this.BaseRoute}/Register`;
    return this.httpClient.post<any>(route, request)
  }

}