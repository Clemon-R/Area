import {RouterModule, Routes} from '@angular/router';
import {NgModule} from '@angular/core';

import {YoutubeComponent} from './youtube.component';
import {YoutubeCallbackComponent} from './callback/youtube-callback.component';
import {YoutubeLoginComponent} from './login/youtube-login.component';

const spotifyRoutes: Routes = [
  { path: '', component: YoutubeComponent },
  { path: 'callback', component: YoutubeCallbackComponent },
  { path: 'login', component: YoutubeLoginComponent }
];

@NgModule({
  imports: [
    RouterModule.forChild(spotifyRoutes)
  ],
  exports: [
    RouterModule
  ]
})

export class YoutubeRoutingModule { }
