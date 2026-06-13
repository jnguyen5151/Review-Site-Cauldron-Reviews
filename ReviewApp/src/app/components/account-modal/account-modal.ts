import { Component, signal, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { Dialog, DialogRef, DIALOG_DATA } from '@angular/cdk/dialog';

import { registerDefaults, registerValidators, passwordValidator } from './register-form.config';
import { AuthService } from '../../services/auth-service';

export type ModalView = 'login' | 'register';

@Component({
  selector: 'app-account-modal',
  imports: [ReactiveFormsModule, RouterModule],
  templateUrl: './account-modal.html',
  styleUrl: './account-modal.css',
})
export class AccountModal {
  private fb = inject(FormBuilder);
  private dialogRef = inject(DialogRef);
  private data = inject(DIALOG_DATA, { optional: true }) as { initialView: ModalView } | null;
  private authService = inject(AuthService);
  modalView: 'Login' | 'Register' = 'Login';

  view: ModalView = this.data?.initialView ?? 'login';

  loginForm: FormGroup = this.fb.group({
    identifier: '',
    password: ''
  });

  closeModal() {
    this.dialogRef.close();
  }

  login() {

    const dto = this.loginForm.value;
    this.authService.login(dto).subscribe({
      next: () => {
        this.dialogRef.close();
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

  switchView(v: ModalView) {
    this.view = v;
  }

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

  registerMessage = signal<{ type: 'success' | 'error'; message: string } | null>(null);

  register() {

    if (this.registerForm.invalid) {
      this.registerForm.markAllAsTouched();
      return;
    }

    const dto = this.registerForm.value;

    this.authService.register(dto).subscribe({
      next: () => {
        this.registerMessage.set({ type: 'success', message: 'Account created, Please check your email for a Verification Link.' });
      },
      error: (err: any) => {
        const apiMessage = err.error?.[0]?.description ?? 'Something went wrong. Please try again.';
        this.registerMessage.set({ type: 'error', message: apiMessage });
      }
    });

  }


}
