using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area.Wrappers.Models
{
    public class RequestSuccessModel : IRequestStateModel
    {
        public bool Success { get => true;}
    }
}
