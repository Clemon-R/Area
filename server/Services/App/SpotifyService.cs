using Area.Graphs.Spotify;
using Area.Models;
using Area.ViewModels;
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
        private readonly ApplicationDbContext _context;

        public SpotifyService(SpotifyWrapper spotifyWrapper, ApplicationDbContext context)
        {
            _spotifyWrapper = spotifyWrapper;
            _context = context;
        }

        public IViewModel GetSpotifyToken(Account owner, string code)
        {
            Console.WriteLine($"SpotifyService(GetSpotifyToken): The user code is {code}");
            var result = _spotifyWrapper.GetSpotifyToken(code);
            if (!result.Success)
            {
                Console.WriteLine("SpotifyService(GetSpotifyToken): Failed to get token");
                return new ErrorViewModel() { Error = (result as SpotifyFailedModel).Error};
            }
            _context.Tokens.RemoveRange(_context.Tokens.Where(t => t.Owner.Id == owner.Id && t.Type == Enums.ServiceTypeEnum.Spotify));
            SpotifyTokenModel tokenModel = result as SpotifyTokenModel;
            var token = new Token()
            {
                AccessToken = tokenModel.access_token,
                RefreshToken = tokenModel.refresh_token,
                ExpireIn = tokenModel.expires_in,
                Owner = owner,
                Type = Enums.ServiceTypeEnum.Spotify
            };
            _context.Tokens.Add(token);
            _context.SaveChanges();
            Console.WriteLine("SpotifyService(GetSpotifyToken): Token successfully saved");
            return new SuccessViewModel();
        }
    }
}
