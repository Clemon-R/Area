import {Component, OnInit} from '@angular/core';
import {YammerService} from '../yammer.service';
import {Account} from '../../../models/account';
import {Router} from '@angular/router';

@Component({
  selector: 'app-spotify-all',
  templateUrl: './yammer-all.component.html',
  styleUrls: ['./yammer-all.component.css']
})
export class YammerAllComponent implements OnInit {
  account: Account;
  constructor(
    private router: Router,
    private twitchService: YammerService) {

  }

  ngOnInit() {
    this.account = JSON.parse(localStorage.getItem('account')) as Account;
    if (this.account == null) {
      this.router.navigate(['/disconnected']);
      return;
    }
  }
}
