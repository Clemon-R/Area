using Area.Models;
using Area.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area
{
    public class ViewModelFiller
    {
        public AccountViewModel FillAccount(Account current)
        {
            var result = new AccountViewModel()
            {
                Token = current.Token,
                Username = current.UserName
            };
            return result;
        }
    }
}
