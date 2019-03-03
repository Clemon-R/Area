import {Component, OnInit} from '@angular/core';
import {SpotifyService} from '../spotify.service';

@Component({
  selector: 'app-spotify-all',
  templateUrl: './spotify-all.component.html',
  styleUrls: ['./spotify-all.component.css']
})
export class SpotifyAllComponent implements OnInit {
  constructor(private spotifyService: SpotifyService) {

  }

  ngOnInit() {
  }
}
