import { Account } from './models/account';
import {AccountResultViewModel} from './viewModels/AccountResultViewModel';

export class EntityFiller {
  public static FillAccount(model: AccountResultViewModel): Account {
    const result: Account = new Account();
    result.token = model.token;
    result.userName = model.username;
    return result;
  }
}
