import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private fb: FormBuilder) { }

  formModel = this.fb.group({
    UserName: ['', Validators.required],
    Email: ['', Validators.email],
    FullName: [''],
    Passwords: this.fb.group({
      Password: ['', [Validators.required, Validators.minLength(4)]],
      ConfirmPassword: ['', Validators.required]
    }, { validator: this.comparePasswords })
  });

  comparePasswords(fb: FormGroup) {
    let cofirmPasswordCntrol = fb.get('ConfirmPassword');
    if (cofirmPasswordCntrol.errors == null || 'passwordMismatch' in cofirmPasswordCntrol.errors) {
      if (fb.get('Password').value != cofirmPasswordCntrol.value)
        cofirmPasswordCntrol.setErrors({ passwordMismatch: true });
      else
        cofirmPasswordCntrol.setErrors(null);
    }
  }
}
