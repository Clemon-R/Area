import { Component, OnInit } from '@angular/core';
import {RedditService} from './reddit.service';

@Component({
  selector: 'app-spotify',
  templateUrl: './reddit.component.html',
  styleUrls: ['./reddit.component.css']
})
export class RedditComponent implements OnInit {

  constructor(private spotifyService: RedditService) { }

  ngOnInit() {
  }
}
