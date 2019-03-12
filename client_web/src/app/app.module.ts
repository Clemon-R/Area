import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { FileSaverModule } from 'ngx-filesaver';

import { HttpClientModule } from '@angular/common/http';

import { LayoutComponent } from './layout/layout.component';
import { AppComponent } from './app.component';

import { HomeComponent } from './pages/home/home.component';
import { RegisterComponent } from './pages/register/register.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { Page404 } from './pages/404/404.component';

import { AppRoutingModule } from './app-routing.module';
import {DisconnectedComponent} from './pages/disconnected/disconnected.component';
import {ApkComponent} from './pages/apk/apk.component';

@NgModule({
  declarations: [
    AppComponent,
    LayoutComponent,
    HomeComponent,
    RegisterComponent,
    DashboardComponent,
    Page404,
    DisconnectedComponent,
    ApkComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    RouterModule,
    HttpClientModule,
    AppRoutingModule,
    FileSaverModule
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
