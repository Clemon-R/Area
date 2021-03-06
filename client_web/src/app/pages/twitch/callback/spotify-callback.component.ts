import {Component, OnInit} from '@angular/core';
import {TwitchService} from '../twitch.service';
import {ActivatedRoute, Params, Router} from '@angular/router';
import {LayoutService} from '../../../layout/layout.service';
import { Account } from '../../../models/account';
import {ResultViewModel} from '../../../viewModels/ResultViewModel';

@Component({
  selector: 'app-spotify-callback',
  templateUrl: './spotify-callback.component.html',
  styleUrls: ['./spotify-callback.component.css']
})
export class SpotifyCallbackComponent implements OnInit {
  code: string;
  account: Account;
  error: boolean;
  message: string;
  constructor(
    private layoutService: LayoutService,
    private twitchService: TwitchService,
    private route: ActivatedRoute,
    private router: Router) {
    this.error = false;
    this.message = 'Veuillez attendre un instant s\'il vous plez';
  }

  ngOnInit() {
    this.account = JSON.parse(localStorage.getItem('account')) as Account;
    if (this.account == null) {
      this.router.navigate(['/disconnected']);
      return;
    }
    console.log('Trying to find the code...');
    this.route.queryParams.subscribe(params => {
      if (!params.hasOwnProperty('code') || this.account == null) {
        return;
      }
      this.code = params.code;
      console.log('Code: ' + this.code);
      if (this.code) {
        this.twitchService.getToken(this.account, this.code).then(
          (result: ResultViewModel) => {
            if (result.success) {
              this.error = false;
              this.message = 'Votre compte à bien été connecté';
            } else {
              this.error = true;
              this.message = result.error;
            }
          },
          (error) => {
            this.error = true;
            this.message = 'Une erreur est survenue';
          }
        );
      }
    });
  }
}
