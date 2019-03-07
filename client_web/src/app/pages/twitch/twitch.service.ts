import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Account } from '../../models/account';
import {ResultViewModel} from '../../viewModels/ResultViewModel';
import {AccountResultViewModel} from '../../viewModels/AccountResultViewModel';
import {EntityFiller} from '../../entityFiller';

@Injectable({ providedIn: 'root' })
export class TwitchService {
  constructor(private http: HttpClient) {}

  public getToken(account: Account, code: string): Promise<ResultViewModel> {
    const body = JSON.stringify(account);
    return this.http.post<ResultViewModel>('/api/twitch/token/' + code, body).toPromise().then(
      (result: ResultViewModel) => {
        return result;
      }, (error) => {
        console.log('TwitchService(getToken): Error ' + error);
        return null;
      }
    );
  }

  public isTokenAvailable(account: Account): Promise<ResultViewModel> {
    const body = JSON.stringify(account);
    return this.http.post<ResultViewModel>('/api/twitch/available/', body).toPromise().then(
      (result: ResultViewModel) => {
        return result;
      }, (error) => {
        console.log('TwitchService(getToken): Error ' + error);
        const result = new ResultViewModel();
        result.error = 'Une erreur sais produite';
        result.success = false;
        return result;
      }
    );
  }

  public connectAccount(code: string): Promise<Account> {
    return this.http.get('/api/twitch/login/' + code).toPromise().then(
      (result: AccountResultViewModel) => {
        if (!result.success)
          return null;
        return EntityFiller.FillAccount(result);
      }, (error) => {
        console.log('TwitchService(connectAccount): Error ' + error);
        return null;
      }
    );
  }

  public deleteToken(account: Account): Promise<ResultViewModel> {
    const body = JSON.stringify(account);
    return this.http.post<ResultViewModel>('/api/twitch/delete/token', body).toPromise().then(
      (result: ResultViewModel) => {
        return result;
      }, (error) => {
        console.log('TwitchService(deleteToken): Error ' + error);
        const result = new ResultViewModel();
        result.error = 'Une erreur sais produite';
        result.success = false;
        return result;
      }
    );
  }
}
