import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Account } from '../../models/account';
import {SpotifyTokenApi} from '../../viewModels/spotify/SpotifyTokenApi';

@Injectable({ providedIn: 'root' })
export class SpotifyService {
  constructor(private http: HttpClient) {}

  public getToken(code: string): Promise<SpotifyTokenApi> {
    return this.http.get<SpotifyTokenApi>('/api/spotify/token/' + code).toPromise();
  }
}
