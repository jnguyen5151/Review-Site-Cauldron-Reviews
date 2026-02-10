import { Component, inject, signal } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, ReactiveFormsModule } from '@angular/forms';

import { AuthService } from '../../services/auth-service';
import { AccountDetailsModel } from '../../models/user-account';

@Component({
  selector: 'app-my-account',
  imports: [],
  templateUrl: './my-account.html',
  styleUrl: './my-account.css',
})
export class MyAccount {
  private authService = inject(AuthService);
  userDetails = signal<AccountDetailsModel | null>(null);

  accountForm = new FormGroup({
    displayName: new FormControl<string>(''),
    description: new FormControl<string>(''),
    birthday: new FormControl<Date | null>(null),
    pronouns: new FormControl<string>(''),
    safeMode: new FormControl<boolean>(false)
  })

  ngOnInit(): void {
    this.authService.getAccount().subscribe((data: AccountDetailsModel) => {
      console.log("data is " + data);
      this.userDetails.set(data);
      console.log(this.userDetails);
      this.accountForm.setValue({
        displayName: data.displayName,
        description: data.description,
        birthday: data.birthday,
        pronouns: data.pronouns,
        safeMode: data.safeMode
      });
      console.log(this.accountForm.value);
    })
  }
}
