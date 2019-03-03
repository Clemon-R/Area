import {Component, OnInit} from '@angular/core';
import {SpotifyService} from '../spotify.service';
import {ActivatedRoute, Params} from '@angular/router';
import {SpotifyTokenApi} from '../../../viewModels/spotify/SpotifyTokenApi';

@Component({
  selector: 'app-spotify-callback',
  templateUrl: './spotify-callback.component.html',
  styleUrls: ['./spotify-callback.component.css']
})
export class SpotifyCallbackComponent implements OnInit {
  code: string;

  constructor(private spotifyService: SpotifyService, private route: ActivatedRoute) {
  }

  ngOnInit() {
    console.log('Trying to find the code...');
    this.route.queryParams.subscribe(params => {
      if (!params.hasOwnProperty('code'))
        return;
      this.code = params.code;
      console.log('Code: ' + this.code);
      if (this.code) {
        this.spotifyService.getToken(this.code).then(
          (result: SpotifyTokenApi) => {
            console.log(result);
          }
        );
      }
    });
  }
}
