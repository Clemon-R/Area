using Area.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area.Helpers
{
    [AttributeUsage(AttributeTargets.Field)]
    public class DescriptionReactionAttribute : Attribute
    {
        public DescriptionReactionAttribute(string description, ServiceTypeEnum service, TriggerCompatibilityEnum comp)
        {
            Description = description;
            Compatibility = comp;
            Service = service;
        }
        public string Description { get; set; }
        public TriggerCompatibilityEnum Compatibility { get; set; }
        public ServiceTypeEnum Service { get; set; }
    }
}
