using Area.Models;
using Area.Services.Actions;
using Area.Services.Actions.Spotify;
using Area.Services.App;
using Area.Services.Reactions;
using Area.Services.Reactions.Spotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area.Services.Triggers
{
    public class TriggerTemplate : ITrigger
    {
        private readonly IAction _action;
        private readonly IReaction _reaction;

        public TriggerTemplate(
            Type action,
            Type reaction)
        {
            _action = (IAction)Activator.CreateInstance(action);
            _reaction = (IReaction)Activator.CreateInstance(reaction);
        }

        public string Id => $"{nameof(_action)} and {nameof(_reaction)}";

        public bool TryActivate(Account user, string args)
        {
            _action.CheckAction( user);
            if (_action.IsTriggered())
                return _reaction.Execute(user, _action.GetResult(), args);
            return false;
        }
    }
}
