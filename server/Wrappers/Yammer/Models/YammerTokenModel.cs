using Area.Wrappers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area.Wrappers.Yammer.Models
{
    public class YammerTokenModel : RequestSuccessModel
    {
        public string Token { get; set; }
        public string Expires_At { get; set; }
    }
}
