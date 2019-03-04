using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area.Wrappers.Models
{
    public class RequestFailedModel : IRequestStateModel
    {
        public bool Success => false;
        public string Error { get; set; }
    }
}
