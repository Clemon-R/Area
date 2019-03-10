/* tslint:disable:curly */
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Account } from '../../models/account';
import {ResultViewModel} from '../../viewModels/ResultViewModel';
import {AccountResultViewModel} from '../../viewModels/AccountResultViewModel';
import {EntityFiller} from '../../entityFiller';
import {YoutubeTokenViewModel} from '../../viewModels/youtube/YoutubeTokenViewModel';

@Injectable({ providedIn: 'root' })
export class YoutubeService {
  constructor(private http: HttpClient) {}

  public getToken(account: Account, code: string): Promise<ResultViewModel> {
    const model: YoutubeTokenViewModel = new YoutubeTokenViewModel();
    model.token = account.token
    model.code = code
    const body = JSON.stringify(model);
    return this.http.post<ResultViewModel>('/api/youtube/token/', body).toPromise().then(
      (result: ResultViewModel) => {
        return result;
      }, (error) => {
        console.log('YoutubeService(getToken): Error ' + error);
        const result = new ResultViewModel();
        result.error = 'Une erreur sais produite';
        result.success = false;
        return result;
      }
    );
  }

  public isTokenAvailable(account: Account): Promise<ResultViewModel> {
    const body = JSON.stringify(account);
    return this.http.post<ResultViewModel>('/api/youtube/available/', body).toPromise().then(
      (result: ResultViewModel) => {
        return result;
      }, (error) => {
        console.log('YoutubeService(getToken): Error ' + error);
        const result = new ResultViewModel();
        result.error = 'Une erreur sais produite';
        result.success = false;
        return result;
      }
    );
  }

  public connectAccount(code: string): Promise<Account> {
    return this.http.get('/api/youtube/login/' + code).toPromise().then(
      (result: AccountResultViewModel) => {
        if (!result.success)
          return null;
        return EntityFiller.FillAccount(result);
      }, (error) => {
        console.log('YoutubeService(connectAccount): Error ' + error);
        return null;
      }
    );
  }

  public deleteToken(account: Account): Promise<ResultViewModel> {
    const body = JSON.stringify(account);
    return this.http.post<ResultViewModel>('/api/youtube/delete/token', body).toPromise().then(
      (result: ResultViewModel) => {
        return result;
      }, (error) => {
        console.log('YoutubeService(deleteToken): Error ' + error);
        const result = new ResultViewModel();
        result.error = 'Une erreur sais produite';
        result.success = false;
        return result;
      }
    );
  }
}
