import { FormsModule } from '@angular/forms';
import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {YoutubeComponent} from './youtube.component';
import {YoutubeRoutingModule} from './youtube-routing.module';
import {YoutubeCallbackComponent} from './callback/youtube-callback.component';
import {YoutubeLoginComponent} from './login/youtube-login.component';

@NgModule({
  declarations: [
    YoutubeComponent,
    YoutubeLoginComponent,
    YoutubeCallbackComponent
  ],
  imports: [
    CommonModule,
    YoutubeRoutingModule
  ],
  exports: [
    YoutubeComponent,
    YoutubeCallbackComponent
  ]
})
export class YoutubeModule { }
