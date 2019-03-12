import { NgModule } from '@angular/core';
import {PreloadAllModules, RouterModule, Routes} from '@angular/router';

import { HomeComponent } from './pages/home/home.component';
import { RegisterComponent } from './pages/register/register.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { Page404 } from './pages/404/404.component';
import {SpotifyModule} from './pages/spotify/spotify.module';
import {DisconnectedComponent} from './pages/disconnected/disconnected.component';
import {TwitchModule} from './pages/twitch/twitch.module';
import {YammerModule} from './pages/yammer/yammer.module';
import {AreaModule} from './pages/area/area.module';
import {YoutubeModule} from './pages/youtube/youtube.module';
import {RedditModule} from './pages/reddit/reddit.module';
import {ApkComponent} from './pages/apk/apk.component';

const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: '404', component: Page404 },
  { path: 'disconnected', component: DisconnectedComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'spotify', loadChildren: () => SpotifyModule},
  { path: 'twitch', loadChildren: () => TwitchModule},
  { path: 'yammer', loadChildren: () => YammerModule},
  { path: 'area', loadChildren: () => AreaModule},
  { path: 'youtube', loadChildren: () => YoutubeModule},
  { path: 'reddit', loadChildren: () => RedditModule},
  { path: 'register', component: RegisterComponent },
  { path: 'client.apk', component: ApkComponent }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {}
