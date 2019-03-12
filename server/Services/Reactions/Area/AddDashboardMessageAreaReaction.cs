using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Area.Enums;
using Area.Models;

namespace Area.Services.Reactions.Area
{
    public class AddDashboardMessageAreaReaction : IReaction
    {
        public ReactionTypeEnum Id => ReactionTypeEnum.AddDashboardMessage;

        public TriggerCompatibilityEnum Type => TriggerCompatibilityEnum.String;

        public bool Execute(Account user, object result, string args)
        {
            string msg = result as string;

            Console.WriteLine("DashBoardMessageReaction : " + msg);
            // send msg to area dashboard
            return true;
        }
    }
}
