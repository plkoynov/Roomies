import { Component } from '@angular/core';
import { LoginFormComponent } from './components/login-form/login-form.component';
import { RegistrationFormComponent } from './components/registration-form/registration-form.component';
import { MatCardModule } from '@angular/material/card';
import { CommonModule } from '@angular/common';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { AuthService } from '../../core/services/auth.service';
import { LoadingService } from '../../core/services/loading.service';
import { LoginRequest } from '../../core/models/login-request.model';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-login-page',
  standalone: true,
  imports: [
    CommonModule,
    LoginFormComponent,
    RegistrationFormComponent,
    MatCardModule,
    MatProgressBarModule,
  ],
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss'],
})
export class LoginPageComponent {
  showRegistration = false;
  isLoading = false;

  constructor(
    private authService: AuthService,
    private loadingService: LoadingService
  ) {}

  showRegisterForm() {
    this.showRegistration = true;
  }

  showLoginForm() {
    this.showRegistration = false;
  }

  handleLoginSubmit = (form: FormGroup) => {
    this.loadingService.show();
    form.disable();
    const request: LoginRequest = {
      email: form.get('email')?.value,
      password: form.get('password')?.value,
    };
    this.authService.login(request).subscribe({
      next: (_res: any) => {
        this.loadingService.hide();
        form.enable();
        // handle successful login (e.g., redirect, store token, etc.)
      },
      error: (_err: any) => {
        this.loadingService.hide();
        form.enable();
        // handle error (e.g., show error message)
      },
    });
  };
}
