import { FormsModule } from '@angular/forms';
import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {AreaComponent} from './area.component';
import {AreaAddComponent} from './add/area-add.component';
import {AreaRoutingModule} from './area-routing.module';
import {AreaAllComponent} from './all/area-all.component';

@NgModule({
  declarations: [
    AreaComponent,
    AreaAddComponent,
    AreaAllComponent
  ],
  imports: [
    CommonModule,
    AreaRoutingModule
  ],
  exports: [
    AreaComponent,
    AreaAddComponent,
    AreaAllComponent
  ]
})
export class AreaModule { }
