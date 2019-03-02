import { Component, OnInit } from '@angular/core';
import {RegisterService} from './register.service';
import {ResultViewModel} from '../../viewModels/ResultViewModel';

@Component({
  selector: 'app-home',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  providers: [RegisterService]
})
export class RegisterComponent implements OnInit {
  username: string;
  password: string;
  passwordConfirm: string;

  error: boolean;
  success: boolean;
  message: string;

  constructor(private registerService: RegisterService) { }

  ngOnInit() {
    this.username = '';
    this.password = '';
    this.passwordConfirm = '';

    this.error = false;
    this.success = false;
    this.message = '';
  }

  public register() {
    console.log('Trying to register...');
    this.registerService.registerAccount(this.username, this.password, this.passwordConfirm).then(
      (result: ResultViewModel) => {
        if (result && result.success) {
          this.message = 'Votre compte à été créer avec succés';
          this.success = true;
          this.error = false;
          console.log('Register successfully');
        } else if (result) {
          this.message = result.error;
          this.error = true;
          this.success = false;
          console.log('Register failed');
        }
      }
    );
  }
}
