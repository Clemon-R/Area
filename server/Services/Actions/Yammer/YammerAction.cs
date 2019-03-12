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
        private DateTime _lastTriggerDate;
        public ActionTypeEnum Id => throw new NotImplementedException();

        public void CheckAction(Account user, DateTime lastCheck)
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

        public DateTime GetDate()
        {
            return _lastTriggerDate;
        }
    }
}
