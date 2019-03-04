import {Component, OnInit} from '@angular/core';
import {TwitchService} from '../twitch.service';
import {Account} from '../../../models/account';
import {Router} from '@angular/router';

@Component({
  selector: 'app-spotify-all',
  templateUrl: './twitch-all.component.html',
  styleUrls: ['./twitch-all.component.css']
})
export class TwitchAllComponent implements OnInit {
  account: Account;
  constructor(
    private router: Router,
    private twitchService: TwitchService) {

  }

  ngOnInit() {
    this.account = JSON.parse(localStorage.getItem('account')) as Account;
    if (this.account == null) {
      this.router.navigate(['/disconnected']);
      return;
    }
  }
}
