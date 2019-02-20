using Area.Services.App;
using Area.ViewModels;
using Area.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("login")]
        public IViewModel Login([FromBody] LoginViewModel model)
        {
            return _accountService.Login(model);
        }

        [HttpGet("logout")]
        public void Logout([FromBody] IConnectedViewModel model)
        {
            var account = _accountService.GetAccount(model);
            _accountService.Logout(account);
        }
    }
}
