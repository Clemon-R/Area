/* tslint:disable:curly triple-equals */
import {Component, OnInit} from '@angular/core';
import {AreaService} from '../area.service';
import {Account} from '../../../models/account';
import {Router} from '@angular/router';
import {Trigger} from '../../../models/trigger';
import {ReactionViewModel} from '../../../viewModels/area/ReactionViewModel';
import {ResultViewModel} from '../../../viewModels/ResultViewModel';
import {ActionViewModel} from '../../../viewModels/area/ActionViewModel';

@Component({
  selector: 'app-spotify-all',
  templateUrl: './area-all.component.html',
  styleUrls: ['./area-all.component.css']
})
export class AreaAllComponent implements OnInit {
  account: Account;
  triggers: Trigger[];

  actions: ActionViewModel[];
  reactions: ReactionViewModel[];

  constructor(
    private router: Router,
    private areaService: AreaService) {
    this.triggers = [];
    this.account = JSON.parse(localStorage.getItem('account')) as Account;
    if (this.account == null) {
      this.router.navigate(['/disconnected']);
      return;
    }
    this.areaService.getActions().then(
      (actions) => {
        this.actions = actions;
        this.areaService.getReactions().then(
          (reactions) => {
            this.reactions = reactions;
            this.refreshTriggers();
          }
        );
      }
    );
  }

  ngOnInit() {
  }

  private refreshTriggers() {
    this.areaService.getTriggers(this.account).then(
      (result: Trigger[]) => {
        this.triggers = result;
      }
    );
  }

  public getActionDescriptionById(id: number): string {
    for (const action of this.actions) {
      if (action.id == id)
        return action.description;
    }
    return null;
  }

  public getReactionDescriptionById(id: number): string {
    for (const reaction of this.reactions) {
      if (reaction.id == id)
        return reaction.description;
    }
    return null;
  }

  public deleteTrigger(id: number) {
    console.log('Trying deleting trigger: ' + id);
    this.areaService.deleteTrigger(this.account, id).then(
      (result: ResultViewModel) => {
        if (result.success) {
          this.refreshTriggers();
          console.log('Trigger deleted');
        } else
          console.log('Not deleted');
      }
    );
  }
}
