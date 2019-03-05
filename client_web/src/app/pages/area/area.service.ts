import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import {ActionReactionViewModel} from '../../viewModels/area/ActionReactionViewModel';
import {ResultViewModel} from '../../viewModels/ResultViewModel';
import {NewAreaViewModel} from '../../viewModels/area/NewAreaViewModel';

import { Account } from '../../models/account';

@Injectable({ providedIn: 'root' })
export class AreaService {
  constructor(private http: HttpClient) {}

  public getActions(): Promise<ActionReactionViewModel[]> {
    return this.http.get<ActionReactionViewModel[]>('/api/area/actions/').toPromise().then(
      (result: ActionReactionViewModel[]) => {
        return result;
      }, (error) => {
        console.log('AreaService(getActions): Error ' + error);
        return null;
      }
    );
  }

  public getReactions(): Promise<ActionReactionViewModel[]> {
    return this.http.get<ActionReactionViewModel[]>('/api/area/reactions/').toPromise().then(
      (result: ActionReactionViewModel[]) => {
        return result;
      }, (error) => {
        console.log('AreaService(getActions): Error ' + error);
        return null;
      }
    );
  }

  public newArea(account: Account, actionId: number, reactionId: number): Promise<ResultViewModel> {
    const model: NewAreaViewModel = new NewAreaViewModel();
    model.token = account.token;
    model.actionId = actionId;
    model.reactionId = reactionId;
    const body = JSON.stringify(model);
    return this.http.post('/api/area/new', body).toPromise().then(
      (result: ResultViewModel) => {
        return result;
      },
      (error) => {
        console.log('AreaService(newArea): Error ' + error);
        return null;
      }
    );
  }
}
