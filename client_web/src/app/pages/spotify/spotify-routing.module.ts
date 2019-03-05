import {RouterModule, Routes} from '@angular/router';
import {NgModule} from '@angular/core';

import {SpotifyComponent} from './spotify.component';
import {SpotifyCallbackComponent} from './callback/spotify-callback.component';
import {SpotifyLoginComponent} from './login/spotify-login.component';

const spotifyRoutes: Routes = [
  { path: '', component: SpotifyComponent },
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
