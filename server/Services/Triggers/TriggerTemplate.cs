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
    public class TriggerTemplate<T, F> : ITrigger where T : IAction where F : IReaction 
    {
        private readonly IService _service;

        private readonly IAction _action;
        private readonly IReaction _reaction;

        public TriggerTemplate(
            IService service)
        {
            _service = service;
            _action = (IAction)Activator.CreateInstance(typeof(T));
            _reaction = (IReaction)Activator.CreateInstance(typeof(F));
        }

        public string Id => $"{nameof(_action)} and {nameof(_reaction)}";

        public bool TryActivate(Account user)
        {
            _action.CheckAction( user);
            if (_action.IsTriggered())
                return _reaction.Execute(user, _action.GetResult());
            return false;
        }
    }
}
