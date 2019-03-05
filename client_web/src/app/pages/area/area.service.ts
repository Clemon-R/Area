import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import {ActionReactionViewModel} from '../../viewModels/area/ActionReactionViewModel';

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
}
