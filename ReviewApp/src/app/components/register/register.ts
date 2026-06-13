import { Component, signal, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Field, form } from '@angular/forms/signals';
import { FormBuilder, FormGroup, FormControl, ReactiveFormsModule } from '@angular/forms';

import { AuthService } from '../../services/auth-service';
//import { registerDefaults, registerValidators, passwordValidator } from './register-form.config';
import { Dialog, DialogRef } from '@angular/cdk/dialog';
import { Login } from '../login/login';

@Component({
  selector: 'app-register',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {

  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private dialogRef = inject(DialogRef);
  private dialog = inject(Dialog);

  // registerForm: FormGroup = this.fb.group({
  //   userName: [
  //     registerDefaults.userName,
  //     registerValidators.userName
  //   ],
  //   email: [
  //     registerDefaults.email,
  //     registerValidators.email
  //   ],
  //   password: [
  //     registerDefaults.password,
  //     registerValidators.password
  //   ]
  // });

  // get passwordMessages(): string[] {
  //   const errors = this.registerForm.get('password')?.errors;
  //   return errors?.['messages'] ?? [];
  // }

  // registerMessage = signal<{ type: 'success' | 'error'; message: string } | null>(null);

  // register() {

  //   if (this.registerForm.invalid) {
  //     this.registerForm.markAllAsTouched();
  //     return;
  //   }

  //   const dto = this.registerForm.value;

  //   this.authService.register(dto).subscribe({
  //     next: () => {
  //       this.registerMessage.set({ type: 'success', message: 'Account created, Please check your email for a Verification Link.' });
  //     },
  //     error: (err: any) => {
  //       const apiMessage = err.error?.[0]?.description ?? 'Something went wrong. Please try again.';
  //       this.registerMessage.set({ type: 'error', message: apiMessage });
  //     }
  //   });

  // }

  // showPassword = false;

  // togglePasswordVisibility() {
  //   this.showPassword = !this.showPassword;
  // }

  // protected openLoginModal() {
  //   this.dialog.open(Login);
  // }

  // closeModal() {
  //   this.dialogRef.close();
  // }

}
