import { Validators, FormGroup, AbstractControl, ValidationErrors } from '@angular/forms';
import { RegisterModel } from '../../models/user-account';

export const registerDefaults: RegisterModel = {
  userName: '',
  email: '',
  password: ''
}

export const registerValidators = {
  userName: [
    Validators.required,
    Validators.minLength(3),
    Validators.maxLength(15)
  ],

  email: [
    Validators.required,
    Validators.email
  ],

  password: [
    Validators.required,
    Validators.minLength(8),
    Validators.maxLength(50),
    passwordValidator
  ]
}

export function passwordValidator(control: AbstractControl): ValidationErrors | null
{
  const value = control.value || '';
  const errors: string[] = [];

  const hasDigit = /.*\d.*/.test(value);
  const hasLower = /^(?=.*[a-z]).+$/.test(value);
  const hasSymbol = /^(?=.*[^A-Za-z0-9]).+$/.test(value);
  const hasUpper = /^(?=.*[A-Z]).+$/.test(value);

  if (!hasDigit) {
    errors.push('Must contain at least 1 Number')
  }

  if (!hasLower) {
    errors.push('Must contain at least 1 Lowercase Letter');
  }

  if (!hasSymbol) {
    errors.push('Must Contain at least 1 Symbol');
  }

  if (!hasUpper) {
    errors.push('Must Contain at least 1 Uppercase Letter');
  }

  return errors.length ? { messages:errors } : null;
}
