import {Component, OnInit} from '@angular/core';
import {SpotifyService} from '../spotify.service';

@Component({
  selector: 'app-spotify-add',
  templateUrl: './spotify-add.component.html',
  styleUrls: ['./spotify-add.component.css']
})
export class SpotifyAddComponent implements OnInit {
  constructor(private spotifyService: SpotifyService) {

  }

  ngOnInit() {
  }
}
