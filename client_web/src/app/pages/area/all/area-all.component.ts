import {Component, OnInit} from '@angular/core';
import {AreaService} from '../area.service';
import {Account} from '../../../models/account';
import {Router} from '@angular/router';

@Component({
  selector: 'app-spotify-all',
  templateUrl: './area-all.component.html',
  styleUrls: ['./area-all.component.css']
})
export class AreaAllComponent implements OnInit {
  account: Account;
  constructor(
    private router: Router,
    private spotifyService: AreaService) {

  }

  ngOnInit() {
    this.account = JSON.parse(localStorage.getItem('account')) as Account;
    if (this.account == null) {
      this.router.navigate(['/disconnected']);
      return;
    }
  }
}
