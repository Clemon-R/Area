using Area.Services.App;
using Area.ViewModels;
using Area.ViewModels.Account;
using Area.ViewModels.Youtube;
using Microsoft.AspNetCore.Mvc;

namespace Area.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YoutubeController : ControllerBase
    {
        private readonly YoutubeService _youtubeService;
        private readonly AccountService _accountService;

        public YoutubeController(
            AccountService accountService,
            YoutubeService youtubeService)
        {
            _youtubeService = youtubeService;
            _accountService = accountService;
        }

        [HttpPost("token")]
        public IViewModel GenerateNewToken(string code, [FromBody] YoutubeTokenViewModel model)
        {
            var account = _accountService.GetAccount(model);
            return _youtubeService.GenerateToken(account, model.Code);
        }

        [HttpPost("available")]
        public IViewModel IsAvailable([FromBody] ConnectedViewModel model)
        {
            var account = _accountService.GetAccount(model);
            return _youtubeService.IsTokenAvailable(account);
        }

        [HttpPost("delete/token")]
        public IViewModel DeleteToken([FromBody] ConnectedViewModel model)
        {
            var account = _accountService.GetAccount(model);
            return _youtubeService.DeleteToken(account, Enums.ServiceTypeEnum.Yammer);
        }
    }
}