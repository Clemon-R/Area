import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';

import { Account } from '../models/account';

@Injectable({ providedIn: 'root' })
export class LayoutService {
  constructor(private http: HttpClient) {}

  public getAccount(account: Account): Account
  {
    const body = JSON.stringify(account);
    let result: Account = null;
    const headers =  new HttpHeaders({ 'Content-Type': 'application/json' }); // ... Set content type to JSON
    headers.append('Accept', 'application/json');
    headers.append('Access-Control-Allow-Methods', 'POST, GET, OPTIONS, DELETE, PUT');
    headers.append('Access-Control-Allow-Origin', 'http://localhost:4503');
    headers.append('Access-Control-Allow-Headers', "X-Requested-With, Content-Type, Origin, Authorization, Accept, Client-Security-Token, Accept-Encoding");
    this.http.post('/api/account/logout', body, {headers: headers})
      .subscribe(
        (data: Account) => {
          console.log('LayoutService(getAccount): ' + data);
          result = data;
        }, (error) => {
          console.log('LayoutService(getAccount): ' + error);
        }
      );
    return result;
    /*return this.http.put(this.heroesUrl, hero, httpOptions).pipe(
    tap(_ => this.log(`updated hero id=${hero.id}`)),
    catchError(this.handleError<any>('updateHero'))
    );*/
  }
}
