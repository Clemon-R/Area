using Area.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area.ViewModels.Area
{
    public class ActionViewModel : SuccessViewModel
    {
        public string Description { get; set; }
        public TriggerCompatibilityEnum[] Compatibilitys { get; set; }
        public ServiceTypeEnum Service { get; set; }
        public int Id { get; set; }
    }
}
