import { FormsModule } from '@angular/forms';
import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {YammerComponent} from './yammer.component';
import {YammerRoutingModule} from './yammer-routing.module';
import {YammerCallbackComponent} from './callback/yammer-callback.component';
import {SpotifyLoginComponent} from './login/spotify-login.component';

@NgModule({
  declarations: [
    YammerComponent,
    SpotifyLoginComponent,
    YammerCallbackComponent
  ],
  imports: [
    CommonModule,
    YammerRoutingModule
  ],
  exports: [
    YammerComponent,
    YammerCallbackComponent
  ]
})
export class YammerModule { }
