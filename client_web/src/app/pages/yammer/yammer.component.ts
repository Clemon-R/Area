import { Component, OnInit } from '@angular/core';
import {YammerService} from './yammer.service';

@Component({
  selector: 'app-spotify',
  templateUrl: './yammer.component.html',
  styleUrls: ['./yammer.component.css']
})
export class YammerComponent implements OnInit {

  constructor(private spotifyService: YammerService) { }

  ngOnInit() {
  }
}
