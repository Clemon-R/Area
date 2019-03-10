using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotifyAPI.Web.Models;
using Area.Models;
using Area.Services.Actions;
using Area.Services.App;
using Area.Services.Actions.Spotify;
using Area.Enums;
using Area.Helpers;

namespace Area.Services.Reactions.Spotify
{
    public class AddToPlaylistSpotifyReaction : IReaction
    {
        private readonly SpotifyService _spotifyService;

        public TriggerCompatibilityEnum Type { get; private set; }
        public ReactionTypeEnum Id => ReactionTypeEnum.AddToPlaylistSpotify;

        public AddToPlaylistSpotifyReaction(IServiceProvider serviceProvider)
        {
            _spotifyService = (SpotifyService)serviceProvider.GetService(typeof(SpotifyService));
            Type = Id.GetAttributeOfType<DescriptionReactionAttribute>().Compatibility;
        }

        public bool Execute(Account user, object result, string args)
        {
            var api = _spotifyService.GetSpotifyWebApi(_spotifyService.GetSpotifyToken(user));
            var tracks = result as List<SimpleTrack>;
            var playlists = _spotifyService.GetUserPlaylists(api, user);

            if (playlists == null || tracks == null)
                return false;
            for (int i = 0; i < playlists.Items.Count; i++)
            {
                if (playlists.Items[i].Name == "AreaPlaylist")
                {
                    Console.WriteLine("AreaPlaylist already exists, adding new track to it");
                    _spotifyService.AddTracksToPlaylist(api, tracks, playlists.Items[i].Id, user);
                    return true;
                }
            }
            FullPlaylist playlist = _spotifyService.CreatePlaylist(api, "AreaPlaylist", user);
            if (playlist != null)
            {
                Console.WriteLine("AreayPlaylist created and tracks added");
                _spotifyService.AddTracksToPlaylist(api, tracks, playlist.Id, user);
                return true;
            }
            Console.WriteLine("AreaPlaylist not found even after creating it");
            return false;
        }
    }
}
