import { FormsModule } from '@angular/forms';
import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {TwitchComponent} from './twitch.component';
import {TwitchRoutingModule} from './twitch-routing.module';
import {SpotifyCallbackComponent} from './callback/spotify-callback.component';
import {SpotifyLoginComponent} from './login/spotify-login.component';

@NgModule({
  declarations: [
    TwitchComponent,
    SpotifyLoginComponent,
    SpotifyCallbackComponent
  ],
  imports: [
    CommonModule,
    TwitchRoutingModule
  ],
  exports: [
    TwitchComponent,
    SpotifyCallbackComponent
  ]
})
export class TwitchModule { }
