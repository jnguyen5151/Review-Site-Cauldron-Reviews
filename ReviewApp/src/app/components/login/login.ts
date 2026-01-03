import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { DialogRef } from '@angular/cdk/dialog';

import { AuthService } from '../../services/auth-service';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule, RouterModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {

  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private dialogRef = inject(DialogRef);

  loginForm: FormGroup = this.fb.group({
    identifier: '',
    password: ''
  });

  showPassword = false;

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
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

  closeModal() {
    this.dialogRef.close();
  }

  backToLogin() {
    console.log('Back to Login triggered!');
  }

}
