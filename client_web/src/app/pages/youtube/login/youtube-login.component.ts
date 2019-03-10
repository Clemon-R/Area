import {Component, Host, OnInit} from '@angular/core';
import {YoutubeService} from '../youtube.service';
import {ActivatedRoute, Params, Router} from '@angular/router';
import {LayoutService} from '../../../layout/layout.service';
import { Account } from '../../../models/account';
import {ResultViewModel} from '../../../viewModels/ResultViewModel';
import {LayoutComponent} from '../../../layout/layout.component';

@Component({
  selector: 'app-spotify-login',
  templateUrl: './youtube-login.component.html',
  styleUrls: ['./youtube-login.component.css']
})
export class YoutubeLoginComponent implements OnInit {
  code: string;
  account: Account;
  error: boolean;
  message: string;
  constructor(
    @Host() private layout: LayoutComponent,
    private layoutService: LayoutService,
    private spotifyService: YoutubeService,
    private route: ActivatedRoute,
    private router: Router) {
    this.error = true;
    this.message = 'Veuillez attendre un instant s\'il vous plez';
    this.account = null;
  }

  ngOnInit() {
    console.log('Trying to find the code...');
    this.route.queryParams.subscribe(params => {
      if (!params.hasOwnProperty('code')) {
        return;
      }
      this.code = params.code;
      console.log('Code: ' + this.code);
      if (this.code) {
        this.spotifyService.connectAccount(this.code).then(
          (result: Account) => {
            this.account = result;
            if (this.account) {
              this.error = false;
              localStorage.setItem('account', JSON.stringify(this.account));
              document.cookie = this.account.token;
              this.layout.connected = true;
              this.layout.account = this.account;
              this.message = 'Votre compte à bien été connecté';
            } else {
              this.error = true;
              this.message = 'Une erreur est survenue lors de votre connexion';
            }
          }
        );
      }
    });
  }
}
