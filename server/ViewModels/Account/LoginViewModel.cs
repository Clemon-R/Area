using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area.ViewModels.Account
{
    public class LoginViewModel : IViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
