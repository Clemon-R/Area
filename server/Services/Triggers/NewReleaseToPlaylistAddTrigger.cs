using Area.Services.Actions.Spotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area.Services.Triggers
{
    public class NewReleaseToPlaylistAddTrigger : ITrigger
    {
        static string _id = "NewReleaseToPlaylist";
        SpotifyService _service;

        NewReleaseToPlaylistAddTrigger(SpotifyService service)
        {
            _service = service;
        }

        public string GetTriggerID()
        {
            return _id;
        }

        public bool TryActivate()
        {
            FollowedArtistNewReleaseSpotifyAction action = new FollowedArtistNewReleaseSpotifyAction();

            action.CheckReleases(_service);
            if (action.IsTriggered())
            {
                new AddToPlaylistSpotifyReaction().AddTracksToPlaylist(_service.GetTracksFromAlbums(action.GetFollowedNewReleases()));
                return true;
            }
            return false;
        }
    }
}
