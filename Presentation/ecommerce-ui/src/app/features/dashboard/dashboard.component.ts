import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../core/services/auth.service';
import { UserRole } from '../../core/models/auth.models';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  userRoles: UserRole[] = [];
  loading = true;
  error: string | null = null;
  userId: string | null = null;
  accessToken: string | null = null;
  refreshToken: string | null = null;
  refreshing = false;
  refreshSuccess = false;

  constructor(private readonly authService: AuthService) {}

  ngOnInit(): void {
    this.loadTokenInfo();
    this.fetchUserDetails();
  }

  private loadTokenInfo(): void {
    this.userId = localStorage.getItem('ecommerce_user_id');
    this.accessToken = this.authService.getAccessToken();
    this.refreshToken = this.authService.getRefreshToken();
  }

  fetchUserDetails(): void {
    this.loading = true;
    this.error = null;

    this.authService.getUserDetails().subscribe({
      next: (roles) => {
        this.userRoles = roles;
        this.loading = false;
      },
      error: (err: unknown) => {
        this.error = 'Failed to load user details. Your session may have expired.';
        this.loading = false;
        console.error(err);
      }
    });
  }

  triggerManualRefresh(): void {
    this.refreshing = true;
    this.refreshSuccess = false;
    this.error = null;

    this.authService.refreshToken().subscribe({
      next: (response) => {
        this.refreshing = false;
        if (response.isValid) {
          this.refreshSuccess = true;
          this.loadTokenInfo();
          setTimeout(() => this.refreshSuccess = false, 3000);
        } else {
          this.error = response.errors.join(', ') || 'Failed to refresh token';
        }
      },
      error: (err: unknown) => {
        this.refreshing = false;
        this.error = 'An error occurred during token refresh.';
        console.error(err);
      }
    });
  }

  logout(): void {
    this.authService.logout();
  }
}
