﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SpotifyAPI.Web.Models;
using Area.Services.APIs;
using Area.Services.App;
using Area.Models;
using SpotifyAPI.Web;
using Area.Enums;

namespace Area.Services.Actions.Spotify
{
    public class FollowedArtistNewReleaseSpotifyAction : IAction
    {
        private readonly SpotifyService _spotifyService;
        private List<SimpleAlbum> _newReleases = new List<SimpleAlbum>();

        public TriggerTypeEnum Type => TriggerTypeEnum.ListSimpleAlbum;

        public FollowedArtistNewReleaseSpotifyAction(SpotifyService spotifyService)
        {
            _spotifyService = spotifyService;
        }

        public void CheckAction(Account user)
        {
            var api = _spotifyService.GetSpotifyWebApi(_spotifyService.GetSpotifyToken(user));
            FollowedArtists followedArtists = _spotifyService.GetFollowedArtists(api);
            NewAlbumReleases releases = _spotifyService.GetNewReleases(api);
            DateTime lastCheck = DateTime.Now;
            for (int i = 0; i < followedArtists.Artists.Items.Count; i++)
            {
                for (int j = 0; j < releases.Albums.Items.Count; j++)
                {
                    for (int k = 0; k < releases.Albums.Items[j].Artists.Count; k++)
                    {
                        if (followedArtists.Artists.Items[i].Id == releases.Albums.Items[j].Artists[k].Id
                       && lastCheck < _spotifyService.GetDateFromString(releases.Albums.Items[j].ReleaseDate, releases.Albums.Items[j].ReleaseDatePrecision))
                        {
                            _newReleases.Add(releases.Albums.Items[j]);
                        }
                    }
                }
            }
        }

        public object GetResult()
        {
            return _newReleases;
        }

        public bool IsTriggered()
        {
            return _newReleases.Count != 0;
        }
    }
}
