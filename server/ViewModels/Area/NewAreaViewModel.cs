using Area.Enums;
using Area.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area.ViewModels.Area
{
    public class NewAreaViewModel : ConnectedViewModel
    {
        public ActionTypeEnum ActionId { get; set; }
        public ReactionTypeEnum ReactionId { get; set; }
    }
}
