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
      this.account = await this.layoutService.getAccount(this.account);
      this.connected = true;
    }
  }

  public async disconnect() {
    console.log('Trying to disconnect...');
    if (this.connected) {
      const result = await this.layoutService.disconnectAccount(this.account);
      if (result.succes) {
        this.connected = false;
        document.cookie = null;
      }
    }
  }

  public async connect() {
    console.log('Trying to connect...');
    const result: Account = await this.layoutService.connectAccount(this.username, this.password);
    document.cookie = result.token;
    console.log('Cookie content: ' + document.cookie);
    if (document.cookie) {
      this.connected = true;
    }
  }
}
