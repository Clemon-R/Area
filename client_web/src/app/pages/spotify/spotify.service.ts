import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Account } from '../../models/account';
import {ResultViewModel} from '../../viewModels/ResultViewModel';
import {AccountResultViewModel} from '../../viewModels/AccountResultViewModel';
import {EntityFiller} from '../../entityFiller';

@Injectable({ providedIn: 'root' })
export class SpotifyService {
  constructor(private http: HttpClient) {}

  public getToken(account: Account, code: string): Promise<ResultViewModel> {
    const body = JSON.stringify(account);
    return this.http.post<ResultViewModel>('/api/spotify/token/' + code, body).toPromise().then(
      (result: ResultViewModel) => {
        return result;
      }, (error) => {
        console.log('YammerService(getToken): Error ' + error);
        return null;
      }
    );
  }

  public isTokenAvailable(account: Account): Promise<ResultViewModel> {
    const body = JSON.stringify(account);
    return this.http.post<ResultViewModel>('/api/spotify/avaibale/', body).toPromise().then(
      (result: ResultViewModel) => {
        return result;
      }, (error) => {
        console.log('YammerService(getToken): Error ' + error);
        return null;
      }
    );
  }

  public connectAccount(code: string): Promise<Account> {
    return this.http.get('/api/spotify/login/' + code).toPromise().then(
      (result: AccountResultViewModel) => {
        if (!result.success)
          return null;
        return EntityFiller.FillAccount(result);
      }, (error) => {
        console.log('YammerService(connectAccount): Error ' + error);
        return null;
      }
    );
  }
}
