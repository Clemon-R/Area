import {RouterModule, Routes} from '@angular/router';
import {NgModule} from '@angular/core';

import {AreaComponent} from './area.component';
import {AreaAddComponent} from './add/area-add.component';
import {AreaAllComponent} from './all/area-all.component';

const spotifyRoutes: Routes = [
  { path: '', component: AreaComponent },
  { path: 'add', component: AreaAddComponent },
  { path: 'all', component: AreaAllComponent }
];

@NgModule({
  imports: [
    RouterModule.forChild(spotifyRoutes)
  ],
  exports: [
    RouterModule
  ]
})

export class AreaRoutingModule { }
