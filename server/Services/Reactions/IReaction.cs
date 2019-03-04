using Area.Enums;
using Area.Models;
using Area.Services.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area.Services.Reactions
{
    public interface IReaction
    {
        TriggerTypeEnum Type { get; }
        bool Execute(Account user, object result, string args);
    }
}
