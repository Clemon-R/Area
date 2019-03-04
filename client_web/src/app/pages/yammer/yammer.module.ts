import { FormsModule } from '@angular/forms';
import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {YammerComponent} from './yammer.component';
import {YammerAddComponent} from './add/yammer-add.component';
import {YammerRoutingModule} from './yammer-routing.module';
import {YammerAllComponent} from './all/yammer-all.component';
import {YammerCallbackComponent} from './callback/yammer-callback.component';
import {SpotifyLoginComponent} from './login/spotify-login.component';

@NgModule({
  declarations: [
    YammerComponent,
    YammerAddComponent,
    YammerAllComponent,
    SpotifyLoginComponent,
    YammerCallbackComponent
  ],
  imports: [
    CommonModule,
    YammerRoutingModule
  ],
  exports: [
    YammerComponent,
    YammerAddComponent,
    YammerAllComponent,
    YammerCallbackComponent
  ]
})
export class YammerModule { }
