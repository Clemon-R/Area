import {Component, OnInit} from '@angular/core';
import {SpotifyService} from '../spotify.service';
import {Account} from '../../../models/account';
import {Router} from '@angular/router';

@Component({
  selector: 'app-spotify-all',
  templateUrl: './spotify-all.component.html',
  styleUrls: ['./spotify-all.component.css']
})
export class SpotifyAllComponent implements OnInit {
  account: Account;
  constructor(
    private router: Router,
    private spotifyService: SpotifyService) {

  }

  ngOnInit() {
    this.account = JSON.parse(localStorage.getItem('account')) as Account;
    if (this.account == null) {
      this.router.navigate(['/404']);
      return;
    }
  }
}
