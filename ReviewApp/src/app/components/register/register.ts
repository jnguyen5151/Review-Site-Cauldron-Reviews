import { Component, signal, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Field, form } from '@angular/forms/signals';
import { FormBuilder, FormGroup, FormControl, ReactiveFormsModule } from '@angular/forms';

import { AuthService } from '../../services/auth-service';
import { registerDefaults, registerValidators, passwordValidator } from './register-form.config';

@Component({
  selector: 'app-register',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {

  private fb = inject(FormBuilder);
  private authService = inject(AuthService);

  registerForm: FormGroup = this.fb.group({
    userName: [
      registerDefaults.userName,
      registerValidators.userName
    ],
    email: [
      registerDefaults.email,
      registerValidators.email
    ],
    password: [
      registerDefaults.password,
      registerValidators.password
    ]
  });

  get passwordMessages(): string[] {
    const errors = this.registerForm.get('password')?.errors;
    return errors?.['messages'] ?? [];
  }

  register() {

    if (this.registerForm.invalid) {
      console.log('invalid form');
      this.registerForm.markAllAsTouched();
      return;
    }

    const dto = this.registerForm.value;
    console.log(dto);

    this.authService.register(dto).subscribe({
      next: () => {
        console.log('Account Successfully Created');
      },
      error: (err: any) => {
        console.log('Error: ' + JSON.stringify(err.error ?? err));
      }
    });
  }

  showPassword = false;

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }

}
