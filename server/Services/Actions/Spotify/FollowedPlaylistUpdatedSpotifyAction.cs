using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotifyAPI.Web.Models;
using Area.Models;
using Area.Services.App;
using Area.Enums;
using Area.Helpers;
using Area.Graphs.Spotify;
using Area.Wrappers.Spotify.Models;

namespace Area.Services.Actions.Spotify
{
    public class FollowedPlaylistUpdatedSpotifyAction : IAction
    {
        private readonly SpotifyWrapper _spotifyWrapper;
        private readonly SpotifyService _spotifyService;
        private List<PlaylistTrack> _newTracks;
        
        public TriggerCompatibilityEnum Type { get; private set; }

        public ActionTypeEnum Id => ActionTypeEnum.FollowedPlaylistUpdatedSpotify;


        public FollowedPlaylistUpdatedSpotifyAction(IServiceProvider serviceProvider)
        {
            _spotifyWrapper = (SpotifyWrapper)serviceProvider.GetService(typeof(SpotifyWrapper));
            _spotifyService = (SpotifyService)serviceProvider.GetService(typeof(SpotifyService));
            Type = Id.GetAttributeOfType<DescriptionActionAttribute>().Compatibilitys[0];
        }

        public void CheckAction(Account user)
        {
            var api = _spotifyService.GetSpotifyWebApi(_spotifyService.GetSpotifyToken(user));
            string currentUserId = (_spotifyWrapper.GetSpotifyProfile(_spotifyService.GetSpotifyToken(user)) as SpotifyProfileModel).Id;
            var lastCheck = user.LastVerificationDate;
            Paging<SimplePlaylist> playlists = _spotifyService.GetUserPlaylists(api, user);
            if (playlists == null)
                return;
            _newTracks = new List<PlaylistTrack>();
            for (int i = 0; i < playlists.Items.Count; i++)
            {
                Paging<PlaylistTrack> tracks = _spotifyService.GetPlaylistTracks(api, playlists.Items[i].Id, user);

                for (int j = 0; j < tracks.Items.Count; j++)
                {
                    if (tracks.Items[j].AddedAt >= lastCheck && tracks.Items[j].AddedBy.Id != currentUserId)
                    {
                        _newTracks.Add(tracks.Items[j]);
                    }
                }
            }
        }

        public object GetResult()
        {
            return _newTracks;
        }

        public bool IsTriggered()
        {
            return _newTracks.Count > 0;
        }
    }
}
