import { FormsModule } from '@angular/forms';
import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {SpotifyComponent} from './spotify.component';
import {SpotifyRoutingModule} from './spotify-routing.module';
import {SpotifyCallbackComponent} from './callback/spotify-callback.component';
import {SpotifyLoginComponent} from './login/spotify-login.component';

@NgModule({
  declarations: [
    SpotifyComponent,
    SpotifyLoginComponent,
    SpotifyCallbackComponent
  ],
  imports: [
    CommonModule,
    SpotifyRoutingModule
  ],
  exports: [
    SpotifyComponent,
    SpotifyCallbackComponent
  ]
})
export class SpotifyModule { }
