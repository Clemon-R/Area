using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotifyAPI.Web.Models;

namespace Area.Services.Actions.Spotify
{
    public class FollowedPlaylistUpdatedSpotifyAction : IAction
    {
        public bool IsTriggered()
        {
            /*string currentUserId = "?";
            DateTime lastCheckDate = new DateTime();
            List<PlaylistTrack> newTracks = new List<PlaylistTrack>();
            // Check the last time this trigger was checked in the database, if value is null set it to current time
            //DateTime lastCheckDate =
            Paging<SimplePlaylist> playlists = SpotifyService.GetUserPlaylists();
            for (int i = 0; i < playlists.Items.Count; i++)
            {
                Paging<PlaylistTrack> tracks = SpotifyService.GetPlaylistTracks(playlists.Items[i].Id);

                for (int j = 0; j < tracks.Items.Count; j++)
                {
                    if (tracks.Items[j].AddedAt >= lastCheckDate && tracks.Items[j].AddedBy.Id != currentUserId)
                    {
                        newTracks.Add(tracks.Items[j]);
                    }
                }
            }*/
            return true;// newTracks.Count > 0;
        }
    }
}
