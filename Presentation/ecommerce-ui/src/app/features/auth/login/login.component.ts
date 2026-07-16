import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { LoginRequest } from '../../../core/models/auth.models';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  loading = false;
  errorMessage: string | null = null;
  showPassword = false;

  constructor(
    private readonly fb: FormBuilder,
    private readonly authService: AuthService,
    private readonly router: Router
  ) {}

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  togglePasswordVisibility(): void {
    this.showPassword = !this.showPassword;
  }

  onSubmit(): void {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    this.loading = true;
    this.errorMessage = null;

    const requestData: LoginRequest = this.loginForm.value as LoginRequest;

    this.authService.login(requestData).subscribe({
      next: (response) => {
        this.loading = false;
        if (response.isValid) {
          this.router.navigate(['/dashboard']);
        } else {
          this.errorMessage = response.errors.join(', ') || 'Login failed. Please try again.';
        }
      },
      error: (err: unknown) => {
        this.loading = false;
        this.errorMessage = 'An error occurred during login. Please try again later.';
        console.error(err);
      }
    });
  }
}
