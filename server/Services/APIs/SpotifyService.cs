using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SpotifyAPI.Web.Models;

namespace Area.Services.APIs
{
    public class SpotifyService
    {
        static string ClientId = "a42c9625d0534a27911d6d708531b4b9";
        static string SecretId = "94618a312bf14e478c0e7c77d29312be";

        public static FollowedArtists GetFollowedArtists()
        {
            SpotifyAPI.Web.SpotifyWebAPI api = new SpotifyAPI.Web.SpotifyWebAPI()
            {
                // get current user access token : AccessToken = ??,
                UseAuth = true
            };
            return api.GetFollowedArtists(SpotifyAPI.Web.Enums.FollowType.Artist, 50);
        }

        public static NewAlbumReleases GetNewReleases()
        {
            SpotifyAPI.Web.SpotifyWebAPI api = new SpotifyAPI.Web.SpotifyWebAPI()
            {
                // get current user access token : AccessToken = ??,
                UseAuth = true
            };
            return api.GetNewAlbumReleases("", 50);
        }

        public static Paging<SimplePlaylist> GetUserPlaylists()
        {
            SpotifyAPI.Web.SpotifyWebAPI api = new SpotifyAPI.Web.SpotifyWebAPI()
            {
                // get current user access token : AccessToken = ??,
                UseAuth = true
            };
            return api.GetUserPlaylists("?", 50);
        }

        public static Paging<PlaylistTrack> GetPlaylistTracks(string playlistId)
        {
            SpotifyAPI.Web.SpotifyWebAPI api = new SpotifyAPI.Web.SpotifyWebAPI()
            {
                // get current user access token : AccessToken = ??,
                UseAuth = true
            };
            return api.GetPlaylistTracks("?", playlistId);
        }

        public static void AddTracksToPlaylist(List<PlaylistTrack> tracks, string playlistId)
        {
            SpotifyAPI.Web.SpotifyWebAPI api = new SpotifyAPI.Web.SpotifyWebAPI()
            {
                // get current user access token : AccessToken = ??,
                UseAuth = true
            };
            List<string> trackUris = new List<string>();
            for (int i = 0; i < tracks.Count; i++)
            {
                trackUris.Add(tracks[i].Track.Uri);
            }
            api.AddPlaylistTracks("?", playlistId, trackUris);
        }

    }
}
