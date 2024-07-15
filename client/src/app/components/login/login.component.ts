import { Component, inject } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { SharedModule } from '../../shared.module';
import { LoginUser } from '../../models/loginUser';
import { AccountService } from '../../services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  private accountService = inject(AccountService);
  private router = inject(Router);
  formBuilder = inject(FormBuilder);

  loginForm = this.formBuilder.group({
    email: new FormControl<string>('', [Validators.required]),
    password: new FormControl<string>('', [Validators.required]),
  });

  login() {
    const logUserData: LoginUser = {
      email: this.loginForm.get("email")?.value ?? "",
      password: this.loginForm.get("password")?.value ?? "",
    };
    this.accountService.login(logUserData)
      .subscribe(_ => {
        this.router.navigate([""]);
      });
  }

  toRegistrationPage(): void {
    this.router.navigate(["/sign-up"]);
  }
}
