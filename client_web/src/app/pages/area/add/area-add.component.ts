/* tslint:disable:curly triple-equals */
import {Component, OnInit, ViewChild} from '@angular/core';
import {AreaService} from '../area.service';
import {Account} from '../../../models/account';
import {Router} from '@angular/router';
import {ResultViewModel} from '../../../viewModels/ResultViewModel';
import {SpotifyService} from '../../spotify/spotify.service';
import {TwitchService} from '../../twitch/twitch.service';
import {YammerService} from '../../yammer/yammer.service';
import {ReactionViewModel} from '../../../viewModels/area/ReactionViewModel';
import {Action} from 'rxjs/internal/scheduler/Action';
import {LayoutComponent} from '../../../layout/layout.component';
import {ActionViewModel} from '../../../viewModels/area/ActionViewModel';

@Component({
  selector: 'app-spotify-add',
  templateUrl: './area-add.component.html',
  styleUrls: ['./area-add.component.css']
})
export class AreaAddComponent implements OnInit {
  account: Account;
  services: any[];
  serviceTargeted: boolean[];
  serviceConnected: boolean[];
  submitPossible: boolean;

  reactionConnected: boolean;

  actions: ActionViewModel[];
  reactions: ReactionViewModel[];
  validReactions: ReactionViewModel[];

  action: ActionViewModel;
  reaction: ReactionViewModel;

  success: boolean;

  constructor(
    private areaService: AreaService,
    private spotifyService: SpotifyService,
    private twitchService: TwitchService,
    private yammerService: YammerService,
    private router: Router) {
    this.serviceConnected = [false, false, false];
    this.reactionConnected = false;
    this.validReactions = [];
    this.serviceTargeted = [false, false, false];
    this.action = null;
    this.reaction = null;
    this.submitPossible = false;
    this.success = false;
    this.account = JSON.parse(localStorage.getItem('account')) as Account;
    if (this.account == null) {
      this.router.navigate(['/disconnected']);
      return;
    }
    this.services = [this.spotifyService, this.twitchService, this.yammerService];
    for (let i = 0; i < this.services.length; i++)
      this.refreshTokenAvailable(i);
    this.areaService.getActions().then(
      (result: ActionViewModel[]) => {
        this.actions = result;

        this.areaService.getReactions().then(
          (data: ReactionViewModel[]) => {
            this.reactions = data;
            this.ActionChanged(this.actions[0].id);
          }
        );
      }
    );
  }

  ngOnInit() {
  }

  private refreshTokenAvailable(id: number) {
    console.log('Refresh check token for service: ' + id);
    this.services[id].isTokenAvailable(this.account).then(
      (result: ResultViewModel) => {
        this.serviceConnected[id] = result.success;
      }
    );
  }

  public ActionChanged(id: number) {
    console.log('New action: ' + id);
    let tmp: ActionViewModel = null;
    for (const action of this.actions) {
      if (action.id == id) {
        tmp = action;
        break;
      }
    }
    if (tmp === null)
      return;
    console.log('Action: ' + tmp.description);
    const result: ReactionViewModel[] = [];
    for (const reaction of this.reactions) {
      if (reaction.compatibility == -1 || tmp.compatibilitys.includes(reaction.compatibility)) {
        result.push(reaction);
      }
    }
    console.log('Reaction availables:');
    console.log(result);
    this.validReactions = result;
    this.reactionConnected = result.length != 0;
    if (this.action != null && (this.reaction == null || this.reaction.service != this.action.service))
      this.serviceTargeted[this.action.service] = false;
    this.action = tmp;
    this.serviceTargeted[this.action.service] = true;
    this.submitPossible = false;
    if (this.validReactions.length > 0) {
      console.log('Refresh first reaction...');
      this.reactionChanged(this.validReactions[0].id);
    } else if (this.reaction != null)
      this.serviceTargeted[this.reaction.service] = false;
  }

  public reactionChanged(id: number) {
    console.log('New reaction: ' + id);
    let tmp: ReactionViewModel = null;
    for (const reaction of this.reactions) {
      if (reaction.id == id) {
        tmp = reaction;
        break;
      }
    }
    if (tmp === null)
      return;
    console.log('Reaction: ' + tmp.description);
    if (this.reaction != null && this.reaction.service != this.action.service)
      this.serviceTargeted[this.reaction.service] = false;
    this.reaction = tmp;
    this.serviceTargeted[this.reaction.service] = true;
    this.submitPossible = this.serviceConnected[this.action.service] && this.serviceConnected[this.reaction.service];
  }

  public save() {
    console.log('Saving new AREA...');
    this.areaService.newArea(this.account, this.action.id, this.reaction.id).then(
      (result: ResultViewModel) => {
        this.success = result.success;
        if (result.success)
          console.log('AREA has been saved');
        else {
          console.log('Not saved');
          alert(result.error);
        }
      }
    );
  }
  public deleteToken(id: number) {
    console.log('Deleting token...');
    this.services[id].deleteToken(this.account).then(
      (result: ResultViewModel) => {
        if (result.success) {
          this.serviceConnected[id] = false;
          console.log('Deleted');
        } else
          console.log('Not deleted');
        this.refreshTokenAvailable(id);
      }
    );
  }
}
