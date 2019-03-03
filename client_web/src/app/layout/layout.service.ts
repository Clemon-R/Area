import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {EntityFiller} from '../entityFiller';

import { Account } from '../models/account';
import {LoginViewModel} from '../viewModels/layout/LoginViewModel';
import {LoginResultViewModel} from '../viewModels/layout/LoginResultViewModel';
import {AccountResultViewModel} from '../viewModels/AccountResultViewModel';
import {ResultViewModel} from '../viewModels/ResultViewModel';

@Injectable({ providedIn: 'root' })
export class LayoutService {
  constructor(private http: HttpClient) {}

  public getAccount(account: Account): Promise<Account> {
    const body = JSON.stringify(account);
    return this.http.post('/api/account/get', body).toPromise().then(
      (result: AccountResultViewModel) => {
        return EntityFiller.FillAccount(result);;
      }, (error) => {
        console.log('LayoutService(getAccount): Error ' + error);
        return null;
      }
    );
  }

  public disconnectAccount(account: Account): Promise<ResultViewModel> {
    const body = JSON.stringify(account);
    return this.http.post('/api/account/logout', body).toPromise().then(
      (result: ResultViewModel) => {
        return result;
      }, (error) => {
        console.log('LayoutService(disconnectAccount): Error ' + error);
        return null;
      }
    );
  }

  public connectAccount(username: string, password: string): Promise<Account> {
    const model = new LoginViewModel();
    model.username = username;
    model.password = password;
    const body = JSON.stringify(model);
    return this.http.post('/api/account/login', body).toPromise().then(
      (result: AccountResultViewModel) => {
        return EntityFiller.FillAccount(result);;
      },
      (error) => {
        console.log('LayoutService(connectAccount): Error ' + error);
        return null;
      }
    );
  }
}
