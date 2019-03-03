using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SpotifyAPI.Web.Models;

namespace Area.Services.Actions.Spotify
{
    public class FollowedArtistNewReleaseSpotifyAction : IAction
    {
        public bool IsTriggered()
        {
            /*FollowedArtists followedArtists = SpotifyService.GetFollowedArtists();
            NewAlbumReleases releases = SpotifyService.GetNewReleases();
            List<SimpleAlbum> followedReleases = new List<SimpleAlbum>();
            for (int i = 0; i < followedArtists.Artists.Items.Count; i++)
            {
                for (int j = 0; j < releases.Albums.Items.Count; j++)
                {
                    for (int k = 0; k < releases.Albums.Items[j].Artists.Count; k++)
                    {
                        if (followedArtists.Artists.Items[i].Id == releases.Albums.Items[j].Artists[k].Id)
                        {
                            followedReleases.Add(releases.Albums.Items[j]);
                        }
                    }
                }
            }*/
            return true;//followedReleases.Count != 0;
        }
    }
}
