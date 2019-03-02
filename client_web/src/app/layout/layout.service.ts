import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';

import { Account } from '../models/account';
import {LoginViewModel} from '../viewModels/layout/LoginViewModel';
import {LoginResultViewModel} from '../viewModels/layout/LoginResultViewModel';
import {AccountResultViewModel} from '../viewModels/AccountResultViewModel';
import {ResultViewModel} from '../viewModels/ResultViewModel';

@Injectable({ providedIn: 'root' })
export class LayoutService {
  constructor(private http: HttpClient) {}

  public async getAccount(account: Account): Promise<Account> {
    const body = JSON.stringify(account);
    let data: Account = null;
    await this.http.post('/api/account/get', body).toPromise().then(
      (result: AccountResultViewModel) => {
        data = new Account();
        data.token = result.token;
      }, (error) => {
        console.log('LayoutService(getAccount): Error ' + error);
      }
    );
    return data;
  }

  public async disconnectAccount(account: Account): Promise<ResultViewModel> {
    const body = JSON.stringify(account);
    let data: ResultViewModel = null;
    await this.http.post('/api/account/logout', body).toPromise().then(
      (result: ResultViewModel) => {
        data = result;
      }, (error) => {
        console.log('LayoutService(disconnectAccount): Error ' + error);
      }
    );
    return data;
  }

  public async connectAccount(username: string, password: string): Promise<Account> {
    const model = new LoginViewModel();
    model.username = username;
    model.password = password;
    const body = JSON.stringify(model);
    console.log(body);
    let data: Account = null;
    await this.http.post('/api/account/login', body).toPromise().then(
      (result: LoginResultViewModel) => {
        data = new Account();
        data.token = result.token;
        return data;
      },
      (error) => {
        console.log('LayoutService(connectAccount): Error ' + error);
      }
    );
    return data;
  }
}
