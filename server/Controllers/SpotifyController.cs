using Area.Services.App;
using Area.ViewModels;
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
        public SpotifyController(SpotifyService spotifyService)
        {
            _spotifyService = spotifyService;
        }

        [HttpGet("token/{code}")]
        public IViewModel GetToken(string code)
        {
            Console.WriteLine($"Code: {code}");
            _spotifyService.GetSpotifyToken(code);
            return null;
        }
    }
}
