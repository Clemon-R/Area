import { Component, OnInit } from '@angular/core';
import {LayoutService} from './layout.service';
import {Account} from '../models/account';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css'],
  providers: [LayoutService]
})
export class LayoutComponent implements OnInit {
  account: Account;

  constructor(private layoutService: LayoutService) { }

  ngOnInit() {

  }

  public disconnect() {
    console.log('Trying to disconnect...')
    console.log(document.cookie);
    if (document.cookie != null) {
      this.account = new Account();
      this.account.token = document.cookie;
      this.account = this.layoutService.getAccount(this.account);
    }
  }
}
