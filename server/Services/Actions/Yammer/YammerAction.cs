using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Area.Enums;
using Area.Models;
using Microsoft;

namespace Area.Services.Actions.Yammer
{
    public class YammerAction : IAction
    {
        public TriggerCompatibilityEnum Type => throw new NotImplementedException();

        public ActionTypeEnum Id => throw new NotImplementedException();

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
