using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Area.Enums;
using Area.Models;

namespace Area.Services.Actions.Steam
{
    public class NewObjectInInventorySteamAction : IAction
    {
        public ActionTypeEnum Id => throw new NotImplementedException();

        TriggerCompatibilityEnum IAction.Type => throw new NotImplementedException();

        public void CheckAction(Account user)
        {
            throw new NotImplementedException();
        }

        public object GetResult()
        {
            throw new NotImplementedException();
        }

        public bool IsTriggered()
        {
            throw new NotImplementedException();
        }
    }
}
