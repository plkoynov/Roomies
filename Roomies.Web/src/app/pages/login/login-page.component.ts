import { Component } from '@angular/core';
import { LoginFormComponent } from './components/login-form/login-form.component';
import { RegistrationFormComponent } from './components/registration-form/registration-form.component';
import { MatCardModule } from '@angular/material/card';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login-page',
  standalone: true,
  imports: [
    CommonModule,
    LoginFormComponent,
    RegistrationFormComponent,
    MatCardModule,
  ],
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss'],
})
export class LoginPageComponent {
  showRegistration = false;

  showRegisterForm() {
    this.showRegistration = true;
  }

  showLoginForm() {
    this.showRegistration = false;
  }
}
