using Area.Graphs.Spotify;
using Area.Wrappers.Spotify.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Area.Services.App
{
    public class SpotifyService : IService
    {
        private readonly SpotifyWrapper _spotifyWrapper;
        public SpotifyService(SpotifyWrapper spotifyWrapper)
        {
            _spotifyWrapper = spotifyWrapper;
        }

        public ISpotifyStateModel GetSpotifyToken(string code)
        {
            var result = _spotifyWrapper.GetSpotifyToken(code);
            if (!result.Success)
                Console.WriteLine("Failed to get token");
            return null;
        }
    }
}
