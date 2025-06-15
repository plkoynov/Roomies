import { Component } from '@angular/core';
import { LoginFormComponent } from './components/login-form/login-form.component';
import { MatCardModule } from '@angular/material/card';

@Component({
  selector: 'app-login-page',
  standalone: true,
  imports: [LoginFormComponent, MatCardModule],
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss'],
})
export class LoginPageComponent {}
