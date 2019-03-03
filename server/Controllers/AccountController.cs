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

        [HttpPost("get")]
        public IViewModel Get([FromBody] ConnectedViewModel model)
        {
            var account = _accountService.GetAccount(model);
            return _accountService.GetModel(account);
        }

        [HttpPost("register")]
        public IViewModel Register([FromBody] RegisterViewModel model)
        {
            return _accountService.Register(model);
        }

        [HttpPost("login")]
        public IViewModel Login([FromBody] LoginViewModel model)
        {
            return _accountService.Login(model);
        }

        [HttpPost("logout")]
        public IViewModel Logout([FromBody] ConnectedViewModel model)
        {
            var account = _accountService.GetAccount(model);
            return _accountService.Logout(account);
        }
    }
}
