using Area.Enums;
using Area.Models;
using Area.Services.App;
using Google.Apis.YouTube.v3.Data;
using Reddit.Controllers;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Models;
using Steam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchLib.Api.Helix.Models.Users;

namespace Area.ActionDataConverter
{
    public class ActionDataConverter
    {
        public object Convert(TriggerCompatibilityEnum action, TriggerCompatibilityEnum reaction, object data, IServiceProvider serviceProvider, Models.Account user)
        {
            switch (reaction)
            {
                case TriggerCompatibilityEnum.ListSimpleTrack:
                    return ConverToListSimpleTracks(data, action, serviceProvider, user);
                case TriggerCompatibilityEnum.RedditPosts:
                    return ConvertToRedditPosts(data, action, serviceProvider, user);
                case TriggerCompatibilityEnum.RedditComments:
                    return ConvertToRedditComments(data, action, serviceProvider, user);
                case TriggerCompatibilityEnum.String:
                    return ConvertToString(data, action, serviceProvider, user);
                case TriggerCompatibilityEnum.ListTwitchUser:
                    return ConvertToListTwitchUser(data, action, serviceProvider, user);
                default:
                    return null;
            }
        }

        private object ConvertToListTwitchUser(object data, TriggerCompatibilityEnum action, IServiceProvider serviceProvider, Models.Account user)
        {
            Console.WriteLine("Convert to list twitch user");
            switch (action)
            {
                case TriggerCompatibilityEnum.ListTwitchUser:
                    return data;
                default:
                    return null;
            }
        }

        private object ConverToListSimpleTracks(object data, TriggerCompatibilityEnum action, IServiceProvider serviceProvider,Models.Account user)
        {
            Console.WriteLine("Convert to simple tracks list");
            switch (action)
            {
                case TriggerCompatibilityEnum.ListSimpleAlbum:
                    return ListAlbumToListTracks(data, serviceProvider, user);
                case TriggerCompatibilityEnum.ListPlaylistTrack:
                    return ListPlaylistTracksToListTracks(data, serviceProvider, user);
                default:
                    return null;
            }
        }

        private object ConvertToRedditPosts(object data, TriggerCompatibilityEnum action, IServiceProvider serviceProvider,Models.Account user)
        {
            if (action == TriggerCompatibilityEnum.RedditPosts)
                return data;
            return null;
        }

        private object ConvertToString(object data, TriggerCompatibilityEnum action, IServiceProvider serviceProvider,Models.Account user)
        {
            switch (action)
            {
                case TriggerCompatibilityEnum.ListSimpleAlbum:
                    return ListAlbumsToString(data, action, serviceProvider, user);
                case TriggerCompatibilityEnum.ListSimpleTrack:
                    return ListSimpleTrackToString(data, action, serviceProvider, user);
                case TriggerCompatibilityEnum.ListTwitchUser:
                    return ListTwitchFollowersToString(data, action, serviceProvider, user);
                case TriggerCompatibilityEnum.String:
                    return data;
                case TriggerCompatibilityEnum.RedditComments:
                    return RedditCommentToString(data, action, serviceProvider, user);
                case TriggerCompatibilityEnum.RedditPosts:
                    return RedditPostToString(data, action, serviceProvider, user);
                case TriggerCompatibilityEnum.SteamNews:
                    return SteamNewsToString(data, action, serviceProvider, user);
                case TriggerCompatibilityEnum.SteamInventoryItem:
                    return SteamInventoryItemToString(data, action, serviceProvider, user);
                case TriggerCompatibilityEnum.YoutubeActivity:
                    return YoutubeActivityToString(data, action, serviceProvider, user);
                case TriggerCompatibilityEnum.None:
                default:
                    return null;
            }
        }

        private object YoutubeActivityToString(object data, TriggerCompatibilityEnum action, IServiceProvider serviceProvider, Models.Account user)
        {
            List<Activity> activities = data as List<Activity>;
            string msg = "There are new activites for some of your subscribed channels : ";

            foreach (Activity activity in activities)
                msg += Environment.NewLine + activity.Snippet.ChannelTitle;
            return msg as object;
        }

        private object RedditCommentToString(object data, TriggerCompatibilityEnum action, IServiceProvider serviceProvider, Models.Account user)
        {
            string msg = "There are new replies to some of your comments";
            return msg as object;
        }

        private object RedditPostToString(object data, TriggerCompatibilityEnum action, IServiceProvider serviceProvider, Models.Account user)
        {
            List<Post> posts = data as List<Post>;
            string msg = "There are new posts in some of your subscribed subreddits : ";

            for (int i = 0; i < posts.Count; i++)
            {
                msg += Environment.NewLine + posts[i].Subreddit + " : " + posts[i].Fullname;
            }
            return msg as object;
        }

        private object SteamNewsToString(object data, TriggerCompatibilityEnum action, IServiceProvider serviceProvider, Models.Account user)
        {
            List<NewsItemModel> news = data as List<NewsItemModel>;
            string msg = "There are news for some of your games :";

