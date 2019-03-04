import { FormsModule } from '@angular/forms';
import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {TwitchComponent} from './twitch.component';
import {TwitchAddComponent} from './add/twitch-add.component';
import {TwitchRoutingModule} from './twitch-routing.module';
import {TwitchAllComponent} from './all/twitch-all.component';
import {SpotifyCallbackComponent} from './callback/spotify-callback.component';
import {SpotifyLoginComponent} from './login/spotify-login.component';

@NgModule({
  declarations: [
    TwitchComponent,
    TwitchAddComponent,
    TwitchAllComponent,
    SpotifyLoginComponent,
    SpotifyCallbackComponent
  ],
  imports: [
    CommonModule,
    TwitchRoutingModule
  ],
  exports: [
    TwitchComponent,
    TwitchAddComponent,
    TwitchAllComponent,
    SpotifyCallbackComponent
  ]
})
export class TwitchModule { }
