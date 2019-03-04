import {Component, OnInit} from '@angular/core';
import {YammerService} from '../yammer.service';
import {Account} from '../../../models/account';
import {Router} from '@angular/router';
import {ResultViewModel} from '../../../viewModels/ResultViewModel';

@Component({
  selector: 'app-spotify-add',
  templateUrl: './yammer-add.component.html',
  styleUrls: ['./yammer-add.component.css']
})
export class YammerAddComponent implements OnInit {
  account: Account;
  connected: boolean;

  constructor(
    private twitchService: YammerService,
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
