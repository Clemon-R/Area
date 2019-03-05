import {RouterModule, Routes} from '@angular/router';
import {NgModule} from '@angular/core';

import {YammerComponent} from './yammer.component';
import {YammerCallbackComponent} from './callback/yammer-callback.component';
import {SpotifyLoginComponent} from './login/spotify-login.component';

const spotifyRoutes: Routes = [
  { path: '', component: YammerComponent },
  { path: 'callback', component: YammerCallbackComponent },
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

export class YammerRoutingModule { }
