import {Component, OnInit} from '@angular/core';
import {AreaService} from '../area.service';
import {Account} from '../../../models/account';
import {Router} from '@angular/router';
import {ResultViewModel} from '../../../viewModels/ResultViewModel';
import {SpotifyService} from '../../spotify/spotify.service';
import {TwitchService} from '../../twitch/twitch.service';
import {YammerService} from '../../yammer/yammer.service';
import {ActionReactionViewModel} from '../../../viewModels/area/ActionReactionViewModel';
import {Action} from 'rxjs/internal/scheduler/Action';

@Component({
  selector: 'app-spotify-add',
  templateUrl: './area-add.component.html',
  styleUrls: ['./area-add.component.css']
})
export class AreaAddComponent implements OnInit {
  account: Account;
  spotifyConnected: boolean;
  twitchConnected: boolean;
  yammerConnected: boolean;

  reactionConnected: boolean;

  actions: ActionReactionViewModel[];
  reactions: ActionReactionViewModel[];
  validReactions: ActionReactionViewModel[];

  actionId: number;
  reactionId: number;

  constructor(
    private areaService: AreaService,
    private spotifyService: SpotifyService,
    private twitchService: TwitchService,
    private yammerService: YammerService,
    private router: Router) {
    this.spotifyConnected = false;
    this.twitchConnected = false;
    this.yammerConnected = false;
    this.reactionConnected = false;
    this.validReactions = [];
    this.areaService.getActions().then(
      (result: ActionReactionViewModel[]) => {
        this.actions = result;

        this.areaService.getReactions().then(
          (data: ActionReactionViewModel[]) => {
            this.reactions = data;
            this.ActionChanged(this.actions[0].id);
          }
        );
      }
    );
  }

  ngOnInit() {
    this.account = JSON.parse(localStorage.getItem('account')) as Account;
    if (this.account == null) {
      this.router.navigate(['/disconnected']);
      return;
    }
    this.spotifyService.isTokenAvailable(this.account).then(
      (result: ResultViewModel) => {
        this.spotifyConnected = result.success;
      }
    );
    this.twitchService.isTokenAvailable(this.account).then(
      (result: ResultViewModel) => {
        this.twitchConnected = result.success;
      }
    );
    this.yammerService.isTokenAvailable(this.account).then(
      (result: ResultViewModel) => {
        this.yammerConnected = result.success;
      }
    );
  }

  public ActionChanged(id: number) {
    console.log('Nouvelle action: ' + id);
    let tmp: ActionReactionViewModel = null;
    for (const action of this.actions) {
      if (action.id == id) {
        tmp = action;
        break;
      }
    }
    if (tmp === null)
      return;
    console.log('Action: ' + tmp.description);
    const result: ActionReactionViewModel[] = [];
    for (const reaction of this.reactions) {
      if (reaction.compatibility == -1 || reaction.compatibility == tmp.compatibility) {
        result.push(reaction);
      }
    }
    console.log(result);
    this.validReactions = result;
    this.reactionConnected = result.length != 0;
    this.actionId = tmp.id;
    if (this.validReactions)
      this.ReactionChanged(this.validReactions[0].id);
  }

  public ReactionChanged(id: number) {
    console.log('Nouvelle reaction: ' + id);
    let tmp: ActionReactionViewModel = null;
    for (const reaction of this.reactions) {
      if (reaction.id == id) {
        tmp = reaction;
        break;
      }
    }
    if (tmp === null)
      return;
    console.log('Reaction: ' + tmp.description);
    this.reactionId = tmp.id;
  }

  public Save() {
    console.log('Saving new area...');
    this.areaService.newArea(this.account, this.actionId, this.reactionId).then(
      (result: ResultViewModel) => {
        if (result.success)
          console.log('Saved');
      }
    );
  }
  public DeleteSotifyToken() {
    console.log('Deleting Spotify token...');
    this.spotifyService.deleteToken(this.account).then(
      (result: ResultViewModel) => {
        if (result.success) {
          this.spotifyConnected = false;
          console.log('Deleted');
        }
      }
    );
  }
  public DeleteTwitchToken() {
    console.log('Deleting Twitch token...');
    this.twitchService.deleteToken(this.account).then(
      (result: ResultViewModel) => {
        if (result.success) {
          this.twitchConnected = false;
          console.log('Deleted');
        }
      }
    );
  }
  public DeleteYammerToken() {
    console.log('Deleting Yammer token...');
    this.yammerService.deleteToken(this.account).then(
      (result: ResultViewModel) => {
        if (result.success) {
          this.yammerConnected = false;
          console.log('Deleted');
        }
      }
    );
  }
}
