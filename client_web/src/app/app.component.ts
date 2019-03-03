import {Component, ViewChild} from '@angular/core';
import {LayoutComponent} from './layout/layout.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'area';

  @ViewChild('layoutComponent') layout: LayoutComponent;
}
