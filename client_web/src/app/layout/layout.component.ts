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

  connected: boolean;
  username: string;
  password: string;

  constructor(private layoutService: LayoutService) { }

  async ngOnInit() {
    this.username = '';
    this.password = '';
    this.connected = false;
    if (document.cookie) {
      console.log('Token found: ' + document.cookie);
      this.account = new Account();
      this.account.token = document.cookie;
      console.log('Getting account...');
      this.account = await this.layoutService.getAccount(this.account);
      if (this.account) {
        this.connected = true;
        console.log('Account loaded');
        console.log(this.account);
      }
    }
  }

  public async disconnect() {
    console.log('Trying to disconnect...');
    if (this.connected && this.account) {
      const result = await this.layoutService.disconnectAccount(this.account);
      if (result.success) {
        this.connected = false;
        document.cookie = null;
        console.log('Disconnected successfully');
      }
    }
  }

  public async connect() {
    console.log('Trying to connect...');
    const result: Account = await this.layoutService.connectAccount(this.username, this.password);
    this.account = result;
    console.log(this.account);
    if (result) {
      document.cookie = result.token;
      console.log('Cookie content: ' + document.cookie);
      if (document.cookie) {
        this.connected = true;
      }
    }
  }
}
