using Area.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area.Enums
{
    public enum ActionTypeEnum
    {
        [DescriptionActionAttribute("Un nouvelle album d'un artiste suivie", ServiceTypeEnum.Spotify, TriggerCompatibilityEnum.ListSimpleAlbum)]
        FollowedArtistNewReleaseSpotify = 0,
        [DescriptionActionAttribute("Mise à jour d'une playlist suivie", ServiceTypeEnum.Spotify, TriggerCompatibilityEnum.ListPlaylistTrack)]
        FollowedPlaylistUpdatedSpotify = 1,
        [DescriptionActionAttribute("Nouvelle abonnement", ServiceTypeEnum.Twitch, TriggerCompatibilityEnum.None)]
        NewSubTwitch = 2,
        [DescriptionActionAttribute("Nouvelle réponse à un commentaire", ServiceTypeEnum.Reddit, TriggerCompatibilityEnum.None)]
        NewSmashClipTwitch = 3,
        [DescriptionActionAttribute("Nouvelle réponse à un commentaire", ServiceTypeEnum.Reddit, TriggerCompatibilityEnum.None)]
        NewReplyToCommentReddit = 4,
        [DescriptionActionAttribute("Nouveau 'top post'", ServiceTypeEnum.Reddit, TriggerCompatibilityEnum.None)]
        NewTopPostsReddit = 5,
        [DescriptionActionAttribute("Nouveau jeu", ServiceTypeEnum.Steam, TriggerCompatibilityEnum.None)]
        GameNewsSteam = 6
    }
}
