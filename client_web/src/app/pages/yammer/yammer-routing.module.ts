import {RouterModule, Routes} from '@angular/router';
import {NgModule} from '@angular/core';

import {YammerComponent} from './yammer.component';
import {YammerAddComponent} from './add/yammer-add.component';
import {YammerAllComponent} from './all/yammer-all.component';
import {YammerCallbackComponent} from './callback/yammer-callback.component';
import {SpotifyLoginComponent} from './login/spotify-login.component';

const spotifyRoutes: Routes = [
  { path: '', component: YammerComponent },
  { path: 'add', component: YammerAddComponent },
  { path: 'all', component: YammerAllComponent },
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
