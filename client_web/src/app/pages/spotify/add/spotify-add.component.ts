import {Component, OnInit} from '@angular/core';
import {SpotifyService} from '../spotify.service';
import {Account} from '../../../models/account';
import {Router} from '@angular/router';

@Component({
  selector: 'app-spotify-add',
  templateUrl: './spotify-add.component.html',
  styleUrls: ['./spotify-add.component.css']
})
export class SpotifyAddComponent implements OnInit {
  account: Account;

  constructor(
    private spotifyService: SpotifyService,
    private router: Router) {

  }

  ngOnInit() {
    this.account = JSON.parse(localStorage.getItem('account')) as Account;
    if (this.account == null) {
      this.router.navigate(['/disconnected']);
      return;
    }
  }
}
