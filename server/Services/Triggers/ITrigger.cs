using Area.Models;
using Area.Services.Actions;
using Area.Services.Reactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area.Services.Triggers
{
    public interface ITrigger
    {
        bool TryActivate(Account user, string args, DateTime lastCheck);
        string Id { get; }
    }
}
