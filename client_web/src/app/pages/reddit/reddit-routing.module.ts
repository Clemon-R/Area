import {RouterModule, Routes} from '@angular/router';
import {NgModule} from '@angular/core';

import {RedditComponent} from './reddit.component';
import {RedditCallbackComponent} from './callback/reddit-callback.component';
import {RedditLoginComponent} from './login/reddit-login.component';

const spotifyRoutes: Routes = [
  { path: '', component: RedditComponent },
  { path: 'callback', component: RedditCallbackComponent },
  { path: 'login', component: RedditLoginComponent }
];

@NgModule({
  imports: [
    RouterModule.forChild(spotifyRoutes)
  ],
  exports: [
    RouterModule
  ]
})

export class RedditRoutingModule { }