            for (int i = 0; i < news.Count; i++)
            {
                msg += Environment.NewLine + news[i].Url;
            }
            return msg as object;
        }

        private object SteamInventoryItemToString(object data, TriggerCompatibilityEnum action, IServiceProvider serviceProvider, Models.Account user)
        {
            List<NewsItemModel> news = data as List<NewsItemModel>;
            string msg = "There are news for some of your games :";

            for (int i = 0; i < news.Count; i++)
            {
                msg += Environment.NewLine + news[i].Url;
            }
            return msg as object;
        }

        private object ListTwitchFollowersToString(object data, TriggerCompatibilityEnum action, IServiceProvider serviceProvider, Models.Account user)
        {
            int newFollowersCount = (data as List<TwitchLib.Api.Helix.Models.Users.User>).Count;
            string msg = "You have " + newFollowersCount + " new follower" + (newFollowersCount > 1 ? "s" : "");

            return msg as object;
        }

        private object ListSimpleTrackToString(object data, TriggerCompatibilityEnum action, IServiceProvider serviceProvider, Models.Account user)
        {
            List<SimpleTrack> tracks = data as List<SimpleTrack>;
            string msg = "";

            msg = "Check out these new tracks added to playlists you follow ";
            for (int i = 0; i < tracks.Count; i++)
            {
                if (i > 0)
                    msg += ", ";
                msg += tracks[i].Name + " by " + tracks[i].Artists[0].Name;
            }
            return msg as object;
        }

        private object ListAlbumsToString(object data, TriggerCompatibilityEnum action, IServiceProvider serviceProvider, Models.Account user)
        {
            List<SimpleAlbum> albums = data as List<SimpleAlbum>;
            string msg = "";

            if (albums.Count == 1)
                msg = "Check out this new album : " + albums[0].Name + " by " + albums[0].Artists[0].Name;
            else
            {
                msg = "Check out these new releases : ";
                for (int i = 0; i < albums.Count; i++)
                {
                    if (i > 0)
                        msg += ", ";
                    msg += albums[i].Name + " by " + albums[i].Artists[0].Name;
                }
            }
            return msg as object;    
        }

        private object ConvertToRedditComments(object data, TriggerCompatibilityEnum action, IServiceProvider serviceProvider, Models.Account user)
        {
            if (action == TriggerCompatibilityEnum.RedditComments)
                return data;
            return null;
        }

        private object ListPlaylistTracksToListTracks(object data, IServiceProvider serviceProvider, Models.Account user)
        {
            List<PlaylistTrack> playlistTracks = data as List<PlaylistTrack>;
            List<SimpleTrack> tracks = new List<SimpleTrack>();
            SpotifyService service = (SpotifyService)serviceProvider.GetService(typeof(SpotifyService));
            SpotifyWebAPI api = service.GetSpotifyWebApi(service.GetSpotifyToken(user));

            Console.WriteLine("Adding tracks from playlist");
            for (int i = 0; i < playlistTracks.Count; i++)
            {
                Console.WriteLine("Adding track " + playlistTracks[i].Track.Name);
                SimpleTrack track = new SimpleTrack();

                track.Artists = playlistTracks[i].Track.Artists;
                track.AvailableMarkets = playlistTracks[i].Track.AvailableMarkets;
                track.DiscNumber = playlistTracks[i].Track.DiscNumber;
                track.DurationMs = playlistTracks[i].Track.DurationMs;
                track.Error = playlistTracks[i].Track.Error;
                track.Explicit = playlistTracks[i].Track.Explicit;
                track.ExternUrls = playlistTracks[i].Track.ExternUrls;
                track.Href = playlistTracks[i].Track.Href;
                track.Id = playlistTracks[i].Track.Id;
                track.Name = playlistTracks[i].Track.Name;
                track.PreviewUrl = playlistTracks[i].Track.PreviewUrl;
                track.Restrictions = playlistTracks[i].Track.Restrictions;
                track.TrackNumber = playlistTracks[i].Track.TrackNumber;
                track.Type = playlistTracks[i].Track.Type;
                track.Uri = playlistTracks[i].Track.Uri;

                tracks.Add(track);
            }
            return tracks as object;
        }

        private object ListAlbumToListTracks(object data, IServiceProvider serviceProvider, Models.Account user)
        {
            List<SimpleAlbum> albums = data as List<SimpleAlbum>;
            List<SimpleTrack> tracks = new List<SimpleTrack>();
            SpotifyService service = (SpotifyService)serviceProvider.GetService(typeof(SpotifyService));
            SpotifyWebAPI api = service.GetSpotifyWebApi(service.GetSpotifyToken(user));

            for (int i = 0; i < albums.Count; i++)
            {
                List<SimpleTrack> albumTracks = api.GetAlbumTracks(albums[i].Id).Items;
                for (int j = 0; j < albumTracks.Count; j++)
                {
                    tracks.Add(albumTracks[j]);
                }
            }
            return tracks as object;
        }
    }
}
