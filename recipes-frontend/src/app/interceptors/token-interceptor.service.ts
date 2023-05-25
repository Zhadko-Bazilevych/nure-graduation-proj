import { Injectable } from '@angular/core';
import { catchError, switchMap } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { AuthenticationService } from 'src/app/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class TokenInterceptorService implements HttpInterceptor {
  constructor(private authService: AuthenticationService) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const access = localStorage.getItem('accessToken');
    if (access) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${access}`,
        },
      });
    }
    return next.handle(request).pipe(
      catchError((err) => {
        if (err.status === 401) {
          return this.authService.refreshToken().pipe(
            switchMap((response) => {
              const newAccessToken = response.accessToken;
              const newRefreshToken = response.refreshToken;
              localStorage.setItem('accessToken', newAccessToken);
              localStorage.setItem('refreshToken', newRefreshToken);
              // Clone the original request with the new access token
              const newRequest = request.clone({
                setHeaders: {
                  Authorization: `Bearer ${newAccessToken}`,
                },
              });
              // Retry the request with the new access token
              return next.handle(newRequest) as Observable<HttpEvent<any>>;
            }),
            catchError((error) => {
              // Handle refresh token request error
              this.authService.logout();
              return throwError('Unable to refresh token. Please log in again.') as Observable<HttpEvent<any>>;
            })
          ) as Observable<HttpEvent<any>>;
        }
        const error = err.error.message || err.statusText;
        return throwError(error) as Observable<HttpEvent<any>>;
      })
    );
  }
}