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
    public class TwitchController : ControllerBase
    {
        private readonly TwitchService _twitchService;
        private readonly AccountService _accountService;

        public TwitchController(
            AccountService accountService,
            TwitchService twitchService)
        {
            _twitchService = twitchService;
            _accountService = accountService;
        }

        [HttpPost("token/{code}")]
        public IViewModel GenerateNewToken(string code, [FromBody] ConnectedViewModel model)
        {
            var account = _accountService.GetAccount(model);
            return _twitchService.GenerateTwitchToken(account, code);
        }
    }
}
