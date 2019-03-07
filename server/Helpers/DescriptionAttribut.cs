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
        public DescriptionAttribut(string description, TriggerCompatibilityEnum comp, ServiceTypeEnum service)
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
