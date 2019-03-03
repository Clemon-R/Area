using Area.Services.Actions.Spotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area.Services.Triggers
{
    public class NewReleaseToPlaylistAddTrigger : ITrigger
    {
        public bool TryActivate()
        {
            FollowedArtistNewReleaseSpotifyAction action = new FollowedArtistNewReleaseSpotifyAction();
            throw new NotImplementedException();
        }
    }
}
