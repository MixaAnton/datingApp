import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { User } from '../_models/user';
import { ToastrService } from 'ngx-toastr';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  @Output() cancelRegister = new EventEmitter();
  registerForm!: FormGroup;
  bsConfig!: Partial<BsDatepickerConfig>;
  maxDate!: Date;
  validationErrors: string[] = [];

  constructor(private accountService: AccountService,
    private toastr: ToastrService,
    private fb: FormBuilder,
    private router: Router) {

    this.bsConfig = {
      containerClass: 'theme-red',
      dateInputFormat: 'DD.MMMM.YYYY.'

    }
    this.maxDate = new Date();
    this.maxDate.setFullYear(this.maxDate.getFullYear() - 18);
  }
  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.registerForm = this.fb.group({
      gender:['male'],
      username: ['', Validators.required],
      knownAs: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required],
      password:['', [Validators.required,
        Validators.minLength(4), Validators.maxLength(10)]],
      confirmPassword: ['', Validators.required]
    }, {
      validators: this.confirmPasswordValidator
    })
    //this.registerForm.controls['password'].valueChanges.subscribe(() => {
    //  this.registerForm.controls['confirmPassword'].updateValueAndValidity();
    //})
  }
   confirmPasswordValidator: ValidatorFn = (
    control: AbstractControl
  ): ValidationErrors | null => {
     return control.value.password === control.value.confirmPassword
      ? null
      : { PasswordNoMatch: true };
  };
  
  register() {
    console.log(this.registerForm.value);
    this.accountService.register(this.registerForm.value).subscribe(response => {
      this.router.navigateByUrl('/members');
      
    }, error => {
      this.validationErrors = error;
    })
  }
  cancel() {

    this.cancelRegister.emit(false);
  }
}
