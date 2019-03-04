import {RouterModule, Routes} from '@angular/router';
import {NgModule} from '@angular/core';

import {TwitchComponent} from './twitch.component';
import {TwitchAddComponent} from './add/twitch-add.component';
import {TwitchAllComponent} from './all/twitch-all.component';
import {SpotifyCallbackComponent} from './callback/spotify-callback.component';
import {SpotifyLoginComponent} from './login/spotify-login.component';

const spotifyRoutes: Routes = [
  { path: '', component: TwitchComponent },
  { path: 'add', component: TwitchAddComponent },
  { path: 'all', component: TwitchAllComponent },
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

export class TwitchRoutingModule { }
