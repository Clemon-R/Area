import { Component, OnInit } from '@angular/core';
import {LayoutService} from './layout.service';
import {Account} from '../models/account';
import {ResultViewModel} from '../viewModels/ResultViewModel';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css'],
  providers: [LayoutService]
})
export class LayoutComponent implements OnInit {
  account: Account;

  public connected: boolean;
  username: string;
  password: string;

  constructor(private layoutService: LayoutService) {
    this.username = '';
    this.password = '';
    this.connected = false;
    this.account = JSON.parse(localStorage.getItem('account')) as Account;
    if (this.account)
      this.connected = true;
    if (this.account == null && document.cookie) {
      console.log('Token found: ' + document.cookie);
      this.account = new Account();
      this.account.token = document.cookie.split(';')[0].trim();
      if (this.account.token && this.account.token !== 'null') {
        console.log('Getting account...');
        this.layoutService.getAccount(this.account).then(
          (result: Account) => {
            this.account = result;
            if (!this.account) {
              document.cookie = null;
              localStorage.clear();
            } else {
              this.connected = true;
              localStorage.setItem('account', JSON.stringify(this.account));
              console.log('Account loaded');
              console.log(this.account);
            }
          }
        );
      } else {
        this.account = null;
        localStorage.clear();
      }
    }
  }

  ngOnInit() {
  }

  public disconnect() {
    console.log('Trying to disconnect...');
    if (this.connected && this.account) {
      this.layoutService.disconnectAccount(this.account).then(
        (result: ResultViewModel) => {
          if (result.success) {
            this.connected = false;
            document.cookie = null;
            localStorage.clear();
            console.log('Disconnected successfully');
          }
        }
      );
    }
  }

  public connect() {
    console.log('Trying to connect...');
    this.layoutService.connectAccount(this.username, this.password).then(
      (result: Account) => {
        this.account = result;
        if (result) {
          document.cookie = result.token;
          console.log('Cookie content: ' + document.cookie);
          if (document.cookie) {
            this.connected = true;
          }
        }
      }
    );
  }
}
