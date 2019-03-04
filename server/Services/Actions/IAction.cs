using Area.Enums;
using Area.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area.Services.Actions
{
    public interface IAction
    {
        void CheckAction(Account user);
        bool IsTriggered();
        TriggerTypeEnum Type { get; }
        object GetResult();
    }
}
