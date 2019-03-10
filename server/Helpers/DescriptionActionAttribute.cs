using Area.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area.Helpers
{
    [AttributeUsage(AttributeTargets.Field)]
    public class DescriptionActionAttribute : Attribute
    {
        public DescriptionActionAttribute(string description, ServiceTypeEnum service, TriggerCompatibilityEnum must, params TriggerCompatibilityEnum[] comp)
        {
            Description = description;
            Service = service;
            var tmp = new List<TriggerCompatibilityEnum>();
            tmp.Add(must);
            foreach (var compatibility in comp)
                tmp.Add(compatibility);
            Compatibilitys = tmp.ToArray();
        }
        public string Description { get; set; }
        public TriggerCompatibilityEnum[] Compatibilitys { get; set; }
        public ServiceTypeEnum Service { get; set; }
    }
}
