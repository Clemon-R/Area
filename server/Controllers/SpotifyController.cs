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
    public class SpotifyController : ControllerBase
    {
        private readonly SpotifyService _spotifyService;
        private readonly AccountService _accountService;

        public SpotifyController(
            AccountService accountService,
            SpotifyService spotifyService)
        {
            _spotifyService = spotifyService;
            _accountService = accountService;
        }

        [HttpPost("token/{code}")]
        public IViewModel GenerateNewToken(string code, [FromBody] ConnectedViewModel model)
        {
            var account = _accountService.GetAccount(model);
            return _spotifyService.GenerateSpotifyToken(account, code);
        }

        [HttpGet("login/{code}")]
        public IViewModel GenerateNewToken(string code)
        {
            return _spotifyService.ConnectToAccount(code);
        }

        [HttpPost("available")]
        public IViewModel IsAvailable([FromBody] ConnectedViewModel model)
        {
            var account = _accountService.GetAccount(model);
            return _spotifyService.IsSpotifyTokenAvailable(account);
        }
    }
}
