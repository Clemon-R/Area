import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import {ReactionViewModel} from '../../viewModels/area/ReactionViewModel';
import {ResultViewModel} from '../../viewModels/ResultViewModel';
import {NewAreaViewModel} from '../../viewModels/area/NewAreaViewModel';

import { Account } from '../../models/account';
import {Trigger} from '../../models/trigger';
import {ActionViewModel} from '../../viewModels/area/ActionViewModel';

@Injectable({ providedIn: 'root' })
export class AreaService {
  constructor(private http: HttpClient) {}

  public getActions(): Promise<ActionViewModel[]> {
    return this.http.get<ActionViewModel[]>('/api/area/actions/').toPromise().then(
      (result: ActionViewModel[]) => {
        return result;
      }, (error) => {
        console.log('AreaService(getActions): Error ' + error);
        return [];
      }
    );
  }

  public getReactions(): Promise<ReactionViewModel[]> {
    return this.http.get<ReactionViewModel[]>('/api/area/reactions/').toPromise().then(
      (result: ReactionViewModel[]) => {
        return result;
      }, (error) => {
        console.log('AreaService(getReactions): Error ' + error);
        return [];
      }
    );
  }

  public newArea(account: Account, actionId: number, reactionId: number): Promise<ResultViewModel> {
    const model: NewAreaViewModel = new NewAreaViewModel();
    model.token = account.token;
    model.reactionId = reactionId;
    model.actionId = actionId;
    console.log(actionId + ' ' + reactionId);
    const body = JSON.stringify(model);
    return this.http.post('/api/area/new', body).toPromise().then(
      (result: ResultViewModel) => {
        return result;
      },
      (error) => {
        console.log('AreaService(newArea): Error ' + error);
        const result = new ResultViewModel();
        result.error = 'Une erreur sais produite';
        result.success = false;
        return result;
      }
    );
  }

  public getTriggers(account: Account): Promise<Trigger[]> {
    const body = JSON.stringify(account);
    return this.http.post('/api/area/triggers', body).toPromise().then(
      (result: Trigger[]) => {
        return result;
      },
      (error) => {
        console.log('AreaService(getTriggers): Error ' + error);
        return [];
      }
    );
  }

  public deleteTrigger(account: Account, id: number): Promise<ResultViewModel> {
    const body = JSON.stringify(account);
    return this.http.post<ResultViewModel>('/api/area/delete/trigger/' + id, body).toPromise().then(
      (result: ResultViewModel) => {
        return result;
      },
      (error) => {
        console.log('AreaService(getTriggers): Error ' + error);
        const result = new ResultViewModel();
        result.error = 'Une erreur sais produite';
        result.success = false;
        return result;
      }
    );
  }
}
