using Area.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area.Helpers
{
    [AttributeUsage(AttributeTargets.Field)]
    public class DescriptionAttribut : Attribute
    {
        public DescriptionAttribut(string description, TriggerCompatibilityEnum comp)
        {
            Description = description;
            Compatibility = comp;
        }
        public string Description { get; set; }
        public TriggerCompatibilityEnum Compatibility { get; set; }
    }
}
