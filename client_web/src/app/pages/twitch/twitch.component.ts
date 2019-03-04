import { Component, OnInit } from '@angular/core';
import {TwitchService} from './twitch.service';

@Component({
  selector: 'app-spotify',
  templateUrl: './twitch.component.html',
  styleUrls: ['./twitch.component.css']
})
export class TwitchComponent implements OnInit {

  constructor(private spotifyService: TwitchService) { }

  ngOnInit() {
  }
}
