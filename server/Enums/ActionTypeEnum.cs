using Area.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area.Enums
{
    public enum ActionTypeEnum
    {
        [DescriptionAttribut("Un nouvelle album d'un artiste suivie", TriggerCompatibilityEnum.ListSimpleAlbum, ServiceTypeEnum.Spotify)]
        FollowedArtistNewReleaseSpotify = 0,
        [DescriptionAttribut("Mise à jour d'une playlist suivie", TriggerCompatibilityEnum.ListPlaylistTrack, ServiceTypeEnum.Spotify)]
        FollowedPlaylistUpdatedSpotify = 1,
        [DescriptionAttribut("Nouvelle abonnement", TriggerCompatibilityEnum.ListTwitchFollowers, ServiceTypeEnum.Twitch)]
        NewSubTwitch = 2
    }
}
