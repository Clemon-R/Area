using Area.Services.App;
using Area.ViewModels;
using Area.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;

namespace Area.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedditController : ControllerBase
    {
        private readonly RedditService _redditService;
        private readonly AccountService _accountService;

        public RedditController(
            AccountService accountService,
            RedditService redditService)
        {
            _redditService = redditService;
            _accountService = accountService;
        }

        [HttpPost("token/{code}")]
        public IViewModel GenerateNewToken(string code, [FromBody] 	ConnectedViewModel model)
        {
            var account = _accountService.GetAccount(model);
            return _redditService.GenerateToken(account, code);
        }

        [HttpPost("available")]
        public IViewModel IsAvailable([FromBody] ConnectedViewModel model)
        {
            var account = _accountService.GetAccount(model);
            return _redditService.IsTokenAvailable(account);
        }

        [HttpPost("delete/token")]
        public IViewModel DeleteToken([FromBody] ConnectedViewModel model)
        {
            var account = _accountService.GetAccount(model);
            return _redditService.DeleteToken(account, Enums.ServiceTypeEnum.Yammer);
        }
    }
}