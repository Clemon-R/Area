import {Component, OnInit} from '@angular/core';
import {SpotifyService} from '../spotify.service';
import {Account} from '../../../models/account';
import {Router} from '@angular/router';
import {ResultViewModel} from '../../../viewModels/ResultViewModel';

@Component({
  selector: 'app-spotify-add',
  templateUrl: './spotify-add.component.html',
  styleUrls: ['./spotify-add.component.css']
})
export class SpotifyAddComponent implements OnInit {
  account: Account;
  connected: boolean;

  constructor(
    private spotifyService: SpotifyService,
    private router: Router) {
    this.connected = false;
  }

  ngOnInit() {
    this.account = JSON.parse(localStorage.getItem('account')) as Account;
    if (this.account == null) {
      this.router.navigate(['/disconnected']);
      return;
    }
    this.spotifyService.isTokenAvailable(this.account).then(
      (result: ResultViewModel) => {
        this.connected = result.success;
      }
    );
  }
}
