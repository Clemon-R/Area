import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Account } from '../../models/account';
import {ResultViewModel} from '../../viewModels/ResultViewModel';
import {AccountResultViewModel} from '../../viewModels/AccountResultViewModel';
import {EntityFiller} from '../../entityFiller';

@Injectable({ providedIn: 'root' })
export class YammerService {
  constructor(private http: HttpClient) {}

  public getToken(account: Account, code: string): Promise<ResultViewModel> {
    const body = JSON.stringify(account);
    return this.http.post<ResultViewModel>('/api/yammer/token/' + code, body).toPromise().then(
      (result: ResultViewModel) => {
        return result;
      }, (error) => {
        console.log('RedditService(getToken): Error ' + error);
        const result = new ResultViewModel();
        result.error = 'Une erreur sais produite';
        result.success = false;
        return result;
      }
    );
  }

  public isTokenAvailable(account: Account): Promise<ResultViewModel> {
    const body = JSON.stringify(account);
    return this.http.post<ResultViewModel>('/api/yammer/available/', body).toPromise().then(
      (result: ResultViewModel) => {
        return result;
      }, (error) => {
        console.log('RedditService(getToken): Error ' + error);
        const result = new ResultViewModel();
        result.error = 'Une erreur sais produite';
        result.success = false;
        return result;
      }
    );
  }

  public connectAccount(code: string): Promise<Account> {
    return this.http.get('/api/yammer/login/' + code).toPromise().then(
      (result: AccountResultViewModel) => {
        if (!result.success)
          return null;
        return EntityFiller.FillAccount(result);
      }, (error) => {
        console.log('RedditService(connectAccount): Error ' + error);
        return null;
      }
    );
  }

  public deleteToken(account: Account): Promise<ResultViewModel> {
    const body = JSON.stringify(account);
    return this.http.post<ResultViewModel>('/api/yammer/delete/token', body).toPromise().then(
      (result: ResultViewModel) => {
        return result;
      }, (error) => {
        console.log('RedditService(deleteToken): Error ' + error);
        const result = new ResultViewModel();
        result.error = 'Une erreur sais produite';
        result.success = false;
        return result;
      }
    );
  }
}
