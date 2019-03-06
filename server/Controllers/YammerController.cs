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
    public class YammerController : ControllerBase
    {
        private readonly YammerService _yammerService;
        private readonly AccountService _accountService;

        public YammerController(
            AccountService accountService,
            YammerService yammerService)
        {
            _yammerService = yammerService;
            _accountService = accountService;
        }

        [HttpPost("token/{code}")]
        public IViewModel GenerateNewToken(string code, [FromBody] ConnectedViewModel model)
        {
            var account = _accountService.GetAccount(model);
            return _yammerService.GenerateToken(account, code);
        }

        [HttpPost("available")]
        public IViewModel IsAvailable([FromBody] ConnectedViewModel model)
        {
            var account = _accountService.GetAccount(model);
            return _yammerService.IsTokenAvailable(account);
        }

        [HttpPost("delete/token")]
        public IViewModel DeleteToken([FromBody] ConnectedViewModel model)
        {
            var account = _accountService.GetAccount(model);
            return _yammerService.DeleteToken(account, Enums.ServiceTypeEnum.Yammer);
        }
    }
}
