import { Component, OnInit } from '@angular/core';
import {DashboardService} from './dashboard.service';
import {Account} from '../../models/account';
import {Router} from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  account: Account;

  constructor(private dashboardService: DashboardService,
              private router: Router) { }

  ngOnInit() {
    this.account = JSON.parse(localStorage.getItem('account')) as Account;
    if (this.account == null) {
      this.router.navigate(['/disconnected']);
      return;
    }
  }
}
