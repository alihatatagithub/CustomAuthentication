import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import {
  ApiResponse,
  AuthResponse,
  LoginRequest,
  RegisterRequest,
  UserRole,
  UserTokens
} from '../models/auth.models';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly baseUrl = environment.apiUrl;
  private readonly ACCESS_TOKEN_KEY = 'ecommerce_access_token';
  private readonly REFRESH_TOKEN_KEY = 'ecommerce_refresh_token';
  private readonly USER_ID_KEY = 'ecommerce_user_id';

  private readonly currentUserSubject = new BehaviorSubject<AuthResponse | null>(null);
  public currentUser$ = this.currentUserSubject.asObservable();

  constructor(
    private readonly http: HttpClient,
    private readonly router: Router
  ) {
    this.loadStoredUser();
  }

  private loadStoredUser(): void {
    const accessToken = this.getAccessToken();
    const refreshToken = this.getRefreshToken();
    const userId = localStorage.getItem(this.USER_ID_KEY);

    if (accessToken && refreshToken && userId) {
      this.currentUserSubject.next({
        accessToken,
        refreshToken,
        userId
      });
    }
  }

  public get isAuthenticated(): boolean {
    return this.currentUserSubject.value !== null;
  }

  public getAccessToken(): string | null {
    return localStorage.getItem(this.ACCESS_TOKEN_KEY);
  }

  public getRefreshToken(): string | null {
    return localStorage.getItem(this.REFRESH_TOKEN_KEY);
  }

  public setTokens(tokens: UserTokens): void {
    localStorage.setItem(this.ACCESS_TOKEN_KEY, tokens.accessToken);
    localStorage.setItem(this.REFRESH_TOKEN_KEY, tokens.refreshToken);

    const currentUser = this.currentUserSubject.value;
    if (currentUser) {
      const updatedUser: AuthResponse = {
        ...currentUser,
        accessToken: tokens.accessToken,
        refreshToken: tokens.refreshToken
      };
      this.currentUserSubject.next(updatedUser);
    }
  }

  private saveAuthResponse(auth: AuthResponse): void {
    localStorage.setItem(this.ACCESS_TOKEN_KEY, auth.accessToken);
    localStorage.setItem(this.REFRESH_TOKEN_KEY, auth.refreshToken);
    localStorage.setItem(this.USER_ID_KEY, auth.userId);
    this.currentUserSubject.next(auth);
  }

  public login(credentials: LoginRequest): Observable<ApiResponse<AuthResponse>> {
    return this.http.post<ApiResponse<AuthResponse>>(`${this.baseUrl}/Auth/login`, credentials).pipe(
      tap((response: ApiResponse<AuthResponse>) => {
        if (response.isValid && response.model) {
          this.saveAuthResponse(response.model);
        }
      })
    );
  }

  public register(userData: RegisterRequest): Observable<ApiResponse<AuthResponse>> {
    return this.http.post<ApiResponse<AuthResponse>>(`${this.baseUrl}/Auth/register`, userData).pipe(
      tap((response: ApiResponse<AuthResponse>) => {
        if (response.isValid && response.model) {
          this.saveAuthResponse(response.model);
        }
      })
    );
  }

  public refreshToken(): Observable<ApiResponse<UserTokens>> {
    const accessToken = this.getAccessToken();
    const refreshToken = this.getRefreshToken();

    if (!accessToken || !refreshToken) {
      return throwError(() => new Error('No tokens available for refresh'));
    }

    const payload: UserTokens = { accessToken, refreshToken };

    return this.http.post<ApiResponse<UserTokens>>(`${this.baseUrl}/Auth/refresh-token`, payload).pipe(
      tap((response: ApiResponse<UserTokens>) => {
        if (response.isValid && response.model) {
          this.setTokens(response.model);
        } else {
          this.logout();
        }
      }),
      catchError((error: unknown) => {
        this.logout();
        return throwError(() => error);
      })
    );
  }

  public getUserDetails(): Observable<UserRole[]> {
    return this.http.get<UserRole[]>(`${this.baseUrl}/Auth/user-details`);
  }

  public logout(): void {
    localStorage.removeItem(this.ACCESS_TOKEN_KEY);
    localStorage.removeItem(this.REFRESH_TOKEN_KEY);
    localStorage.removeItem(this.USER_ID_KEY);
    this.currentUserSubject.next(null);
    this.router.navigate(['/auth/login']);
  }
}
