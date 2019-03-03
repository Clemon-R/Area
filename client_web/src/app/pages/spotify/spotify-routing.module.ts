import {RouterModule, Routes} from '@angular/router';
import {NgModule} from '@angular/core';

import {SpotifyComponent} from './spotify.component';
import {SpotifyAddComponent} from './add/spotify-add.component';
import {SpotifyAllComponent} from './all/spotify-all.component';
import {SpotifyCallbackComponent} from './callback/spotify-callback.component';
import {SpotifyLoginComponent} from './login/spotify-login.component';

const spotifyRoutes: Routes = [
  { path: '', component: SpotifyComponent },
  { path: 'add', component: SpotifyAddComponent },
  { path: 'all', component: SpotifyAllComponent },
  { path: 'callback', component: SpotifyCallbackComponent },
  { path: 'login', component: SpotifyLoginComponent }
];

@NgModule({
  imports: [
    RouterModule.forChild(spotifyRoutes)
  ],
  exports: [
    RouterModule
  ]
})

export class SpotifyRoutingModule { }
