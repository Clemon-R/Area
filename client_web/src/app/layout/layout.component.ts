import { Component, OnInit } from '@angular/core';
import {LayoutService} from './layout.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css'],
  providers: [LayoutService]
})
export class LayoutComponent implements OnInit {

  constructor(private layoutService: LayoutService) { }

  ngOnInit() {
  }
}
