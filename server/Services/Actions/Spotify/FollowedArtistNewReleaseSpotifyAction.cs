using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SpotifyAPI.Web.Models;
using Area.Services.APIs;

namespace Area.Services.Actions.Spotify
{
    public class FollowedArtistNewReleaseSpotifyAction : IAction
    {
        private List<SimpleAlbum> _newReleases = new List<SimpleAlbum>();

        public void CheckReleases(SpotifyService service)
        {
            FollowedArtists followedArtists = SpotifyService.GetFollowedArtists();
            NewAlbumReleases releases = SpotifyService.GetNewReleases();
            List<SimpleAlbum> followedReleases = new List<SimpleAlbum>();
            for (int i = 0; i < followedArtists.Artists.Items.Count; i++)
            {
                for (int j = 0; j < releases.Albums.Items.Count; j++)
                {
                    for (int k = 0; k < releases.Albums.Items[j].Artists.Count; k++)
                    {
                        if (followedArtists.Artists.Items[i].Id == releases.Albums.Items[j].Artists[k].Id
                       && lastCheck < SpotifyService.GetDateFromString(releases.Albums.Items[j].ReleaseDate, releases.Albums.Items[j].ReleaseDatePrecision))
                        {
                            followedReleases.Add(releases.Albums.Items[j]);
                        }
                    }
                }
            }
        }

        public bool IsTriggered()
        {
            return _newReleases.Count != 0;
        }
    }
}
