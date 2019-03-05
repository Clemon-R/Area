using Area.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area.ViewModels.Area
{
    public class ActionReactionViewModel : SuccessViewModel
    {
        public string Description { get; set; }
        public TriggerCompatibilityEnum Compatibility { get; set; }
        public int Id { get; set; }
    }
}
