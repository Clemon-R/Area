using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area.ViewModels.Account
{
    public class RegisterViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
