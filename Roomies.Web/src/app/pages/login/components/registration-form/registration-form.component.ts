import { Component, Output, EventEmitter } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
  ValidatorFn,
  AbstractControl,
  ValidationErrors,
} from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-registration-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
  ],
  templateUrl: './registration-form.component.html',
  styleUrls: ['./registration-form.component.scss'],
})
export class RegistrationFormComponent {
  @Output() backToLoginClicked = new EventEmitter<void>();

  form: FormGroup;

  passwordMatchValidator: ValidatorFn = (control: AbstractControl) => {
    const password = control.get('password')?.value;
    const confirmPassword = control.get('confirmPassword')?.value;
    return password === confirmPassword ? null : { mismatch: true };
  };

  passwordComplexityValidator: ValidatorFn = (
    control: AbstractControl
  ): ValidationErrors | null => {
    const value = control.value;
    if (!value) return null;
    const hasMinLength = value.length > 8;
    const hasLower = /[a-z]/.test(value);
    const hasUpper = /[A-Z]/.test(value);
    const hasNumber = /[0-9]/.test(value);
    const hasSpecial = /[^A-Za-z0-9]/.test(value);
    return hasMinLength && hasLower && hasUpper && hasNumber && hasSpecial
      ? null
      : { passwordComplexity: true };
  };

  constructor(private fb: FormBuilder) {
    this.form = this.fb.nonNullable.group(
      {
        email: ['', [Validators.required, Validators.email]],
        password: ['', [Validators.required, this.passwordComplexityValidator]],
        confirmPassword: ['', [Validators.required]],
      },
      { validators: this.passwordMatchValidator }
    );
  }

  get email() {
    return this.form?.get('email');
  }
  get password() {
    return this.form?.get('password');
  }
  get confirmPassword() {
    return this.form?.get('confirmPassword');
  }

  onSubmit() {
    if (this.form.valid) {
      // handle registration
    } else {
      this.form.markAllAsTouched();
    }
  }
}
