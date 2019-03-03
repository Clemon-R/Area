import { FormsModule } from '@angular/forms';
import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {SpotifyComponent} from './spotify.component';
import {SpotifyAddComponent} from './add/spotify-add.component';
import {SpotifyRoutingModule} from './spotify-routing.module';
import {SpotifyAllComponent} from './all/spotify-all.component';
import {SpotifyCallbackComponent} from './callback/spotify-callback.component';

@NgModule({
  declarations: [
    SpotifyComponent,
    SpotifyAddComponent,
    SpotifyAllComponent,
    SpotifyCallbackComponent
  ],
  imports: [
    CommonModule,
    SpotifyRoutingModule
  ],
  exports: [
    SpotifyComponent,
    SpotifyAddComponent,
    SpotifyAllComponent,
    SpotifyCallbackComponent
  ]
})
export class SpotifyModule { }
