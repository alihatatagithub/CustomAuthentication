export interface ApiResponse<T> {
  errors: string[];
  isValid: boolean;
  model: T;
}

export interface AuthResponse {
  accessToken: string;
  refreshToken: string;
  userId: string;
}

export interface UserTokens {
  accessToken: string;
  refreshToken: string;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest {
  email: string;
  phone: string;
  fullName: string;
  password: string;
}

export interface UserRole {
  roleName: string;
  roleId: number;
}
