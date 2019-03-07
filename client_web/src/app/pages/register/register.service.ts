import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import {RegisterViewModel} from '../../viewModels/register/RegisterViewModel';
import {ResultViewModel} from '../../viewModels/ResultViewModel';

@Injectable({ providedIn: 'root' })
export class RegisterService {
  constructor(private http: HttpClient) {}

  public registerAccount(username: string, password: string, passwordConfirm: string): Promise<ResultViewModel> {
    const model = new RegisterViewModel();
    model.username = username;
    model.password = password;
    model.passwordConfirm = passwordConfirm;
    const body = JSON.stringify(model);
    return this.http.post<ResultViewModel>('/api/account/register', body).toPromise().then(
      (result: ResultViewModel) => {
        return result;
      },
      (error) => {
        console.log('RegisterService(registerAccount): Error ' + error);
        const result = new ResultViewModel();
        result.error = 'Une erreur sais produite';
        result.success = false;
        return result;
      }
    );
  }
}
