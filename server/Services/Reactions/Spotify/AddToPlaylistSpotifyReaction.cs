using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotifyAPI.Web.Models;
using Area.Services.APIs;
using Area.Models;
using Area.Services.Actions;
using Area.Services.App;
using Area.Services.Actions.Spotify;
using Area.Enums;

namespace Area.Services.Reactions.Spotify
{
    public class AddToPlaylistSpotifyReaction : IReaction
    {
        private readonly SpotifyService _spotifyService;
        public TriggerTypeEnum Type => TriggerTypeEnum.ListSimpleAlbum;

        public AddToPlaylistSpotifyReaction(SpotifyService spotifyService)
        {
            _spotifyService = spotifyService;
        }

        public bool Execute(Account user, object result, string args)
        {
            var api = _spotifyService.GetSpotifyWebApi(_spotifyService.GetSpotifyToken(user));
            var tracks = _spotifyService.GetTracksFromAlbums(api, result as List<SimpleAlbum>);
            _spotifyService.AddTracksToPlaylist(api, tracks, string.Empty);
            return true;
        }
    }
}
