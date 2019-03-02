using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area.ViewModels.Account
{
    public class AccountViewModel : SuccessViewModel
    {
        public string Token { get; set; }
        public string Username { get; set; }
    }
}
