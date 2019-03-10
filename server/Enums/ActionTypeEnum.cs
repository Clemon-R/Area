using Area.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area.Enums
{
    public enum ActionTypeEnum
    {
        [DescriptionActionAttribute("Un nouvelle album d'un artiste suivie", ServiceTypeEnum.Spotify, TriggerCompatibilityEnum.ListSimpleAlbum, new TriggerCompatibilityEnum[] { TriggerCompatibilityEnum.String, TriggerCompatibilityEnum.ListSimpleTrack })]
        FollowedArtistNewReleaseSpotify = 0,
        [DescriptionActionAttribute("Mise à jour d'une playlist suivie", ServiceTypeEnum.Spotify, TriggerCompatibilityEnum.ListPlaylistTrack, new TriggerCompatibilityEnum[] { TriggerCompatibilityEnum.String, TriggerCompatibilityEnum.ListSimpleTrack })]
        FollowedPlaylistUpdatedSpotify = 1,
        [DescriptionActionAttribute("Nouvel abonnement", ServiceTypeEnum.Twitch, TriggerCompatibilityEnum.ListTwitchFollowers, new TriggerCompatibilityEnum[] { TriggerCompatibilityEnum.String, TriggerCompatibilityEnum.ListTwitchFollowers })]
        NewSubTwitch = 2,
        [DescriptionActionAttribute("Nouveau clip smash sur Twitch", ServiceTypeEnum.Reddit, TriggerCompatibilityEnum.None, new TriggerCompatibilityEnum[] { TriggerCompatibilityEnum.String })]
        NewSmashClipTwitch = 3,
        [DescriptionActionAttribute("Nouvelle réponse à un commentaire", ServiceTypeEnum.Reddit, TriggerCompatibilityEnum.RedditComments, new TriggerCompatibilityEnum[] { TriggerCompatibilityEnum.String, TriggerCompatibilityEnum.RedditComments })]
        NewReplyToCommentReddit = 4,
        [DescriptionActionAttribute("Nouveau 'top post' des subreddits suivis", ServiceTypeEnum.Reddit, TriggerCompatibilityEnum.RedditPosts, new TriggerCompatibilityEnum[] { TriggerCompatibilityEnum.String, TriggerCompatibilityEnum.RedditPosts })]
        NewTopPostsReddit = 5,
        [DescriptionActionAttribute("News des jeux Steam achetés", ServiceTypeEnum.Steam, TriggerCompatibilityEnum.None)]
        GameNewsSteam = 6,
        [DescriptionActionAttribute("Nouvel item dans l'inventaire Steam", ServiceTypeEnum.Steam, TriggerCompatibilityEnum.None)]
        NewInventoryItemSteam = 7,
        [DescriptionActionAttribute("Nouvelle activité intéressante sur Youtube", ServiceTypeEnum.Youtube, TriggerCompatibilityEnum.YoutubeActivity)]
        NewYoutubeActivity = 7,
    }
}

        [DescriptionActionAttribute("News des jeux Steam achetés", ServiceTypeEnum.Steam, TriggerCompatibilityEnum.None, new TriggerCompatibilityEnum[] { TriggerCompatibilityEnum.String })]
        GameNewsSteam = 6,
        [DescriptionActionAttribute("Nouvel item dans l'inventaire Steam", ServiceTypeEnum.Steam, TriggerCompatibilityEnum.None, new TriggerCompatibilityEnum[] { TriggerCompatibilityEnum.String })]
        NewInventoryItemSteam = 7,
        [DescriptionActionAttribute("Nouvelle activité intéressante sur Youtube", ServiceTypeEnum.Youtube, TriggerCompatibilityEnum.YoutubeActivity, new TriggerCompatibilityEnum[] { TriggerCompatibilityEnum.String })]
        NewYoutubeActivity = 8,