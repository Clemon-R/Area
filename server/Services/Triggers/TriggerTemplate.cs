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
        private readonly IServiceProvider _serviceProvider;

        public TriggerTemplate(
            Type action,
            Type reaction,
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _action = (IAction)Activator.CreateInstance(action, serviceProvider);
            _reaction = (IReaction)Activator.CreateInstance(reaction, serviceProvider);
        }

        public string Id => $"{_action.Id} and {_reaction.Id}";

        public bool TryActivate(Account user, string args)
        {
            _action.CheckAction( user);
            if (_action.IsTriggered())
            {
                Console.WriteLine($"{Id}({user.UserName}): Activate");
                object data = new ActionDataConverter.ActionDataConverter().Convert(_action.Type, _reaction.Type, _action.GetResult(), _serviceProvider, user);
                if (data != null)
                    return _reaction.Execute(user, _action.GetResult(), args);
            }
            return false;
        }
    }
}
