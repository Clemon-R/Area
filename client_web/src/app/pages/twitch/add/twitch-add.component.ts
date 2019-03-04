import {Component, OnInit} from '@angular/core';
import {TwitchService} from '../twitch.service';
import {Account} from '../../../models/account';
import {Router} from '@angular/router';
import {ResultViewModel} from '../../../viewModels/ResultViewModel';

@Component({
  selector: 'app-spotify-add',
  templateUrl: './twitch-add.component.html',
  styleUrls: ['./twitch-add.component.css']
})
export class TwitchAddComponent implements OnInit {
  account: Account;
  connected: boolean;

  constructor(
    private twitchService: TwitchService,
    private router: Router) {
    this.connected = false;
  }

  ngOnInit() {
    this.account = JSON.parse(localStorage.getItem('account')) as Account;
    if (this.account == null) {
      this.router.navigate(['/disconnected']);
      return;
    }
  }
}
