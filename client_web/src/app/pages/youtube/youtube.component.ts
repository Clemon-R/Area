import { Component, OnInit } from '@angular/core';
import {YoutubeService} from './youtube.service';

@Component({
  selector: 'app-spotify',
  templateUrl: './youtube.component.html',
  styleUrls: ['./youtube.component.css']
})
export class YoutubeComponent implements OnInit {

  constructor(private spotifyService: YoutubeService) { }

  ngOnInit() {
  }
}
