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
        bool TryActivate();
        string GetTriggerID();
    }
}
