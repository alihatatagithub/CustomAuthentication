import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { catchError, filter, switchMap, take } from 'rxjs/operators';
import { AuthService } from '../services/auth.service';
import { ApiResponse, UserTokens } from '../models/auth.models';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  private isRefreshing = false;
  private readonly refreshTokenSubject = new BehaviorSubject<string | null>(null);

  constructor(private readonly authService: AuthService) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    const accessToken = this.authService.getAccessToken();

    if (accessToken) {
      request = this.addTokenHeader(request, accessToken);
    }

    return next.handle(request).pipe(
      catchError((error: unknown) => {
        if (error instanceof HttpErrorResponse && error.status === 401) {
          // If we are calling the login, register or refresh-token endpoint and get a 401, we shouldn't attempt refresh.
          if (
            request.url.includes('/Auth/login') ||
            request.url.includes('/Auth/register') ||
            request.url.includes('/Auth/refresh-token')
          ) {
            return throwError(() => error);
          }

          return this.handle401Error(request, next);
        }

        return throwError(() => error);
      })
    );
  }

  private handle401Error(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    if (!this.isRefreshing) {
      this.isRefreshing = true;
      this.refreshTokenSubject.next(null);

      return this.authService.refreshToken().pipe(
        switchMap((response: ApiResponse<UserTokens>) => {
          this.isRefreshing = false;

          if (response.isValid && response.model?.accessToken) {
            const newAccessToken = response.model.accessToken;
            this.refreshTokenSubject.next(newAccessToken);
            return next.handle(this.addTokenHeader(request, newAccessToken));
          }

          this.authService.logout();
          return throwError(() => new Error('Invalid refresh response'));
        }),
        catchError((err: unknown) => {
          this.isRefreshing = false;
          this.authService.logout();
          return throwError(() => err);
        })
      );
    }

    // Queue concurrent requests while token is refreshing
    return this.refreshTokenSubject.pipe(
      filter((token: string | null) => token !== null),
      take(1),
      switchMap((token: string | null) => {
        if (token) {
          return next.handle(this.addTokenHeader(request, token));
        }
        return throwError(() => new Error('Could not retrieve new access token'));
      })
    );
  }

  private addTokenHeader(request: HttpRequest<unknown>, token: string): HttpRequest<unknown> {
    return request.clone({
      headers: request.headers.set('Authorization', `Bearer ${token}`)
    });
  }
}
