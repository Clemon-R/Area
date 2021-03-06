﻿using Area.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area.Enums
{
    public enum ActionTypeEnum
    {
        [DescriptionActionAttribute("Nouvel album d'un artiste suivi", ServiceTypeEnum.Spotify, TriggerCompatibilityEnum.ListSimpleAlbum, TriggerCompatibilityEnum.String, TriggerCompatibilityEnum.ListSimpleTrack )]
        FollowedArtistNewReleaseSpotify = 0,
        [DescriptionActionAttribute("Mise à jour d'une playlist suivie", ServiceTypeEnum.Spotify, TriggerCompatibilityEnum.ListPlaylistTrack, TriggerCompatibilityEnum.String, TriggerCompatibilityEnum.ListSimpleTrack )]
        FollowedPlaylistUpdatedSpotify = 1,
        [DescriptionActionAttribute("Nouveau Follower", ServiceTypeEnum.Twitch, TriggerCompatibilityEnum.ListTwitchUser, TriggerCompatibilityEnum.String, TriggerCompatibilityEnum.ListTwitchUser )]
        NewSubTwitch = 2,
        [DescriptionActionAttribute("Nouveau clip smash sur Twitch", ServiceTypeEnum.Twitch, TriggerCompatibilityEnum.None, TriggerCompatibilityEnum.String )]
        NewSmashClipTwitch = 3,
        [DescriptionActionAttribute("Nouvelle réponse à un commentaire", ServiceTypeEnum.Reddit, TriggerCompatibilityEnum.RedditComments, TriggerCompatibilityEnum.String, TriggerCompatibilityEnum.RedditComments )]
        NewReplyToCommentReddit = 4,
        [DescriptionActionAttribute("Nouveau 'top post' des subreddits suivis", ServiceTypeEnum.Reddit, TriggerCompatibilityEnum.RedditPosts, TriggerCompatibilityEnum.String, TriggerCompatibilityEnum.RedditPosts )]
        NewTopPostsReddit = 5,
        [DescriptionActionAttribute("News des jeux Steam achetés", ServiceTypeEnum.Steam, TriggerCompatibilityEnum.None)]
        GameNewsSteam = 6,
        [DescriptionActionAttribute("Nouvel item dans l'inventaire Steam", ServiceTypeEnum.Steam, TriggerCompatibilityEnum.None)]
        NewInventoryItemSteam = 7,
        [DescriptionActionAttribute("Nouvelle activité intéressante sur Youtube", ServiceTypeEnum.Youtube, TriggerCompatibilityEnum.YoutubeActivity, TriggerCompatibilityEnum.String)]
        NewYoutubeActivity = 8
    }
}
