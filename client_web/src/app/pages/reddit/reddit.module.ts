import { FormsModule } from '@angular/forms';
import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {RedditComponent} from './reddit.component';
import {RedditRoutingModule} from './reddit-routing.module';
import {RedditCallbackComponent} from './callback/reddit-callback.component';
import {RedditLoginComponent} from './login/reddit-login.component';

@NgModule({
  declarations: [
    RedditComponent,
    RedditLoginComponent,
    RedditCallbackComponent
  ],
  imports: [
    CommonModule,
    RedditRoutingModule
  ],
  exports: [
    RedditComponent,
    RedditCallbackComponent
  ]
})
export class RedditModule { }
