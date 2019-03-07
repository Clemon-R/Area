using Area.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area.ViewModels.Area
{
    public class TriggerViewModel : SuccessViewModel
    {
        public int Id { get; set; }
        public ActionTypeEnum ActionId { get; set; }
        public ReactionTypeEnum ReactionId { get; set; }
    }
}
