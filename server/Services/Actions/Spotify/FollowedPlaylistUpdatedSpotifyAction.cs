using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotifyAPI.Web.Models;
using Area.Services.APIs;
using Area.Models;
using Area.Services.App;
using Area.Enums;

namespace Area.Services.Actions.Spotify
{
    public class FollowedPlaylistUpdatedSpotifyAction : IAction
    {
        private readonly SpotifyService _spotifyService;
        private List<PlaylistTrack> _newTracks = new List<PlaylistTrack>();

        public TriggerTypeEnum Type => TriggerTypeEnum.ListPlaylistTrack;


        public FollowedPlaylistUpdatedSpotifyAction(SpotifyService spotifyService)
        {
            _spotifyService = spotifyService;
        }

        public void CheckAction(Account user)
        {
            var api = _spotifyService.GetSpotifyWebApi(_spotifyService.GetSpotifyToken(user));
            string currentUserId = "?";
            DateTime lastCheckDate = new DateTime();
            List<PlaylistTrack> newTracks = new List<PlaylistTrack>();
            // Check the last time this trigger was checked in the database, if value is null set it to current time
            //DateTime lastCheckDate =
            Paging<SimplePlaylist> playlists = _spotifyService.GetUserPlaylists(api);
            for (int i = 0; i < playlists.Items.Count; i++)
            {
                Paging<PlaylistTrack> tracks = _spotifyService.GetPlaylistTracks(api, playlists.Items[i].Id);

                for (int j = 0; j < tracks.Items.Count; j++)
                {
                    if (tracks.Items[j].AddedAt >= lastCheckDate && tracks.Items[j].AddedBy.Id != currentUserId)
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
