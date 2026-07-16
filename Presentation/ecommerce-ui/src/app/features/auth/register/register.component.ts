import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { RegisterRequest } from '../../../core/models/auth.models';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm!: FormGroup;
  loading = false;
  errorMessage: string | null = null;
  showPassword = false;

  constructor(
    private readonly fb: FormBuilder,
    private readonly authService: AuthService,
    private readonly router: Router
  ) {}

  ngOnInit(): void {
    this.registerForm = this.fb.group({
      fullName: ['', [Validators.required, Validators.minLength(2)]],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', [Validators.required, Validators.pattern(/^[0-9+\-\s()]{8,20}$/)]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  togglePasswordVisibility(): void {
    this.showPassword = !this.showPassword;
  }

  onSubmit(): void {
    if (this.registerForm.invalid) {
      this.registerForm.markAllAsTouched();
      return;
    }

    this.loading = true;
    this.errorMessage = null;

    const requestData: RegisterRequest = this.registerForm.value as RegisterRequest;

    this.authService.register(requestData).subscribe({
      next: (response) => {
        this.loading = false;
        if (response.isValid) {
          this.router.navigate(['/dashboard']);
        } else {
          this.errorMessage = response.errors.join(', ') || 'Registration failed. Please try again.';
        }
      },
      error: (err: unknown) => {
        this.loading = false;
        this.errorMessage = 'An error occurred during registration. Please try again later.';
        console.error(err);
      }
    });
  }
}
